using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackScreenManager : MonoBehaviour
{
    public static BlackScreenManager Instance;

    private Animator _animator;

    private void Awake()
    {
        Instance = this;

        _animator = GetComponent<Animator>();
    }

    public void animationPlay(string name)
    {
        _animator.Play(name);
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

    public void Respawn()
    {
        LifeManager.instance.Respawn();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
