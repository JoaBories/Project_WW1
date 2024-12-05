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
        // Initialize the main menu panels state
        menuGroup.SetActive(true);
        optionsPanel.SetActive(false);
        loadGamePanel.SetActive(false);
    }

    private void Update()
    {
        // Handle the Escape key input to go back to the main menu or close any open panels
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
        // Deactivate all panels in the menu group
        foreach (Transform child in menuGroup.transform)
        {
            child.gameObject.SetActive(false);
        }

        // Activate the specified panel
        panelToActivate.SetActive(true);
    }

    private void BackToMainMenu()
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
        }
    }

}
