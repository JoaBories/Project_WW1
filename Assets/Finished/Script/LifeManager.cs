using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public static LifeManager instance;

    public GameObject currentCheckpoint;
    private GameObject triggerZone;
    private TriggerZone triggerZoneScript;

    private Animator _animator;

    public bool inGas;
    public float gasTimer;
    [SerializeField] private float gasMaxTime;

    private void Awake()
    {
        instance = this;
        gasTimer = gasMaxTime;

        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (inGas && !PlayerMask.instance.mask)
        {
            gasTimer -= Time.deltaTime;
            if (gasTimer <= 0)
            {
                Die();
                gasTimer = gasMaxTime;
            }
        }
        Debug.Log(gasTimer);
    }

    public void Die()
    {
        NewMovement.instance.SwitchState(NewMoveStates.action, true);
        _animator.Play("groundDeath");
    }

    public void Respawn()
    {
        transform.position = currentCheckpoint.transform.position;
        NewMovement.instance.SwitchState(NewMoveStates.idle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("checkpoint"))
        {
            currentCheckpoint = collision.gameObject;
        } 
        else if (collision.CompareTag("TriggerZone"))
        {
            triggerZone = collision.gameObject;
            triggerZoneScript = triggerZone.GetComponent<TriggerZone>();
            switch (triggerZoneScript.type)
            {
                case ZoneTypes.BarbedWire:
                    Die();
                    break;

                case ZoneTypes.Gas:
                    inGas = true; 
                    break;

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("TriggerZone"))
        {
            triggerZone = collision.gameObject;
            triggerZoneScript = triggerZone.GetComponent<TriggerZone>();
            switch (triggerZoneScript.type)
            {
                case ZoneTypes.Gas:
                    inGas = false;
                    gasTimer = gasMaxTime;
                    break;

            }
        }
    }

    private void OnDrawGizmos()
    {
        if (currentCheckpoint != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(currentCheckpoint.transform.position, 0.2f);
        }
    }
}
