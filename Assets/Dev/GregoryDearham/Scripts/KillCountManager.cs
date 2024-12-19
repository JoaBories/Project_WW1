using UnityEngine;

public class KillCountManager : MonoBehaviour
{
    public static KillCountManager Instance; // Singleton instance

    private int killCount = 0; // Tracks the number of kills

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate instances
        }
    }

    public void AddKill()
    {
        killCount++;
        Debug.Log($"Kill added. Total kills: {killCount}");
    }

    public int GetKillCount()
    {
        // Return the current kill count
        return killCount;
    }

    public void ResetKillCount()
    {
        killCount = 0;
        Debug.Log("Kill count reset to 0.");
    }
}
