using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDoor : MonoBehaviour
{
    [SerializeField] private TriggerZone doorTriggerZone;
    [SerializeField] private GameObject doorDisplay;

    public void Off()
    {
        doorTriggerZone.gameObject.SetActive(false);
    }

    public void On()
    {
        doorTriggerZone.gameObject.SetActive(true);
        GetComponent<Animator>().Play("slide");
    }
}
