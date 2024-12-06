using Unity.VisualScripting;
using UnityEngine;

public enum ZoneTypes
{
    None,
    Climb,
    BarbedWire
}

public class TriggerZone : MonoBehaviour
{
    public ZoneTypes type;
    private BoxCollider2D _collider;

    public bool climb_right;

    private void OnDrawGizmos()
    {
        _collider = GetComponent<BoxCollider2D>();

        switch (type)
        {
            case ZoneTypes.Climb:
                Gizmos.color = Color.yellow;
                break;

            case ZoneTypes.BarbedWire:
                Gizmos.color = Color.red;
                break;

        }
        Gizmos.DrawWireCube(transform.position + new Vector3(_collider.offset.x, _collider.offset.y, 0), _collider.size);

    }
}
