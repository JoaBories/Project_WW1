using UnityEngine;
using UnityEngine.InputSystem;

public enum MoveStates
{
    idle,
    walk,
    run,
    lieDown,
    crawl,
    action,
    cinematic
}

public class PlayerMovements : MonoBehaviour
{
    public static PlayerMovements instance;

    private Controls _inputActions;
    private InputAction _moveAction;
    private InputAction _runAction;
    private InputAction _lieAction;

    private Animator _animator;

    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float crawlSpeed;

    public MoveStates State;

    private bool running;

    private void Awake()
    {
        instance = this;

        _inputActions = new Controls();
        
        _animator = GetComponent<Animator>();
        
        State = MoveStates.idle;
    }

    private void OnEnable()
    {
        _moveAction = _inputActions.Movements.Move;
        _moveAction.Enable();

        _runAction = _inputActions.Movements.Run;
        _runAction.Enable();
        _runAction.started += StartRun;
        _runAction.canceled += StopRun;

        _lieAction = _inputActions.Movements.LieDown;
        _lieAction.Enable();
        _lieAction.performed += LieDown;
    }

    private void OnDisable()
    {
        _moveAction.Disable();

        _runAction.Disable();
    }

    private void Update()
    {
        float _moveDir = _moveAction.ReadValue<Vector2>().x;
        if (_moveDir != 0)
        {
            if (_moveDir < 0) _moveDir /= -_moveDir;
            else _moveDir /= _moveDir;
        }

        if (Mathf.Abs(_moveDir) < 0.1f)
        {
            if (State == MoveStates.crawl) SwitchState(MoveStates.lieDown);
            else if (State == MoveStates.walk || State == MoveStates.run) SwitchState(MoveStates.idle);
        }
        else
        {
            if (State == MoveStates.lieDown) SwitchState(MoveStates.crawl);
            else if (State == MoveStates.idle || State == MoveStates.walk || State == MoveStates.run)
            {
                if (running) SwitchState(MoveStates.run);
                else SwitchState(MoveStates.walk);
            }
        }
        
        switch (State)
        {
            case MoveStates.walk:
                transform.position += new Vector3(walkSpeed * _moveDir * Time.deltaTime, 0, 0);
                break;

            case MoveStates.run:
                transform.position += new Vector3(runSpeed * _moveDir * Time.deltaTime, 0, 0);
                break;

            case MoveStates.crawl:
                transform.position += new Vector3(crawlSpeed * _moveDir * Time.deltaTime, 0, 0);
                break;
        }
    }

    public void SwitchState(MoveStates nextState)
    {
        switch (nextState)
        {
            case MoveStates.lieDown:
                if (State == MoveStates.run) _animator.Play("dive");
                else if (State != MoveStates.lieDown && State != MoveStates.crawl) _animator.Play("lyingDown");
                SetMoveTreeFloats(1, 0);
                break;

            case MoveStates.idle:
                if (State == MoveStates.lieDown || State == MoveStates.crawl) _animator.Play("gettingUp");
                SetMoveTreeFloats(0, 0);
                break;

            case MoveStates.walk:
                SetMoveTreeFloats(0, 1);
                break;

            case MoveStates.crawl:
                SetMoveTreeFloats(1, 1);
                break;

            case MoveStates.run:
                SetMoveTreeFloats(1, 2);
                break;
        }

        State = nextState;
    }

    private void StartRun(InputAction.CallbackContext context)
    {
        running = true;
    }

    private void StopRun(InputAction.CallbackContext context)
    {
        running = false;
    }

    private void LieDown(InputAction.CallbackContext context)
    {
        if (State == MoveStates.idle || State == MoveStates.walk || State == MoveStates.run) SwitchState(MoveStates.lieDown);
        else if (State == MoveStates.lieDown || State == MoveStates.crawl) SwitchState(MoveStates.idle);
    }

    private void SetMoveTreeFloats(float type, float speed)
    {
        _animator.SetFloat("Speed", speed);
        _animator.SetFloat("Type", type);
    }
}
