using UnityEngine;

public class JournalMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject menuGroup; 
    public GameObject optionsPanel; 
    public GameObject mapPanel; 
    public GameObject pausePanel;
    //public GameObject questPanel;

    private void Start()
    {
        
        menuGroup.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f; 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        
        menuGroup.SetActive(false);
        Time.timeScale = 1f; 
        isPaused = false;
    }

    public void Pause()
    {
        
        menuGroup.SetActive(true);
        ActivatePanel(pausePanel);

        Time.timeScale = 0f; 
        isPaused = true;
    }

    public void OptionsTab()
    {
        
        ActivatePanel(optionsPanel);
    }


    //public void QuestTab()
    //{

    //    ActivatePanel(questPanel);
    //}


    public void MapTab()
    {
        
        ActivatePanel(mapPanel);
    }

    public void QuitGame()
    {
        Debug.Log("Quit button pressed.");
        Application.Quit();
    }

    private void ActivatePanel(GameObject panelToActivate)
    {
        
        foreach (Transform child in menuGroup.transform)
        {
            child.gameObject.SetActive(false);
        }

        
        panelToActivate.SetActive(true);
    }
}
