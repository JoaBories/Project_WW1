using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    private Controls inputActions;
    private InputAction JournalMenuAction;

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
    }

    private void Start()
    {
        // Initialize the states
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
        // Debug functionality for testing (can be removed in production)
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    Debug.Log("Toggling Oxygen Panel");
        //    oxygenPanel.SetActive(!oxygenPanel.activeSelf);
        //}

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
        //inputActions.Gameplay.Enable();
    }

    public void Pause()
    {
        HUDPanel.SetActive(false);
        menuGroup.SetActive(true);
        ActivatePanel(pausePanel);
        Time.timeScale = 0f;
        isPaused = true;
        SetBackgroundBlur(true);

        NewMovement.instance.lockMovements();
        Actions.Instance.LockGameplay();
        //inputActions.Gameplay.Disable();
    }

    public void QuitStart()
    {
        ActivatePanel(areYouSurePanel);
    }

    public void OptionsTab()
    {
        ActivatePanel(optionsPanel);
    }

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
    private void SetBackgroundBlur(bool isActive)
    {
        if (backgroundBlur != null)
        {
            backgroundBlur.SetActive(isActive);

            if (isActive)
            {
                // Adjust padding if needed
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