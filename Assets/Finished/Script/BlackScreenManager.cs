using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreenManager : MonoBehaviour
{
    public static BlackScreenManager Instance;

    private Animator _animator;

    private void Awake()
    {
        Instance = this;

        _animator = GetComponent<Animator>();
    }


    public void goTransparent()
    {
        _animator.Play("blackToTransparent");
    }

    public void goBlack()
    {
        _animator.Play("transparentToBlack");
    }

    public void endTransition()
    {
        NewMovement.instance.SwitchState(NewMoveStates.idle);
        NewMovement.instance.delockMovements();
    }

    public void Teleport()
    {
        Actions.Instance.Teleport();
    }

}
