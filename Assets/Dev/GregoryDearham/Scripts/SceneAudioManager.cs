using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAudioManager : MonoBehaviour
{
    public static SceneAudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    public void LoadScene(string sceneName)
    {
        // Trigger appropriate ambience for the scene
        switch (sceneName)
        {
            case "GregsDevCorner":
                AudioManager.Instance.PlayAmbience(AudioManager.Instance.backgroundNightTrenchies);
                break;
            case "BunkerScene":
                AudioManager.Instance.PlayAmbience(AudioManager.Instance.backgroundBunkerEcho);
                break;
            default:
                AudioManager.Instance.StopAmbience(); // Stop ambience if no specific match
                break;
        }
        Debug.Log("The scene loaded with required audio");
        // Load the new scene
        SceneManager.LoadScene(sceneName);
    }
}
