using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMask : MonoBehaviour
{
    public static PlayerMask instance;

    private TempControls _inputActions;
    private InputAction _keepMaskAction;

    private Animator _animator;

    public bool mask;

    private void Awake()
    {
        _inputActions = new TempControls();

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

    public void BeginMask(InputAction.CallbackContext context)
    {
        mask = true;
        _animator.Play("wearMask");
    }

    public void EndMask(InputAction.CallbackContext context)
    {
        mask = false;
        _animator.Play("removeMask");
    }








}
