using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject menuGroup; // Group containing all panels
    public GameObject optionsPanel; // Options panel

    [Header("Default Buttons")]
    public Button mainMenuDefaultButton; // Default button for main menu
    public Button optionsPanelDefaultButton; // Default button for options panel

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
        // Initialize menu state
        menuGroup.SetActive(true);
        optionsPanel.SetActive(false);

        // Set the default button for the main menu
        if (mainMenuDefaultButton != null)
        {
            EventSystem.current.SetSelectedGameObject(mainMenuDefaultButton.gameObject);
        }
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
        ActivatePanel(optionsPanel, optionsPanelDefaultButton);
    }

    public void StartGame()
    {
        //SceneAudioManager.Instance.LoadScene("GregsDevCorner");
        SceneManager.LoadScene("LVLtuto");
    }

    public void QuitGame()
    {
        Debug.Log("Quit button pressed.");
        Application.Quit();
    }

    private void ActivatePanel(GameObject panelToActivate, Button defaultButton)
    {
        // Deactivate all panels in menuGroup
        foreach (Transform child in menuGroup.transform)
        {
            child.gameObject.SetActive(false);
        }

        // Activate the specified panel
        panelToActivate.SetActive(true);

        // Set the default button for the activated panel
        if (defaultButton != null)
        {
            EventSystem.current.SetSelectedGameObject(defaultButton.gameObject);
        }
    }

    public void BackToMainMenu()
    {
        // Deactivate all panels in menuGroup
        // Deactivate all child panels of menuGroup
        foreach (Transform child in menuGroup.transform)
        {
            child.gameObject.SetActive(false);
        }

        // Reactivate the main menu panel explicitly (assumed to be the first child)
        if (menuGroup.transform.childCount > 0)
        {
            menuGroup.transform.GetChild(0).gameObject.SetActive(true); // Main menu panel
        }

        // Set focus on the main menu default button for controller navigation
        if (mainMenuDefaultButton != null)
        {
            EventSystem.current.SetSelectedGameObject(mainMenuDefaultButton.gameObject);
        }
    }
}
