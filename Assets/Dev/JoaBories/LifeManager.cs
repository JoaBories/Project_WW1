using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public static LifeManager instance;

    public GameObject currentCheckpoint;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Respawn();
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
    }
}
