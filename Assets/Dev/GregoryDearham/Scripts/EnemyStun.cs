using UnityEngine;

public class EnemyStun : MonoBehaviour
{
    public bool Stun = false;
    private Animator _animator;
    public int soldierToKill;
    public GameObject blockedDoor;
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
        soldierToKill--;
        if (soldierToKill == 0)
        {
            blockedDoor.SetActive(false);
        }
    }
}
