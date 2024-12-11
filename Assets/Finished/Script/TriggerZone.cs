using Unity.VisualScripting;
using UnityEngine;

public enum ZoneTypes
{
    None,
    Climb,
    BarbedWire,
    Door,
    SideOfRoom,
    Gas,
    Mask,
    Crate,
    Radio
}

public class TriggerZone : MonoBehaviour
{
    public ZoneTypes type;
    private BoxCollider2D _Boxcollider;
    private CircleCollider2D _Circlecollider;

    public bool climb_right;

    public GameObject nextDoor;
    public bool toRight;

    public GameObject crateObject;
    public Vector3 crateMovement;

    public GameObject radioObject;

    private void OnDrawGizmos()
    {
        switch (type)
        {
            case ZoneTypes.Climb:
                _Boxcollider = GetComponent<BoxCollider2D>();
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(transform.position + new Vector3(_Boxcollider.offset.x * transform.localScale.x, _Boxcollider.offset.y * transform.localScale.y, 0), _Boxcollider.size * transform.localScale);
                break;

            case ZoneTypes.Crate:
                _Boxcollider = GetComponent<BoxCollider2D>();
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(transform.position + new Vector3(_Boxcollider.offset.x * transform.localScale.x, _Boxcollider.offset.y * transform.localScale.y, 0), _Boxcollider.size * transform.localScale);
                break;

            case ZoneTypes.BarbedWire:
                _Circlecollider = GetComponent<CircleCollider2D>();
                _Boxcollider = GetComponent<BoxCollider2D>();
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position + new Vector3(_Circlecollider.offset.x * transform.localScale.x, _Circlecollider.offset.y * transform.localScale.y, 0), _Circlecollider.radius * transform.localScale.x);
                Gizmos.DrawWireCube(transform.position + new Vector3(_Boxcollider.offset.x * transform.localScale.x, _Boxcollider.offset.y * transform.localScale.y, 0), _Boxcollider.size * transform.localScale);
                break;

            case ZoneTypes.Door:
                _Boxcollider = GetComponent<BoxCollider2D>();
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(transform.position + new Vector3(_Boxcollider.offset.x * transform.localScale.x, _Boxcollider.offset.y * transform.localScale.y, 0), _Boxcollider.size * transform.localScale);
                break;

            case ZoneTypes.SideOfRoom:
                _Boxcollider = GetComponent<BoxCollider2D>();
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(transform.position + new Vector3(_Boxcollider.offset.x * transform.localScale.x, _Boxcollider.offset.y * transform.localScale.y, 0), _Boxcollider.size * transform.localScale);
                break;

            case ZoneTypes.Mask:
                _Boxcollider = GetComponent<BoxCollider2D>();
                Gizmos.color = Color.white;
                Gizmos.DrawWireCube(transform.position + new Vector3(_Boxcollider.offset.x * transform.localScale.x, _Boxcollider.offset.y * transform.localScale.y, 0), _Boxcollider.size * transform.localScale);
                break;

            case ZoneTypes.Radio:
                _Boxcollider = GetComponent<BoxCollider2D>();
                Gizmos.color = Color.white;
                Gizmos.DrawWireCube(transform.position + new Vector3(_Boxcollider.offset.x * transform.localScale.x, _Boxcollider.offset.y * transform.localScale.y, 0), _Boxcollider.size * transform.localScale);
                break;

            case ZoneTypes.Gas:
                _Boxcollider = GetComponent<BoxCollider2D>();
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(transform.position + new Vector3(_Boxcollider.offset.x * transform.localScale.x, _Boxcollider.offset.y * transform.localScale.y, 0), _Boxcollider.size * transform.localScale);
                break;

        }
        
    }

    public void Push()
    {
        crateObject.transform.position += crateMovement;
        Destroy(gameObject);
    }

    public void DestroyRadio()
    {
        Destroy(gameObject);
    }
}
