using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    private TriggerZone currentZone;
    private static HashSet<ZoneTypes> triggeredZoneTypes = new HashSet<ZoneTypes>(); 

    [Header("Zone Type to Sprite Mapping")]
    public Sprite climbSprite;
    public Sprite doorSprite;
    public Sprite crateSprite;
    public Sprite radioSprite;
    public Sprite gasSprite;

    private Dictionary<ZoneTypes, Sprite> spriteMapping;

    private void Start()
    {
        spriteMapping = new Dictionary<ZoneTypes, Sprite>
        {
            { ZoneTypes.Climb, climbSprite },
            { ZoneTypes.Door, doorSprite },
            { ZoneTypes.Crate, crateSprite },
            { ZoneTypes.Radio, radioSprite },
            { ZoneTypes.Gas, gasSprite }
        };
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out TriggerZone zone))
        {
            currentZone = zone;

           
            if (triggeredZoneTypes.Contains(zone.type))
            {
                return;
            }

            
            triggeredZoneTypes.Add(zone.type);

           
            if (spriteMapping.TryGetValue(zone.type, out Sprite interactionSprite))
            {
               
                InteractionUI.Instance.ShowInteraction(GetInteractionText(zone.type), interactionSprite);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (currentZone != null && other.GetComponent<TriggerZone>() == currentZone)
        {
            InteractionUI.Instance.HideInteraction();
            currentZone = null;
        }
    }

    private string GetInteractionText(ZoneTypes type)
    {
        switch (type)
        {
            case ZoneTypes.Climb: return "Press A to Climb";
            case ZoneTypes.Door: return "Go UP Analog to Open Door";
            case ZoneTypes.Crate: return "Press X to Push Crate";
            case ZoneTypes.Radio: return "Press X to Interact with Radio";
            case ZoneTypes.Gas: return "Press LT to put gask on";
            default: return "";
        }
    }
}
