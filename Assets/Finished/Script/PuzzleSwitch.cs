using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSwitch : MonoBehaviour
{
    [SerializeField] PuzzleDoor DoorsConnected;
    [SerializeField] PuzzleLight LightToSwitchOn;
    [SerializeField] PuzzleLight LightToSwitchOff;

    private void Start()
    {
        DoorsConnected.Off();
        LightToSwitchOff.On();
        LightToSwitchOn.Off();
    }

    public void switchOn()
    {
        GetComponent<Animator>().Play("SwitchActivated");
        GetComponent<Collider2D>().enabled = false;
        LightToSwitchOn.On();
        LightToSwitchOff.Off();
        DoorsConnected.On();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ElectricShock"))
        {
            switchOn();
        }
    }

}
