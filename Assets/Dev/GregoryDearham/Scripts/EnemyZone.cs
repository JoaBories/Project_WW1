using UnityEngine;

public class EnemyZone : MonoBehaviour
{
    [SerializeField] public bool KillZone; // Indicates if the KillZone is active.
    private EnemyStun enemyStun;          // Reference to the EnemyStun component on the parent or attached object.

    private void Start()
    {
        // Attempt to find the EnemyStun component on this GameObject or its parent.
        enemyStun = GetComponentInParent<EnemyStun>();
        if (enemyStun == null)
        {
            Debug.LogError("EnemyStun component not found on parent or current GameObject.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (enemyStun != null && enemyStun.Stun)
            {
                KillZone = false;
                Debug.Log("Enemy is stunned.");
            }
            else
            {
                KillZone = true;
                Debug.Log("KillZone is active.");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            KillZone = false;
            Debug.Log("KillZone is inactive.");
        }
    }
}
