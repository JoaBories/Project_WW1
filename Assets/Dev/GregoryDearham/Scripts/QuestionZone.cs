using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionZone : MonoBehaviour
{
    [SerializeField] public bool inDetectionZone;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inDetectionZone = true;
        }

    }




    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inDetectionZone = false;

        }

    }


}