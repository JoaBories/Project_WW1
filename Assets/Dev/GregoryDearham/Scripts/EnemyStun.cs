using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyStun : MonoBehaviour
{
    public static EnemyStun instance;
    public bool Stun = false;
    private Animator _animator;
    [SerializeField]  private Canvas childCanvas;
    

    private void Start()
    {
        _animator = GetComponent<Animator>();
        instance = this;
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("ElectricShock"))
        {
            Debug.Log("Dead");
            _animator.Play("EnemyStun");
            
            Stun = true;

            Destroy(childCanvas);
            

        }


    }

    


}
