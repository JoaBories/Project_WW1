using Cinemachine;
using System;
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
    private InputAction _jumpAction;

    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;

    [Header("Core Assign")]
    [SerializeField] private GameObject feet1;
    [SerializeField] private GameObject feet2;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private CinemachineVirtualCamera _cam;

    [Header("Values to tweak")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    [SerializeField] private Vector2 staticJump;
    [SerializeField] private Vector2 walkJump;
    [SerializeField] private Vector2 runJump;

    [SerializeField] private float runOrthoSize;
    private float cameraOffset;
    private float baseOrthoSize;

    [NonSerialized] public NewMoveStates State;

    [NonSerialized] public bool moveLock;

    private bool running;

    private void Awake()
    {
        instance = this;

        _inputActions = new Controls();

        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        SwitchState(NewMoveStates.idle);

        cameraOffset = _cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX;
        baseOrthoSize = _cam.m_Lens.OrthographicSize;
    }

    private void OnEnable()
    {
        _moveAction = _inputActions.Movements.Move;
        _moveAction.Enable();

        _runAction = _inputActions.Movements.Run;
        _runAction.Enable();
        _runAction.started += StartRun;
        _runAction.canceled += StopRun;

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
                if (State == NewMoveStates.walk || State == NewMoveStates.run)
                {
                    SwitchState(NewMoveStates.idle);
                }
            }
            else
            {

                if (State == NewMoveStates.idle || State == NewMoveStates.walk || State == NewMoveStates.run)
                {
                    if (running && !PlayerMask.instance.mask)
                    {
                        SwitchState(NewMoveStates.run);
                        _cam.m_Lens.OrthographicSize = runOrthoSize;
                    }
                    else
                    {
                        SwitchState(NewMoveStates.walk);
                        _cam.m_Lens.OrthographicSize = baseOrthoSize;
                    }
                }

                if (_moveDir < 0)
                {
                    _spriteRenderer.flipX = true;
                    _cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 1-cameraOffset;
                }
                else
                {
                    _spriteRenderer.flipX = false;
                    _cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = cameraOffset;
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

        //Debug.Log(State.ToString() + PlayerMask.instance.mask);
    }

    public void SwitchState(NewMoveStates nextState, bool alreadyPlayingAnim = false)
    {
        switch (nextState)
        {
            case NewMoveStates.idle:
                SetMoveTreeFloats(0, 0);
                moveLock = false;
                break;

            case NewMoveStates.walk:
                SetMoveTreeFloats(0, 1);
                moveLock = false;
                break;


            case NewMoveStates.run:
                SetMoveTreeFloats(0, 2);
                moveLock = false;
                break;

            case NewMoveStates.action:
                SetMoveTreeFloats(0, 0);
                if (!alreadyPlayingAnim)
                {
                    _animator.Play("MoveBlendTree");
                }
                moveLock = true;
                break;

            case NewMoveStates.landing:
                _animator.Play("landing");
                moveLock = true;
                break;

            case NewMoveStates.air:
                moveLock = false;
                break;
        }

        State = nextState;
    }

    private void StartRun(InputAction.CallbackContext context)
    {
        if (!PlayerMask.instance.mask) running = true;
    }

    private void StopRun(InputAction.CallbackContext context)
    {
        running = false;
    }

    private void jumpInput(InputAction.CallbackContext context)
    {
        if (CheckGround() && !moveLock && !PlayerMask.instance.mask)
        {
            if (_spriteRenderer.flipX)
            {
                switch (State)
                {
                    case NewMoveStates.walk:
                        _rigidBody.velocity = walkJump * new Vector2(-1, 1);
                        _animator.Play("jumpStart");
                        break;
                    case NewMoveStates.run:
                        _rigidBody.velocity = runJump * new Vector2(-1, 1);
                        _animator.Play("jumpStart");
                        break;
                    case NewMoveStates.idle:
                        _rigidBody.velocity = staticJump * new Vector2(-1, 1);
                        _animator.Play("jumpStart");
                        break;
                }
            }
            else
            {
                switch (State)
                {
                    case NewMoveStates.walk:
                        _rigidBody.velocity = walkJump;
                        _animator.Play("jumpStart");
                        break;
                    case NewMoveStates.run:
                        _rigidBody.velocity = runJump;
                        _animator.Play("jumpStart");
                        break;
                    case NewMoveStates.idle:
                        _rigidBody.velocity = staticJump;
                        _animator.Play("jumpStart");
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
        _inputActions.Movements.Move.Disable();
        _inputActions.Movements.Jump.Disable();
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
