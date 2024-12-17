using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PuzzleLight : MonoBehaviour
{
    public void Off()
    {
        GetComponent<Light2D>().enabled = false;
    }

    public void On()
    {
        GetComponent<Light2D>().enabled = true;
    }
}
