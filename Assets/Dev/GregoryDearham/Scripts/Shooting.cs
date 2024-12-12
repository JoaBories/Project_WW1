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
       
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isShooting) 
        {
            
            isShooting = true;
            NewMovement.instance.lockMovements(); 
            Actions.Instance.LockGameplay();     

            
            animator.SetTrigger("Attack");

            
            ShootNormalProjectile();

           
            StartCoroutine(ResetShooting());
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

    System.Collections.IEnumerator ResetShooting()
    {
        
        yield return new WaitForSeconds(0.5f); 

       
        NewMovement.instance.delockMovements(); 
        Actions.Instance.DelockGameplay();     
        isShooting = false;
    }
}
