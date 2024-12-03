using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Actions : MonoBehaviour
{
    public static Actions Instance;

    private TempControlsForGameplay _inputActions;
    private InputAction _ActionAction;

    private Utils _utils;

    private GameObject currentTriggerZone;

    private int climbCounter;
    [SerializeField] private int climbSmoothness;
    [SerializeField] private float climbTime;

    private void Awake()
    {
        Instance = this;

        _inputActions = new TempControlsForGameplay();

        _utils = new Utils();
    }

    private void OnEnable()
    {
        _ActionAction = _inputActions.Gameplay.Action;
        _ActionAction.Enable();
        _ActionAction.performed += doAction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TriggerZone"))
        {
            currentTriggerZone = collision.gameObject;
            StartCoroutine(_utils.GamepadVibration(1, 1, 1));
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
            switch (currentTriggerZone.GetComponent<TriggerZone>().type)
            {
                case ZoneTypes.climb:
                    StartCoroutine(climbDisplacement(new Vector3(1 / climbSmoothness, currentTriggerZone.GetComponent<TriggerZone>().climb_height / climbSmoothness, 0)));
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
    }
}
