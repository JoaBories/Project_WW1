using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public static LifeManager instance;

    public GameObject currentCheckpoint;
    private GameObject triggerZone;
    private TriggerZone triggerZoneScript;

    public bool inGas;
    public float gasTimer;
    [SerializeField] private float gasMaxTime;

    private void Awake()
    {
        instance = this;
        gasTimer = gasMaxTime;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Respawn();
        }

        if (inGas)
        {
            if (!PlayerMask.instance.mask)
            {
                gasTimer -= Time.deltaTime;
                if (gasTimer <= 0)
                {
                    Respawn();
                    gasTimer = gasMaxTime;
                }
            }
        }
    }


    public void Respawn()
    {
        transform.position = currentCheckpoint.transform.position;
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
                    Respawn();
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
