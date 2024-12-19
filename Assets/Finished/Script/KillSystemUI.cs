using UnityEngine;

public class KillSystemeUI : MonoBehaviour
{
    public static int targetPresses = 5; // Starting target presses
    private int currentPresses = 0;      // Tracks current presses
    public GameObject canvasToDisable;  // Canvas to hide
    public Animator animator;           // Animator controlling animations
    private int lastKillCount = 0;      // Tracks the last recorded kill count

    void Update()
    {
        // Debug: Simulate kills with the P key
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("P Pressed - Simulating a kill");
            KillCountManager.Instance.AddKill(); // Simulate a kill in the manager
        }

        // Check if the kill count has increased
        if (KillCountManager.Instance != null)
        {
            int currentKillCount = KillCountManager.Instance.GetKillCount();

            // If the kill count has increased
            if (currentKillCount > lastKillCount)
            {
                // Increment targetPresses for each new kill
                while (lastKillCount < currentKillCount)
                {
                    targetPresses++;
                    lastKillCount++;
                    Debug.Log($"Kill detected. New target presses: {targetPresses}");
                }
            }
        }
        else
        {
            Debug.LogError("KillCountManager.Instance is null! Ensure it exists in the scene.");
        }

        // Detect Mouse0 (Left Mouse Button) press
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            currentPresses++;
            Debug.Log($"Pressing: {currentPresses}/{targetPresses}");

            // Check if the target presses are reached
            if (currentPresses >= targetPresses)
            {
                Debug.Log("Canvas to disable is now gone");

                // Disable the canvas
                if (canvasToDisable != null)
                {
                    canvasToDisable.SetActive(false);
                }
                else
                {
                    Debug.LogWarning("Canvas to disable is not assigned!");
                }

                // Update animator state
                if (animator != null)
                {
                    animator.SetBool("Execute", false);
                }
                else
                {
                    Debug.LogWarning("Animator is not assigned!");
                }

                // Reset presses
                currentPresses = 0;
            }
        }
    }
}


//add screen only displays when executing enemy and cant tap when screen is disabled
// tie with joa code