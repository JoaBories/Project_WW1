using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum ZoneTypes
{
    None,
    Climb,
    BarbedWire,
    Door,
    SideOfRoom,
    SceneChangeSideOfRoom,
    Gas,
    Mask,
    Crate,
    Radio,
    SufferingSoldier,
    Shootings,
    ConstantShootings,
    BarbedWireReload,
    SceneChangeDoor
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
    public float crateMovement;

    public GameObject radioObject;
    public Sprite brokenRadio;

    [NonSerialized] public bool shooting;
    private float nextStatechange;
    public float coolDown;
    public GameObject ZoneShot;

    public int sceneNum;

    private void FixedUpdate()
    {
        if (Time.time > nextStatechange)
        {
            if (!shooting)
                ZoneShot.SetActive(true);
            else if (shooting == true)
                ZoneShot.SetActive(false);
            nextStatechange = Time.time + coolDown;
            shooting = !shooting;
        }
    }


    private void OnDrawGizmos()
    {
        if (GetComponent<BoxCollider2D>() == null) return;
        _Boxcollider = GetComponent<BoxCollider2D>();
        if (GetComponent<CircleCollider2D>() != null ) _Circlecollider = GetComponent<CircleCollider2D>();

        switch (type)
        {
            case ZoneTypes.Climb:
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(transform.position + new Vector3(_Boxcollider.offset.x * transform.localScale.x, _Boxcollider.offset.y * transform.localScale.y, 0), _Boxcollider.size * transform.localScale);
                break;

            case ZoneTypes.Crate:
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(transform.position + new Vector3(_Boxcollider.offset.x * transform.localScale.x, _Boxcollider.offset.y * transform.localScale.y, 0), _Boxcollider.size * transform.localScale);
                break;

            case ZoneTypes.BarbedWire:
                Gizmos.color = Color.red;
                if (GetComponent<CircleCollider2D>() == null) return ;
                Gizmos.DrawWireSphere(transform.position + new Vector3(_Circlecollider.offset.x * transform.localScale.x, _Circlecollider.offset.y * transform.localScale.y, 0), _Circlecollider.radius * transform.localScale.x);
                Gizmos.DrawWireCube(transform.position + new Vector3(_Boxcollider.offset.x * transform.localScale.x, _Boxcollider.offset.y * transform.localScale.y, 0), _Boxcollider.size * transform.localScale);
                break;

            case ZoneTypes.Door:
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(transform.position + new Vector3(_Boxcollider.offset.x * transform.localScale.x, _Boxcollider.offset.y * transform.localScale.y, 0), _Boxcollider.size * transform.localScale);
                break;

            case ZoneTypes.SideOfRoom:
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(transform.position + new Vector3(_Boxcollider.offset.x * transform.localScale.x, _Boxcollider.offset.y * transform.localScale.y, 0), _Boxcollider.size * transform.localScale);
                break;

            case ZoneTypes.Mask:
                Gizmos.color = Color.white;
                Gizmos.DrawWireCube(transform.position + new Vector3(_Boxcollider.offset.x * transform.localScale.x, _Boxcollider.offset.y * transform.localScale.y, 0), _Boxcollider.size * transform.localScale);
                break;

            case ZoneTypes.Radio:
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(transform.position + new Vector3(_Boxcollider.offset.x * transform.localScale.x, _Boxcollider.offset.y * transform.localScale.y, 0), _Boxcollider.size * transform.localScale);
                break;

            case ZoneTypes.SufferingSoldier:
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(transform.position + new Vector3(_Boxcollider.offset.x * transform.localScale.x, _Boxcollider.offset.y * transform.localScale.y, 0), _Boxcollider.size * transform.localScale);
                break;

            case ZoneTypes.Gas:
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(transform.position + new Vector3(_Boxcollider.offset.x * transform.localScale.x, _Boxcollider.offset.y * transform.localScale.y, 0), _Boxcollider.size * transform.localScale);
                break;

            case ZoneTypes.Shootings:
                if (shooting) Gizmos.color = Color.red;
                else Gizmos.color = Color.green;
                Gizmos.DrawWireCube(transform.position + new Vector3(_Boxcollider.offset.x * transform.localScale.x, _Boxcollider.offset.y * transform.localScale.y, 0), _Boxcollider.size * transform.localScale);
                break;

            case ZoneTypes.ConstantShootings:
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(transform.position + new Vector3(_Boxcollider.offset.x * transform.localScale.x, _Boxcollider.offset.y * transform.localScale.y, 0), _Boxcollider.size * transform.localScale);
                break;

        }
        
    }

    public void Push()
    {
        StartCoroutine(PushCoroutin(crateMovement));
    }

    public IEnumerator PushCoroutin(float distanceToTravel)
    {
        int iterateNumber = 0;
        while (iterateNumber < 100)
        {
            crateObject.transform.position += new Vector3(distanceToTravel/100, 0, 0);
            iterateNumber++;
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(gameObject);
    }

    public void DestroyRadio()
    {
        radioObject.GetComponent<SpriteRenderer>().sprite = brokenRadio;
        Destroy(gameObject);
    }

    public void ExecuteSoldier()
    {
        Destroy(gameObject);
    }
}
