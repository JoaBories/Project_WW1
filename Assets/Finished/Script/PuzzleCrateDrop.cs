using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCrateDrop : MonoBehaviour
{
    [SerializeField] GameObject crate;

    public void Off()
    {
        crate.GetComponent<Rigidbody2D>().simulated = false;
    }


    public void On()
    {
        crate.GetComponent<Rigidbody2D>().simulated = true;
    }
}
