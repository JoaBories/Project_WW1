using UnityEngine;


using TMPro;

public class InteractionUI : MonoBehaviour
{
    public static InteractionUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI interactionText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void ShowInteraction(string text)
    {
        if (interactionText == null) return;

        interactionText.gameObject.SetActive(true);
        interactionText.text = text;
    }

    public void HideInteraction()
    {
        if (interactionText == null) return;

        interactionText.gameObject.SetActive(false);
    }
}
