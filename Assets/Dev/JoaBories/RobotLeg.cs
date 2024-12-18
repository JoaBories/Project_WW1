using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SmashPhases
{
    goingUp,
    stayingUp,
    goingRight,
    goingDown,
    stayingDown,
    end
}

public class RobotLeg : MonoBehaviour
{
    [SerializeField] private GameObject floorWaypoint;
    [SerializeField] private GameObject skyWaypoint;

    [SerializeField] private float upSpeed, baseDownSpeed , sideSpeed;
    private float downSpeed;

    [SerializeField] private List<GameObject> smashWaypoints;
    private int currenSmashWaypointIndex;

    private float nextStayingEnd;
    [SerializeField] private float firstStayingStart;
    [SerializeField] private float stayingTime;

    [SerializeField] private SmashPhases phase;

    [SerializeField] private RobotShadow shadow;

    [SerializeField] private RobotLeg otherLeg;
    private Vector3 firstPos;
    private SmashPhases firstPhase;

    private void Start()
    {
        nextStayingEnd = Time.time + firstStayingStart;
        downSpeed = baseDownSpeed;
    }

    private void Update()
    {
        switch (phase)
        {
            case SmashPhases.goingUp:
                transform.position += new Vector3(0, upSpeed * Time.deltaTime, 0);
                if(transform.position.y >= skyWaypoint.transform.position.y)
                {
                    phase = SmashPhases.goingRight;
                    transform.position = new Vector3(transform.position.x, skyWaypoint.transform.position.y, transform.position.z);
                }
                break;

            case SmashPhases.goingDown:
                transform.position += new Vector3(0, -downSpeed * Time.deltaTime, 0);
                if (transform.position.y <= floorWaypoint.transform.position.y)
                {
                    phase = SmashPhases.stayingDown;
                    transform.position = new Vector3(transform.position.x, floorWaypoint.transform.position.y, transform.position.z);
                    nextStayingEnd = Time.time + stayingTime;
                }
                

                break;

            case SmashPhases.goingRight:
                transform.position += new Vector3(sideSpeed * Time.deltaTime, 0, 0);
                if (transform.position.x >= smashWaypoints[currenSmashWaypointIndex].transform.position.x)
                {
                    phase = SmashPhases.stayingUp;
                    transform.position = new Vector3(smashWaypoints[currenSmashWaypointIndex].transform.position.x, transform.position.y, transform.position.z);
                    if (currenSmashWaypointIndex < smashWaypoints.Count)
                    {
                        currenSmashWaypointIndex++;
                    }
                    nextStayingEnd = Time.time + stayingTime;
                }
                break;

            case SmashPhases.stayingDown:
                if(Time.time > nextStayingEnd)
                {
                    phase = SmashPhases.goingUp;
                }
                if (currenSmashWaypointIndex >= smashWaypoints.Count)
                {
                    phase = SmashPhases.end;
                }

                break;

            case SmashPhases.stayingUp:
                if(Time.time > nextStayingEnd)
                {
                    phase = SmashPhases.goingDown;
                }
                break;
        }

        if (shadow.isPlayer)
        {
            phase = SmashPhases.goingDown;
            downSpeed = 100;
        }
    }

    public void Reset()
    {
        transform.position = firstPos;
        phase = firstPhase;
        nextStayingEnd = Time.time + firstStayingStart;
        downSpeed = baseDownSpeed;
    }

}