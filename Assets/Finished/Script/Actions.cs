using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Actions : MonoBehaviour
{
    public static Actions Instance;

    private Controls _inputActions;
    private InputAction _ActionAction;

    private Utils _utils;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;

    private GameObject currentTriggerZone;

    private int climbCounter;
    [SerializeField] private int climbSmoothness;
    [SerializeField] private float climbTime;

    private void Awake()
    {
        Instance = this;

        _inputActions = new Controls();

        _utils = new Utils();

        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _ActionAction = _inputActions.Gameplay.Climb;
        _ActionAction.Enable();
        _ActionAction.performed += doAction;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != currentTriggerZone)
        {
            if (collision.CompareTag("TriggerZone") && NewMovement.instance.standing && NewMovement.instance.CheckGround())
            {
                currentTriggerZone = collision.gameObject;
                StartCoroutine(_utils.GamepadVibration(0, 1, 0.1f));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (currentTriggerZone == collision.gameObject)
        {
            currentTriggerZone = null;
        }
    }

    private void doAction(InputAction.CallbackContext context)
    {
        if (currentTriggerZone != null && NewMovement.instance.CheckGround() && NewMovement.instance.standing)
        {
            TriggerZone triggerZone = currentTriggerZone.GetComponent<TriggerZone>();

            switch (triggerZone.type)
            {
                case ZoneTypes.Climb:
                    if (_spriteRenderer.flipX == !triggerZone.climb_right)
                    {
                        _animator.Play("climb");
                        NewMovement.instance.SwitchState(NewMoveStates.action);
                    }
                    break;
            }

        }

    }

    private void EndClimb()
    {
        transform.position = currentTriggerZone.transform.position;
        NewMovement.instance.SwitchState(NewMoveStates.idle);
    }
}