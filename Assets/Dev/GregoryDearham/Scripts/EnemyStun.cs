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
            Debug.Log("ElectricShock collision detected");


            
             

                _animator.Play("EnemyStun");
                Stun = true;
            
        }
    }

    public void killSoldier()
    {
        Destroy(gameObject);
    }
}
