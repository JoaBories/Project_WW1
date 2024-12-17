using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum NewMoveStates
{
    idle,
    walk,
    run,
    jumping,
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
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private CinemachineVirtualCamera _cam;

    [Header("Values to tweak")]
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float decceleration;
    [SerializeField] float velPower;

    [SerializeField] float frictionAmount;

    [SerializeField] private Vector2 staticJump;
    [SerializeField] private Vector2 walkJump;
    [SerializeField] private Vector2 runJump;

    [SerializeField] private float upGravity, downGravity;

    [SerializeField] private float runOrthoSize;
    private float cameraOffset;
    private float baseOrthoSize;

    [NonSerialized] public NewMoveStates State;

    [NonSerialized] public bool moveLock;
    private float _moveDir;

    private bool running;

    private Vector2 nextJumpForce;

    [NonSerialized] public bool isGround;

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

        isGround = CheckGround();
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
        _moveDir = _moveAction.ReadValue<Vector2>().x;
        
        if (_moveDir != 0)
        {
            if (_moveDir < 0) _moveDir /= -_moveDir;
            else _moveDir /= _moveDir;
        }

        if (!moveLock)
        {

            if (Mathf.Abs(_moveDir) < 0.01f)
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

            if (State == NewMoveStates.air && isGround && _rigidBody.velocity.y < 0.1f)
            {
                _animator.Play("landing");
                SwitchState(NewMoveStates.landing);
            }

            if (!isGround)
            {
                SwitchState(NewMoveStates.air);
                _animator.Play("airLoop");
            }
        }
    }

    private void FixedUpdate()
    {
        float targetSpeed;
        float speedDif;
        float accelRate;
        float movement;

        isGround = CheckGround();

        if (!moveLock)
        {
            switch (State)
            {
                case NewMoveStates.walk:
                    targetSpeed = _moveDir * walkSpeed;
                    speedDif = targetSpeed - _rigidBody.velocity.x;
                    accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
                    movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
                    _rigidBody.AddForce(movement * Vector2.right);
                    break;

                case NewMoveStates.air:
                    if (_moveAction.enabled)
                    {
                        targetSpeed = _moveDir * walkSpeed;
                        speedDif = targetSpeed - _rigidBody.velocity.x;
                        accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
                        movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
                        _rigidBody.AddForce(movement * Vector2.right);
                    }
                    break;

                case NewMoveStates.run:
                    targetSpeed = _moveDir * runSpeed;
                    speedDif = targetSpeed - _rigidBody.velocity.x;
                    accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
                    movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
                    _rigidBody.AddForce(movement * Vector2.right);
                    break;
            }
        }

        if (isGround)
        {
            if (State != NewMoveStates.air)
            {
                _rigidBody.gravityScale = 0;
            }

            if (State != NewMoveStates.jumping && State != NewMoveStates.air)
            {
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0);
                RaycastHit2D hit = Physics2D.Raycast(groundCheckPoint.transform.position, Vector2.down, 0.1f, groundLayers);
                transform.position = new Vector3(transform.position.x, hit.point.y + transform.localScale.y * (GetComponent<CapsuleCollider2D>().size.y / 2), transform.position.z);
            }

            if (Mathf.Abs(_moveDir) < 0.01f && State != NewMoveStates.air)
            {
                float amount = Mathf.Min(Mathf.Abs(_rigidBody.velocity.x), Mathf.Abs(frictionAmount)) * Mathf.Sign(_rigidBody.velocity.x);
                _rigidBody.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
            }
        }
        else
        {
            if (_rigidBody.velocity.y <= 0)
            {
                _rigidBody.gravityScale = downGravity;
            }
            else
            {
                _rigidBody.gravityScale = upGravity;
            }
        }

        
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
                moveLock = true;
                break;

            case NewMoveStates.air:
                moveLock = false;
                break;

            case NewMoveStates.jumping:
                moveLock = true;
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
        if (isGround && !moveLock && !PlayerMask.instance.mask)
        {
            if (_spriteRenderer.flipX)
            {
                switch (State)
                {
                    case NewMoveStates.walk:
                        nextJumpForce = walkJump * new Vector2(-1, 1);
                        _animator.Play("jumpStart");
                        break;
                    case NewMoveStates.run:
                        nextJumpForce = runJump * new Vector2(-1, 1);
                        _animator.Play("jumpStart");
                        break;
                    case NewMoveStates.idle:
                        nextJumpForce = staticJump * new Vector2(-1, 1);
                        _animator.Play("jumpStart");
                        break;
                }
            }
            else
            {
                switch (State)
                {
                    case NewMoveStates.walk:
                        nextJumpForce = walkJump;
                        _animator.Play("jumpStart");
                        break;
                    case NewMoveStates.run:
                        nextJumpForce = runJump;
                        _animator.Play("jumpStart");
                        break;
                    case NewMoveStates.idle:
                        nextJumpForce = staticJump;
                        _animator.Play("jumpStart");
                        break;
                }
            }

            SwitchState(NewMoveStates.jumping);
        }
    }

    public void endStartJump()
    {
        _rigidBody.AddForce(nextJumpForce, ForceMode2D.Impulse);
        _rigidBody.gravityScale = upGravity;
        SwitchState(NewMoveStates.air);
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
        return Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayers);
    }

}
