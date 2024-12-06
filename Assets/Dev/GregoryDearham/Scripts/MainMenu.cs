using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject menuGroup;
    public GameObject optionsPanel;
    public GameObject loadGamePanel;

    private Controls inputActions;
    private InputAction journalMenuAction;

    private void Awake()
    {
        inputActions = new Controls();
    }

    private void OnEnable()
    {
        journalMenuAction = inputActions.UI.JournalMenu;
        journalMenuAction.Enable();
    }

    private void Start()
    {
        
        menuGroup.SetActive(true);
        optionsPanel.SetActive(false);
        loadGamePanel.SetActive(false);
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMainMenu();
        }
    }

    public void OptionsTab()
    {
        ActivatePanel(optionsPanel);
    }

    public void LoadGame()
    {
        ActivatePanel(loadGamePanel);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GregsDevCorner");
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

    public void BackToMainMenu()
    {
        // Deactivate all panels within menuGroup
        foreach (Transform child in menuGroup.transform)
        {
            child.gameObject.SetActive(false);
        }

        // Activate the main menu panel (menuGroup itself stays active)
        menuGroup.SetActive(true);
        if (menuGroup.transform.childCount > 0)
        {
            menuGroup.transform.GetChild(0).gameObject.SetActive(true); // Assume the first child is the main menu panel
            menuGroup.transform.GetChild(1).gameObject.SetActive(true);
            menuGroup.transform.GetChild(2).gameObject.SetActive(true);
            menuGroup.transform.GetChild(3).gameObject.SetActive(true);
            menuGroup.transform.GetChild(4).gameObject.SetActive(true);



            menuGroup.transform.gameObject.SetActive(true);


        }
    }

}
