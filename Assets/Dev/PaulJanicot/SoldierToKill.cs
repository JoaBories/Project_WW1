using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierToKill : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> enemies = new List<GameObject>();
    public GameObject BlockedDoor;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (enemies[0] == null)
        {
            BlockedDoor.SetActive(false);
        }
    }
}
