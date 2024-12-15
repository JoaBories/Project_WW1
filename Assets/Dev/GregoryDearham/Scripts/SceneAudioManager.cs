using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAudioManagerScript : MonoBehaviour
{
    public static SceneAudioManagerScript Instance;

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
        
        if (sceneName == "NightScene")
        {
            AudioManager.Instance.PlayAmbience(AudioManager.Instance.backgroundNightTrenchies);
        }
        else if (sceneName == "BunkerScene")
        {
            AudioManager.Instance.PlayAmbience(AudioManager.Instance.backgroundBunkerEcho);
        }

        // Load the new scene
        SceneManager.LoadScene(sceneName);

        //SceneManagerScript.Instance.LoadScene("NextScene"); use this
    }
}
