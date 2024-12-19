using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotShadow : MonoBehaviour
{
    public bool isPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.CompareTag("Player")) isPlayer = true;
    }
}
