using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private BoxCollider2D killZone;
    private SpriteRenderer SpriteZone;

    private void Start()
    {
        killZone = GetComponent<BoxCollider2D>();
        SpriteZone = GetComponent<SpriteRenderer>();
        InvokeRepeating("ShootAction", 2f, 2f);
    }

    private void ShootAction()
    {
        if (killZone.enabled)
        {
            killZone.enabled = false;
            SpriteZone.enabled = false;
        }
        else 
        {
            killZone.enabled = true;
            SpriteZone.enabled = true;
        }
    }

}
