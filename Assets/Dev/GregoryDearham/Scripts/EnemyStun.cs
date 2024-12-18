using UnityEngine;

public class EnemyStun : MonoBehaviour
{
    public bool Stun = false;
    private Animator _animator;


    private void Start()
    {
        _animator = GetComponent<Animator>();
       
      
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
