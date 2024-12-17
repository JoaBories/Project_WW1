using UnityEngine;

public class EnemyStun : MonoBehaviour
{
    public static EnemyStun instance;
    public bool Stun = false;
    private Animator _animator;

    private Canvas childCanvas;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        instance = this;

        // Find the Canvas only within this GameObject's immediate children
        childCanvas = transform.Find("Canvas")?.GetComponent<Canvas>();

        if (childCanvas == null)
        {
            Debug.LogError("No child Canvas found under this Enemy prefab!");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("ElectricShock"))
        {
            Debug.Log("Dead");
            _animator.Play("EnemyStun");

            Stun = true;

            // Destroy the specific child Canvas GameObject
            //if (childCanvas != null)
            //{
            //    Destroy(childCanvas.gameObject);
            //    Debug.Log("Child Canvas Destroyed");
            //}
        }
    }

    public void killSoldier()
    {
        Destroy(gameObject);
    }
}
