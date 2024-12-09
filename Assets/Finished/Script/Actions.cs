using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Actions : MonoBehaviour
{
    public static Actions Instance;

    private Controls _inputActions;
    private InputAction _ClimbAction;
    private InputAction _GoDoorAction;

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
        _ClimbAction = _inputActions.Gameplay.Climb;
        _ClimbAction.Enable();
        _ClimbAction.performed += ClimbAction;

        _GoDoorAction = _inputActions.Gameplay.GoDoor;
        _GoDoorAction.Enable();
        _GoDoorAction.performed += GoDoor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TriggerZone"))
        {
            if (collision.GetComponent<TriggerZone>().type == ZoneTypes.SideOfRoom)
            {
                BlackScreenManager.Instance.goBlack();
                NewMovement.instance.SwitchState(NewMoveStates.action);
                NewMovement.instance.lockMovements();
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != currentTriggerZone)
        {
            if (collision.CompareTag("TriggerZone") /*&& NewMovement.instance.standing*/ && NewMovement.instance.CheckGround())
            {
                currentTriggerZone = collision.gameObject;
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

    private void GoDoor(InputAction.CallbackContext context)
    {
        if (currentTriggerZone != null)
        {
            TriggerZone triggerZone = currentTriggerZone.GetComponent<TriggerZone>();
            if (triggerZone.type == ZoneTypes.Door && NewMovement.instance.CheckGround())
            {
                BlackScreenManager.Instance.goBlack();
                NewMovement.instance.SwitchState(NewMoveStates.action);
                NewMovement.instance.lockMovements();
            }
        }

    }

    private void ClimbAction(InputAction.CallbackContext context)
    {
        if (currentTriggerZone != null)
        {
            TriggerZone triggerZone = currentTriggerZone.GetComponent<TriggerZone>();
            if (_spriteRenderer.flipX == !triggerZone.climb_right && triggerZone.type == ZoneTypes.Climb && NewMovement.instance.CheckGround())
            {
                _animator.Play("climb");
                NewMovement.instance.SwitchState(NewMoveStates.action);
            }

        }

    }

    public void Teleport()
    {
        transform.position = currentTriggerZone.GetComponent<TriggerZone>().nextDoor.transform.position;
        if (currentTriggerZone.GetComponent<TriggerZone>().nextDoor.GetComponent<TriggerZone>().type == ZoneTypes.SideOfRoom)
        {
            GetComponent<SpriteRenderer>().flipX = !currentTriggerZone.GetComponent<TriggerZone>().nextDoor.GetComponent<TriggerZone>().toRight;
        }
    }

    public void EndClimb()
    {
        transform.position = currentTriggerZone.transform.position;
        NewMovement.instance.SwitchState(NewMoveStates.idle);
    }
}