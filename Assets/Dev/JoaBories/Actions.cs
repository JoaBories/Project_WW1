using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Actions : MonoBehaviour
{
    public static Actions Instance;

    private Controls _inputActions;
    private TempControlsForGameplay _tempInputActions;
    private InputAction _ActionAction;

    private Utils _utils;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _boxCollider;

    private GameObject currentTriggerZone;

    private int climbCounter;
    [SerializeField] private int climbSmoothness;
    [SerializeField] private float climbTime;

    private void Awake()
    {
        Instance = this;

        _tempInputActions = new TempControlsForGameplay();
        _inputActions = new Controls();

        _utils = new Utils();

        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        _ActionAction = _tempInputActions.Gameplay.Action;
        _ActionAction.Enable();
        _ActionAction.performed += doAction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TriggerZone"))
        {
            currentTriggerZone = collision.gameObject;
            StartCoroutine(_utils.GamepadVibration(0, 1, 0.1f));
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
        if (currentTriggerZone != null)
        {
            TriggerZone triggerZone = currentTriggerZone.GetComponent<TriggerZone>();

            switch (triggerZone.type)
            {
                case ZoneTypes.Climb:
                    _animator.Play("climb");
                    _rigidbody.gravityScale = 0;
                    _boxCollider.isTrigger = true;
                    PlayerMovements.instance.lockMovements();
                    StartCoroutine(climbDisplacement(new Vector3(1/climbSmoothness, triggerZone.climb_height/climbSmoothness, 0)));
                    break;
            }
        }
    }

    private IEnumerator climbDisplacement(Vector3 displacementPerFrame)
    {
        while (climbCounter < climbSmoothness)
        {
            transform.position += displacementPerFrame;
            climbCounter++;
            yield return new WaitForSeconds(climbTime/climbSmoothness);
        }
        climbCounter = 0;
        _rigidbody.gravityScale = 1;
        _boxCollider.isTrigger = false;
        PlayerMovements.instance.delockMovements();
    }
}