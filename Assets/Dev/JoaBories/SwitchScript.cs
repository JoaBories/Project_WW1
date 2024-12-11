using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    public bool state;
    [SerializeField] List<GameObject> objectsConnected;
    private void Start()
    {
        foreach (GameObject obj in objectsConnected)
        {
            obj.SetActive(state);
        }
    }

    public void switchOn()
    {
        state = true;
        foreach (GameObject obj in objectsConnected)
        {
            obj.SetActive(state);
        }
    }

    public void switchOff()
    {
        state = false;
        foreach (GameObject obj in objectsConnected)
        {
            obj.SetActive(state);
        }
    }
}
