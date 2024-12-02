using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerMovementStates
{
    idle,
    walk,
    run,
    action,
    cinematic
}

public class PlayerMovements : MonoBehaviour
{
    public static PlayerMovements instance;

    private Controls _inputActions;
    private InputAction _moveAction;
    private InputAction _runAction;

    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    public PlayerMovementStates State;

    private bool running;

    private void Awake()
    {
        instance = this;

        _inputActions = new Controls();
        
        State = PlayerMovementStates.idle;
    }

    private void OnEnable()
    {
        _moveAction = _inputActions.Movements.Move;
        _moveAction.Enable();

        _runAction = _inputActions.Movements.Run;
        _runAction.Enable();
        _runAction.started += StartRun;
        _runAction.canceled += StopRun;
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
            if (_moveDir < 0)
            {
                _moveDir = _moveDir / -_moveDir;
            }
            else
            {
                _moveDir = _moveDir / _moveDir;
            }
        }

        Debug.Log(_moveDir);

        if (Mathf.Abs(_moveDir) < 0.1f)
        {
            SwitchState(PlayerMovementStates.idle);
        }
        else
        {
            if(running)
            {
                SwitchState(PlayerMovementStates.run);
            }
            else
            {
                SwitchState(PlayerMovementStates.walk);
            }
        }


        
        switch (State)
        {
            case PlayerMovementStates.idle:

                break;

            case PlayerMovementStates.walk:
                transform.position += new Vector3(walkSpeed * _moveDir * Time.deltaTime, 0, 0);
                break;

            case PlayerMovementStates.run:
                transform.position += new Vector3(runSpeed * _moveDir * Time.deltaTime, 0, 0);
                break;

            case PlayerMovementStates.action:

                break;

            case PlayerMovementStates.cinematic:

                break;
        }
    }

    public void SwitchState(PlayerMovementStates nextState)
    {
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
}
