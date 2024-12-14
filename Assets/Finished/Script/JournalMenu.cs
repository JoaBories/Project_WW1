using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Ensure EventSystem is used

public class JournalMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public static JournalMenu instance;

    [Header("Panels")]
    public GameObject menuGroup;
    public GameObject optionsPanel;
    public GameObject mapPanel;
    public GameObject pausePanel;
    public GameObject areYouSurePanel;
    public GameObject oxygenPanel;
    public GameObject HUDPanel;

    [Header("Background Blur")]
    public GameObject backgroundBlur;
    public float blurPadding = 10f;

    [Header("ControlAction")]
    private Controls inputActions;
    private InputAction JournalMenuAction;
    private InputAction rightShoulderAction;
    private InputAction leftShoulderAction;

    [Header("Panel Default Buttons")]
    public Button optionsPanelDefaultButton;
    public Button pausePanelDefaultButton;
    public Button areYouSureButton;

    private void Awake()
    {
        inputActions = new Controls();
        instance = this;
    }

    private void OnEnable()
    {
        JournalMenuAction = inputActions.UI.JournalMenu;
        JournalMenuAction.Enable();
        JournalMenuAction.performed += JournalMenuInput;

        rightShoulderAction = inputActions.UI.RightShoulder;
        leftShoulderAction = inputActions.UI.LeftShoulder;

        rightShoulderAction.Enable();
        leftShoulderAction.Enable();

        rightShoulderAction.performed += OnRightShoulderPressed;
        leftShoulderAction.performed += OnLeftShoulderPressed;
    }

    private void OnDisable()
    {
        rightShoulderAction.performed -= OnRightShoulderPressed;
        leftShoulderAction.performed -= OnLeftShoulderPressed;

        rightShoulderAction.Disable();
        leftShoulderAction.Disable();
    }

    private void OnRightShoulderPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Right Shoulder (RB) pressed!");
        ActivatePanel(optionsPanel, optionsPanelDefaultButton);
    }

    private void OnLeftShoulderPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Left Shoulder (LB) pressed!");
        ActivatePanel(pausePanel, pausePanelDefaultButton);
    }

    private void Start()
    {
        // Initialize states
        oxygenPanel.SetActive(false);
        HUDPanel.SetActive(true);
        menuGroup.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;

        if (backgroundBlur != null)
        {
            backgroundBlur.SetActive(false);
        }
    }

    private void Update()
    {
        oxygenPanel.SetActive(PlayerMask.instance.mask);

        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("Toggling HUD Panel");
            HUDPanel.SetActive(!HUDPanel.activeSelf);
        }
    }

    public void Resume()
    {
        HUDPanel.SetActive(true);
        menuGroup.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        SetBackgroundBlur(false);

        NewMovement.instance.delockMovements();
        Actions.Instance.DelockGameplay();
    }

    public void Pause()
    {
        HUDPanel.SetActive(false);
        menuGroup.SetActive(true);
        ActivatePanel(pausePanel, pausePanelDefaultButton);
        Time.timeScale = 0f;
        isPaused = true;
        SetBackgroundBlur(true);

        NewMovement.instance.lockMovements();
        Actions.Instance.LockGameplay();
    }

    public void QuitStart()
    {
        ActivatePanel(areYouSurePanel, areYouSureButton);
    }

    public void OptionsTab()
    {
        ActivatePanel(optionsPanel, optionsPanelDefaultButton);
    }

    public void QuitGame()
    {
        Debug.Log("Quit button pressed.");
        Application.Quit();
    }

    private void ActivatePanel(GameObject panelToActivate, Button defaultButton)
    {
        foreach (Transform child in menuGroup.transform)
        {
            child.gameObject.SetActive(false);
        }
        panelToActivate.SetActive(true);

        // Set selection to default button if specified
        if (defaultButton != null)
        {
            EventSystem.current.SetSelectedGameObject(defaultButton.gameObject);
        }
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

    private void SetBackgroundBlur(bool isActive)
    {
        if (backgroundBlur != null)
        {
            backgroundBlur.SetActive(isActive);

            if (isActive)
            {
                RectTransform blurRect = backgroundBlur.GetComponent<RectTransform>();
                if (blurRect != null)
                {
                    blurRect.offsetMin = new Vector2(-blurPadding, -blurPadding);
                    blurRect.offsetMax = new Vector2(blurPadding, blurPadding);
                }
            }
        }
    }
}
