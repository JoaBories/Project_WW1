using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotStepWalk : MonoBehaviour
{
    private float robotPos;
    public bool notTrigger = true;
    public static RobotStepWalk Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        robotPos = GetComponent<Transform>().position.x;

        InvokeRepeating("StepRobot", 4f, 4f); 
    }

    void StepRobot()
    {
        if (notTrigger == true)
            robotPos += 3; 
        Vector3 newPosition = new Vector3(robotPos, transform.position.y, transform.position.z);
        transform.position = newPosition; 
    }
}

