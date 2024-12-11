using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOmbreRobot : MonoBehaviour
{
    public Animator animator;
    public Transform step;
    private bool goDown = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("DFhudisjf");
            RobotStepWalk.Instance.notTrigger = false;
            animator.enabled = false;
            goDown = true;
        }
    }

    private void Update()
    {
        if (goDown)
        {
            step.position = new Vector3(transform.position.x, -1 * Time.deltaTime, transform.position.z);
        }
    }


}
