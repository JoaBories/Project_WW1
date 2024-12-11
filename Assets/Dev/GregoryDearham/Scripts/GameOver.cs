using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    //private CheckpointManager checkpointManager;

    private void Start()
    {
       
        //checkpointManager = FindObjectOfType<CheckpointManager>();
        //if (checkpointManager == null)
        //{
        //    Debug.LogError("CheckpointManager not found in the scene. Restart from Checkpoint will not work.");
        //}
    }

    // Quit to Main Menu
    public void QuitToMainMenu()
    {
        Debug.Log("Returning to Main Menu...");
        Time.timeScale = 1f; // Ensure the game is unpaused
        SceneManager.LoadScene("Start");
    }

    // Restart from Checkpoint
    public void RestartFromCheckpoint()
    {
        //if (checkpointManager != null)
        //{
        //    Debug.Log("Restarting from the last checkpoint...");
        //    Time.timeScale = 1f; // Ensure the game is unpaused
        //    checkpointManager.RestartFromLastCheckpoint();
        //}
        //else
        //{
            Debug.LogError("No CheckpointManager found! Cannot restart from checkpoint.");
        //}
    }
}
