using UnityEngine;

public class EnemyStun : MonoBehaviour
{
    public bool Stun = false;
    private Animator _animator;
    

    private void Start()
    {
        _animator = GetComponent<Animator>();
       
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ElectricShock"))
        {
            Debug.Log("ElectricShock collision detected");




            AudioManager.Instance.PlaySFX("GermanFall");
            _animator.Play("EnemyStun");
                Stun = true;
            
        }
    }

    public void killSoldier()
    {
        Destroy(gameObject);
    }
}
