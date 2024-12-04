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
    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;

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
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _ActionAction = _tempInputActions.Gameplay.Action;
        _ActionAction.Enable();
        _ActionAction.performed += doAction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TriggerZone") && NewMovement.instance.standing && NewMovement.instance.CheckGround())
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
        if (currentTriggerZone != null && NewMovement.instance.CheckGround() && NewMovement.instance.standing)
        {
            TriggerZone triggerZone = currentTriggerZone.GetComponent<TriggerZone>();

            switch (triggerZone.type)
            {
                case ZoneTypes.Climb:
                    if (_spriteRenderer.flipX == !triggerZone.climb_right)
                    {
                        transform.position = new Vector3(currentTriggerZone.transform.position.x, transform.position.y, transform.position.z);
                        _animator.Play("climb");
                        _rigidbody.gravityScale = 0;
                        _collider.enabled = false;
                        if (triggerZone.climb_right)
                        {
                            StartCoroutine(climbDisplacement(new Vector3(0, triggerZone.climb_height / climbSmoothness, 0)));
                        }
                        else
                        {
                            StartCoroutine(climbDisplacement(new Vector3(0, triggerZone.climb_height / climbSmoothness, 0)));
                        }
                    }
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
        _collider.enabled = true;
    }
}