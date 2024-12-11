using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatbubbleAnim : MonoBehaviour
{
    public GameObject NPC;



    void Start()
    {
        
    }

    void Launch()

    {
        NPC.GetComponent<NPCDialogue>().delock();

    }


    void Update()
    {
        
    }
}
