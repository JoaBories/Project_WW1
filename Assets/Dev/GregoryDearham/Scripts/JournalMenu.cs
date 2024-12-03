using UnityEngine;
using UnityEngine.InputSystem;

public class JournalMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject menuGroup; 
    public GameObject optionsPanel; 
    public GameObject mapPanel; 
    public GameObject pausePanel;

    private Controls inputActions;
    private InputAction JournalMenuAction;



    //public GameObject questPanel;

    private void Awake()
    {
        inputActions = new Controls();
    }

    private void OnEnable()
    {
        JournalMenuAction = inputActions.UI.JournalMenu;
        JournalMenuAction.Enable();

        JournalMenuAction.performed += JournalMenuInput;
    }



    private void Start()
    {
        
        menuGroup.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f; 
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape)  )
    //    {

    //    }
    //}

    public void Resume()
    {
        
        menuGroup.SetActive(false);
        Time.timeScale = 1f; 
        isPaused = false;

        PlayerMovements.instance.delockMovements();
    }

    public void Pause()
    {
        
        menuGroup.SetActive(true);
        ActivatePanel(pausePanel);

        Time.timeScale = 0f; 
        isPaused = true;


        PlayerMovements.instance.lockMovements();
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


    private void JournalMenuInput(InputAction.CallbackContext context)
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
