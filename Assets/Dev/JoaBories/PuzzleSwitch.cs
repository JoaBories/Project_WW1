using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSwitch : MonoBehaviour
{
    public bool state;
    [SerializeField] List<PuzzleDoor> DoorsConnected;
    [SerializeField] List<PuzzleLight> LightConnected;

    private void Start()
    {
        foreach (PuzzleDoor door in DoorsConnected)
        {
            door.Off();
        }
        foreach (PuzzleLight light in LightConnected)
        {
            light.Off();
        }
    }

    public void switchOn()
    {
        state = true;
        GetComponent<Animator>().Play("SwitchActivated");
        GetComponent<Collider2D>().enabled = false;
        foreach (PuzzleDoor door in DoorsConnected)
        {
            door.On();
        }
        foreach (PuzzleLight light in LightConnected)
        {
            light.On();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ElectricShock"))
        {
            switchOn();
        }
    }

}
