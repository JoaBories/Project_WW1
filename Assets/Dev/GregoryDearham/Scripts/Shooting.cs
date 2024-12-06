using System.Runtime.CompilerServices;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject normalProjectilePrefab; 
    public GameObject chargedProjectilePrefab; 
    public Transform firePoint; 
    public float projectileSpeed = 10f; 
    public float chargeTime = 1.5f; 

    private bool isCharging = false; 
    private float chargeTimer = 0f;
    private bool facingleft = false;





    private void Awake()
    {
        
    }



    void Update()

    {
        if (NewMovement.instance.CheckGround())
        {
            HandleShootingInput();

        }


        
    
        

        

    }

    void HandleShootingInput()
    {





  if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isCharging = true;
            chargeTimer = 0f;
        }


        if (Input.GetKey(KeyCode.Mouse0) && isCharging)
        {
            chargeTimer += Time.deltaTime;
        }


        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (chargeTimer >= chargeTime)
            {
                /*ShootChargedProjectile()*/

            }
            else
            {
                ShootNormalProjectile();

                isCharging = false;
            }
        }

        void ShootNormalProjectile()
        {
            GameObject projectile = Instantiate(normalProjectilePrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();


            if (GetComponent<SpriteRenderer>().flipX)

            {

                    rb.velocity = -firePoint.right * projectileSpeed;




            }

            else
            {
                rb.velocity = firePoint.right * projectileSpeed;

            }
            

            Destroy(projectile, 0.08f);
        }

      



        //void ShootChargedProjectile()
        //{
        //    GameObject chargedProjectile = Instantiate(chargedProjectilePrefab, firePoint.position, Quaternion.identity);
        //    Rigidbody2D rb = chargedProjectile.GetComponent<Rigidbody2D>();
        //    rb.velocity = firePoint.right * projectileSpeed * 1.5f;
        //    chargedProjectile.GetComponent<SpriteRenderer>().color = Color.yellow;

        //    Destroy(chargedProjectile, 0.08f);
        //}
    }
}
