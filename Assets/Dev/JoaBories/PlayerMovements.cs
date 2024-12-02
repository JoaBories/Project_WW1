using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerMovementStates
{
    walk,
    run,
    crawl,
    climb,
    idle
}

public class PlayerMovements : MonoBehaviour
{
    public static PlayerMovements instance;

    private Controls _inputActions;
    private InputAction _moveAction;

    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    public static PlayerMovementStates State;

    private void Awake()
    {
        instance = this;

        _inputActions = new Controls();
        
        State = PlayerMovementStates.idle;
    }

    private void OnEnable()
    {
        _moveAction = _inputActions.Movements.Move;
        _moveAction.Enable();
    }

    private void OnDisable()
    {
        _moveAction.Disable();
    }

    private void Update()
    {
        float _moveDir = _moveAction.ReadValue<Vector2>().x;
        
        switch (State)
        {
            case PlayerMovementStates.walk:
                break;
        }
    }

    public void SwitchState(PlayerMovementStates nextState)
    {
        State = nextState;
    } 
}
