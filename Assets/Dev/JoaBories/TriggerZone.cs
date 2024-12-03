using UnityEngine;

public enum ZoneTypes
{
    None,
    climb
}

public class TriggerZone : MonoBehaviour
{
    public ZoneTypes type;

    public float climb_height;
}
