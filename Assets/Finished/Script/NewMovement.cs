using UnityEngine;
using UnityEngine.InputSystem;

public enum NewMoveStates
{
    idle,
    walk,
    run,
    //lieDown,
    //crawl,
    air,
    landing,
    action
}

public class NewMovement : MonoBehaviour
{
    public static NewMovement instance;

    private Controls _inputActions;

    private InputAction _moveAction;
    private InputAction _runAction;
    //private InputAction _lieAction;
    private InputAction _jumpAction;

    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private GameObject feet1;
    [SerializeField] private GameObject feet2;
    [SerializeField] private LayerMask groundLayers;

    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    //[SerializeField] private float crawlSpeed;

    public NewMoveStates State;

    private bool running;
    //public bool standing;

    public bool moveLock;

    private void Awake()
    {
        instance = this;

        _inputActions = new Controls();

        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        SwitchState(NewMoveStates.idle);
    }

    private void OnEnable()
    {
        _moveAction = _inputActions.Movements.Move;
        _moveAction.Enable();

        _runAction = _inputActions.Movements.Run;
        _runAction.Enable();
        _runAction.started += StartRun;
        _runAction.canceled += StopRun;

        //_lieAction = _inputActions.Movements.LieDown;
        //_lieAction.Enable();
        //_lieAction.performed += LieDown;

        _jumpAction = _inputActions.Movements.Jump;
        _jumpAction.Enable();
        _jumpAction.performed += jumpInput;
    }

    private void OnDisable()
    {
        _moveAction.Disable();

        _runAction.Disable();

        _jumpAction.Disable();
    }

    private void Update()
    {
        Debug.Log(CheckGround());

        float _moveDir = _moveAction.ReadValue<Vector2>().x;
        if (_moveDir != 0)
        {
            if (_moveDir < 0) _moveDir /= -_moveDir;
            else _moveDir /= _moveDir;
        }

        if (!moveLock)
        {

            if (Mathf.Abs(_moveDir) < 0.1f)
            {
                //if (State == NewMoveStates.crawl) SwitchState(NewMoveStates.lieDown);

                if (State == NewMoveStates.walk || State == NewMoveStates.run) SwitchState(NewMoveStates.idle);
            }
            else
            {
                //if (State == NewMoveStates.lieDown) SwitchState(NewMoveStates.crawl);

                if (State == NewMoveStates.idle || State == NewMoveStates.walk || State == NewMoveStates.run)
                {
                    if (running) SwitchState(NewMoveStates.run);
                    else SwitchState(NewMoveStates.walk);
                }

                if (_moveDir < 0)
                {
                    _spriteRenderer.flipX = true;
                }
                else
                {
                    _spriteRenderer.flipX = false;
                }
            }

            switch (State)
            {
                case NewMoveStates.walk:
                    transform.position += new Vector3(walkSpeed * _moveDir * Time.deltaTime, 0, 0);
                    break;

                case NewMoveStates.run:
                    transform.position += new Vector3(runSpeed * _moveDir * Time.deltaTime, 0, 0);
                    break;

                //case NewMoveStates.crawl:
                //    transform.position += new Vector3(crawlSpeed * _moveDir * Time.deltaTime, 0, 0);
                //    break;
            }

            if (State == NewMoveStates.air && CheckGround())
            {
                SwitchState(NewMoveStates.landing);
            }

            if (!CheckGround())
            {
                SwitchState(NewMoveStates.air);
                _animator.Play("airLoop");
            }
        }

    }

    public void SwitchState(NewMoveStates nextState)
    {
        switch (nextState)
        {
            //case NewMoveStates.lieDown:
            //    if (State == NewMoveStates.run)
            //    {
            //        _animator.Play("dive");
            //        if (_spriteRenderer.flipX == false)
            //        {
            //            _rigidBody.velocity += new Vector2(5, 0.5f);
            //        }
            //        else
            //        {
            //            _rigidBody.velocity += new Vector2(-5, 0.5f);
            //        }
            //    }
            //    else if (State != NewMoveStates.lieDown && State != NewMoveStates.crawl) _animator.Play("lyingDown");
            //    SetMoveTreeFloats(1, 0);
            //    moveLock = false;
            //    standing = false;
            //    break;

            case NewMoveStates.idle:
                //if (State == NewMoveStates.lieDown || State == NewMoveStates.crawl) _animator.Play("gettingUp");
                SetMoveTreeFloats(0, 0);
                moveLock = false;
                //standing = true;
                break;

            case NewMoveStates.walk:
                SetMoveTreeFloats(0, 1);
                moveLock = false;
                //standing = true;
                break;

            //case NewMoveStates.crawl:
            //    SetMoveTreeFloats(1, 1);
            //    moveLock = false;
            //    standing = false;
            //    break;

            case NewMoveStates.run:
                SetMoveTreeFloats(0, 2);
                moveLock = false;
                //standing = true;
                break;

            case NewMoveStates.action:
                moveLock = true;
                //standing = true;
                break;

            case NewMoveStates.landing:
                _animator.Play("landing");
                moveLock = true;
                //standing = false;
                break;

            case NewMoveStates.air:
                moveLock = false;
                //standing = false;
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

    //private void LieDown(InputAction.CallbackContext context)
    //{
    //    if (State == NewMoveStates.idle || State == NewMoveStates.walk || State == NewMoveStates.run) SwitchState(NewMoveStates.lieDown);
    //    else if (State == NewMoveStates.lieDown || State == NewMoveStates.crawl) SwitchState(NewMoveStates.idle);
    //}

    private void jumpInput(InputAction.CallbackContext context)
    {
        if (CheckGround() /*&& standing*/ && !moveLock)
        {
            if (_spriteRenderer.flipX)
            {
                switch (State)
                {
                    case NewMoveStates.walk:
                        _rigidBody.velocity += new Vector2(-3, 3);
                        _animator.Play("jumpStart");
                        break;
                    case NewMoveStates.run:
                        _rigidBody.velocity += new Vector2(-6, 3);
                        _animator.Play("jumpStart");
                        break;
                    case NewMoveStates.idle:
                        _rigidBody.velocity += new Vector2(0, 3);
                        _animator.Play("jumpInPlace");
                        break;
                }
            }
            else
            {
                switch (State)
                {
                    case NewMoveStates.walk:
                        _rigidBody.velocity += new Vector2(3, 3);
                        _animator.Play("jumpStart");
                        break;
                    case NewMoveStates.run:
                        _rigidBody.velocity += new Vector2(6, 3);
                        _animator.Play("jumpStart");
                        break;
                    case NewMoveStates.idle:
                        _rigidBody.velocity += new Vector2(0, 3);
                        _animator.Play("jumpInPlace");
                        break;
                }
            }
        }
    }

    private void SetMoveTreeFloats(float type, float speed)
    {
        _animator.SetFloat("Speed", speed);
        _animator.SetFloat("Type", type);
    }

    public void lockMovements()
    {
        _inputActions.Movements.Disable();
        moveLock = true;
    }

    public void delockMovements()
    {
        _inputActions.Movements.Enable();
        moveLock = false;
    }

    public void EndLanding()
    {
        SwitchState(NewMoveStates.idle);
    }

    public void SwitchToAir()
    {
        SwitchState(NewMoveStates.air);
    }


    public bool CheckGround()
    {
        RaycastHit2D hit1 = Physics2D.Raycast(feet1.transform.position, Vector2.down, 0.1f, groundLayers);
        RaycastHit2D hit2 = Physics2D.Raycast(feet2.transform.position, Vector2.down, 0.1f, groundLayers);

        if (hit1 || hit2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
