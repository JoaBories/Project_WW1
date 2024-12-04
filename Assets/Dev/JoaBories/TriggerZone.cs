using UnityEngine;

public enum ZoneTypes
{
    None,
    Climb
}

public class TriggerZone : MonoBehaviour
{
    public ZoneTypes type;

    public float climb_height;
    public bool climb_right;
}
