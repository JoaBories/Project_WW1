using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Actions : MonoBehaviour
{
    public static Actions Instance;

    private Controls _inputActions;
    private InputAction _ClimbAction;
    private InputAction _GoDoorAction;
    private InputAction _InteractAction;

    private Utils _utils;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;

    private GameObject currentTriggerZone;
    private GameObject currentEnemy;
    private TriggerZone currentClimb;

    public bool gameplayLock;

    [SerializeField] private Vector2 climbMovement;

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

        _InteractAction = _inputActions.Gameplay.Interact;
        _InteractAction.Enable();
        _InteractAction.performed += Interact;
    }

    private void OnDisable()
    {
        _ClimbAction.Disable();
        _GoDoorAction.Disable();
        _InteractAction.Disable();
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
        if (collision != currentTriggerZone && collision.CompareTag("TriggerZone"))
        {
            if (collision.GetComponent<TriggerZone>().type != ZoneTypes.Gas)
            {
                currentTriggerZone = collision.gameObject;
            }
        } 
        else if (collision != currentEnemy && collision.CompareTag("Enemy"))
        {
            currentEnemy = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (currentTriggerZone == collision.gameObject)
        {
            currentTriggerZone = null;
        }

        if (currentEnemy == collision.gameObject)
        {
            currentEnemy = null;
        }
    }

    private void GoDoor(InputAction.CallbackContext context)
    {
        if (currentTriggerZone != null && !gameplayLock)
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
        if (currentTriggerZone != null && !gameplayLock)
        {
            TriggerZone triggerZone = currentTriggerZone.GetComponent<TriggerZone>();
            if (_spriteRenderer.flipX == !triggerZone.climb_right && triggerZone.type == ZoneTypes.Climb && NewMovement.instance.CheckGround())
            {
                _animator.Play("climb");
                NewMovement.instance.SwitchState(NewMoveStates.action, true);
                currentClimb = triggerZone;
            }
        }
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (currentTriggerZone != null && NewMovement.instance.CheckGround() && !gameplayLock)
        {
            
            switch (currentTriggerZone.GetComponent<TriggerZone>().type)
            {
                case ZoneTypes.Mask:
                    _animator.Play("interact");
                    NewMovement.instance.SwitchState(NewMoveStates.action, true);
                    break;

                case ZoneTypes.Crate:
                    _animator.Play("interact");
                    NewMovement.instance.SwitchState(NewMoveStates.action, true);
                    break;

                case ZoneTypes.Radio:
                    _animator.Play("interact");
                    NewMovement.instance.SwitchState(NewMoveStates.action, true);
                    break;
            }
        }

        if (currentEnemy != null && NewMovement.instance.CheckGround() && !gameplayLock)
        {
            _animator.Play("execute");
            NewMovement.instance.SwitchState(NewMoveStates.action, true);
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
        if (currentClimb.climb_right)
        {
            transform.position += (Vector3)climbMovement;
        }
        else
        {
            transform.position += (Vector3) (climbMovement * new Vector2(-1, 1));
        }
        NewMovement.instance.SwitchState(NewMoveStates.idle);
        NewMovement.instance.delockMovements();
        currentClimb = null;
    }

    public void EndInteract()
    {
        TriggerZone triggerZone = currentTriggerZone.GetComponent<TriggerZone>();
        switch (triggerZone.type)
        {
            case ZoneTypes.Mask:
                PlayerMask.instance.gotMask = true;
                Destroy(currentTriggerZone);
                break;

            case ZoneTypes.Crate:
                triggerZone.Push();
                break;

            case ZoneTypes.Radio:
                triggerZone.DestroyRadio();
                break;
        }

        if (currentEnemy != null && NewMovement.instance.CheckGround() && !gameplayLock)
        {
            // play something there
        }

        NewMovement.instance.SwitchState(NewMoveStates.idle);
        NewMovement.instance.delockMovements();
    }

    public void LockGameplay()
    {
        _inputActions.Gameplay.Disable();
        gameplayLock = true;
    }

    public void DelockGameplay()
    {
        _inputActions.Gameplay.Enable();
        gameplayLock = false;
    }
}