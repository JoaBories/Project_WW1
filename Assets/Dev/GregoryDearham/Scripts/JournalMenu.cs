using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class JournalMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject menuGroup; 
    public GameObject optionsPanel; 
    public GameObject mapPanel; 
    public GameObject pausePanel;
    public GameObject areYouSurePanel;
    public GameObject OxygenPanel;
    public GameObject HUDPanel;
    private Controls inputActions;
    private InputAction JournalMenuAction;




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Toggling Oxygen Panel");
            OxygenPanel.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("Toggling HUD Panel");
            HUDPanel.SetActive(true);
        }
    }








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
        OxygenPanel.SetActive(false);
        HUDPanel.SetActive(true);
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
        HUDPanel.SetActive(true);

        menuGroup.SetActive(false);
        Time.timeScale = 1f; 
        isPaused = false;

        PlayerMovements.instance.delockMovements();
    }

    public void Pause()
    {
        HUDPanel.SetActive(false);

        menuGroup.SetActive(true);
        ActivatePanel(pausePanel);

        Time.timeScale = 0f; 
        isPaused = true;
        

        PlayerMovements.instance.lockMovements();
    }

    public void QuitStart()

    {


        ActivatePanel(areYouSurePanel);

        


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

    public void Yes()
    {
        SceneManager.LoadScene("Start");
    }

    public void Back()
    {

        Pause();

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TriggerZone"))
        {
           if (collision.GetComponent<TriggerZone>().type == ZoneTypes.Gas)

            {

                OxygenPanel.SetActive(true);


            }



        }


    }


    private void OnTriggerExit2D(Collider2D collision)
    {


        if (collision.CompareTag("TriggerZone"))
        {
            if (collision.GetComponent<TriggerZone>().type == ZoneTypes.Gas)

            {

                OxygenPanel.SetActive(false);


            }



        }



    }



}
