using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSwitch : MonoBehaviour
{
    [SerializeField] PuzzleDoor DoorsConnected;
    [SerializeField] PuzzleLight LightToSwitchOn;
    [SerializeField] PuzzleLight LightToSwitchOff;
    [SerializeField] PuzzleCrateDrop CrateDrop;

    private void Start()
    {
        if (DoorsConnected != null) DoorsConnected.Off();
        if (LightToSwitchOff != null) LightToSwitchOff.On();
        if (LightToSwitchOn != null) LightToSwitchOn.Off();
        if (CrateDrop != null) CrateDrop.Off();
    }

    public void switchOn()
    {
        GetComponent<Animator>().Play("SwitchActivated");
        GetComponent<Collider2D>().enabled = false;
        if (DoorsConnected != null) DoorsConnected.On(); 
        if (LightToSwitchOff != null) LightToSwitchOff.Off();
        if (LightToSwitchOn != null) LightToSwitchOn.On();
        if (CrateDrop != null) CrateDrop.On();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ElectricShock"))
        {
            switchOn();
        }
    }

}
