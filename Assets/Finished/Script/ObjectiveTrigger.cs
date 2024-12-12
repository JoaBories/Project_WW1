using UnityEngine;

public class CheckpointObjectiveTrigger2D : MonoBehaviour
{
    [Header("Objective Details")]
    public string objectiveShort = "Find the key"; // Short version of the objective
    public string objectiveExpanded = "Search the nearby area to find the key to unlock the door."; // Expanded version

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ObjectiveManager objectiveManager = FindObjectOfType<ObjectiveManager>();
            if (objectiveManager != null)
            {
                objectiveManager.SetObjective(objectiveShort, objectiveExpanded);
                Debug.Log($"Objective Updated: {objectiveShort}");
            }
            else
            {
                Debug.LogError("ObjectiveManager not found in the scene!");
            }

            // Optional: Disable the checkpoint after activation to prevent duplicate triggers
            gameObject.SetActive(false);
        }
    }
}
