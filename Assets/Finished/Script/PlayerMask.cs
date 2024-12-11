using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMask : MonoBehaviour
{
    public static PlayerMask instance;

    private Controls _inputActions;
    private InputAction _keepMaskAction;

    private Animator _animator;

    [NonSerialized] public bool mask;
    /*[NonSerialized]*/ public bool gotMask;

    [SerializeField] private GameObject maskDebug;
    private void Awake()
    {
        _inputActions = new Controls();

        _animator = GetComponent<Animator>();

        instance = this;
    }

    private void OnEnable()
    {
        _keepMaskAction = _inputActions.Gameplay.KeepMask;
        _keepMaskAction.Enable();
        _keepMaskAction.started += BeginMask;
        _keepMaskAction.canceled += EndMask;
    }

    private void OnDisable()
    {
        _keepMaskAction.Disable();
    }

    private void Update()
    {
        maskDebug.SetActive(mask);
    }

    public void BeginMask(InputAction.CallbackContext context)
    {
        if ((NewMovement.instance.State == NewMoveStates.idle || NewMovement.instance.State == NewMoveStates.walk || NewMovement.instance.State == NewMoveStates.run) && gotMask)
        {
            mask = true;
            _animator.Play("wearMask");
        }
    }

    public void EndMask(InputAction.CallbackContext context)
    {
        mask = false;
        if (mask && (NewMovement.instance.State == NewMoveStates.idle || NewMovement.instance.State == NewMoveStates.walk) && gotMask)
        {
            _animator.Play("removeMask");
        }
    }
}
