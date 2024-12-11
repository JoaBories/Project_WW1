using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    [Header("HUD Elements")]
    public TextMeshProUGUI hudObjectiveText; // TextMeshPro for HUD (short version)

    [Header("Pause Menu Elements")]
    public TextMeshProUGUI pauseMenuObjectiveText; // TextMeshPro for pause menu (expanded version)

    private string currentObjectiveShort; // Short version of the objective
    private string currentObjectiveExpanded; // Expanded version of the objective

    // Set the objective
    public void SetObjective(string shortVersion, string expandedVersion)
    {
        currentObjectiveShort = shortVersion;
        currentObjectiveExpanded = expandedVersion;

        // Update the UI elements
        UpdateHUD();
        UpdatePauseMenu();
    }

    private void UpdateHUD()
    {
        if (hudObjectiveText != null)
        {
            hudObjectiveText.text = currentObjectiveShort;
        }
        else
        {
            Debug.LogWarning("HUD Objective Text (TextMeshProUGUI) is not assigned!");
        }
    }

    private void UpdatePauseMenu()
    {
        if (pauseMenuObjectiveText != null)
        {
            pauseMenuObjectiveText.text = currentObjectiveExpanded;
        }
        else
        {
            Debug.LogWarning("Pause Menu Objective Text (TextMeshProUGUI) is not assigned!");
        }
    }
}
