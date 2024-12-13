using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject normalProjectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 0.97f;
    public Animator animator;
    [SerializeField] public float projectileDistance = 0.5f;
    
    private bool facingleft = false; // Detects the facing direction
    private bool isShooting = false; // Tracks whether the character is shooting

    void Update()
    {
        HandleShootingInput();
    }

    void HandleShootingInput()
    {
       
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isShooting && NewMovement.instance.CheckGround()) 
        {
            
            isShooting = true;

            NewMovement.instance.SwitchState(NewMoveStates.action, true);
            
            animator.Play("attack");

            
           

           
          
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

        
        Destroy(projectile, projectileDistance);
    }

    public void ResetShooting()
    {

        NewMovement.instance.delockMovements();
        Actions.Instance.DelockGameplay();

        NewMovement.instance.SwitchState(NewMoveStates.idle);

       


        isShooting = false;
    }
}
