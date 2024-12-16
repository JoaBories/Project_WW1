using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InteractionHandler : MonoBehaviour
{
    private TriggerZone currentZone;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out TriggerZone zone))
        {
            currentZone = zone;

            switch (zone.type)
            {
                case ZoneTypes.Climb:
                    InteractionUI.Instance.ShowInteraction("Press X to Climb");
                    break;
                case ZoneTypes.Door:
                    InteractionUI.Instance.ShowInteraction("Press X to Open Door");
                    break;
                case ZoneTypes.Crate:
                    InteractionUI.Instance.ShowInteraction("Press X to Push Crate");
                    break;
                case ZoneTypes.Radio:
                    InteractionUI.Instance.ShowInteraction("Press X to Interact with Radio");
                    break;
                // Add more cases as needed
                default:
                    InteractionUI.Instance.HideInteraction();
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<TriggerZone>() == currentZone)
        {
            InteractionUI.Instance.HideInteraction();
            currentZone = null;
        }
    }
}
