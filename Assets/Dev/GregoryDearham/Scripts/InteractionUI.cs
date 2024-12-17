using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    public static InteractionUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI interactionText;
    [SerializeField] private Image interactionSprite;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (interactionText == null)
        {
            Debug.LogError("InteractionText is not assigned in the Inspector.");
        }

        if (interactionSprite == null)
        {
            Debug.LogError("InteractionSprite is not assigned in the Inspector.");
        }
    }

    public void ShowInteraction(string text, Sprite sprite)
    {
        if (interactionText == null || interactionSprite == null)
        {
            Debug.LogError("Interaction UI components are missing!");
            return;
        }

        interactionText.gameObject.SetActive(true);
        interactionText.text = text;

        interactionSprite.gameObject.SetActive(true);
        interactionSprite.sprite = sprite;
    }

    public void HideInteraction()
    {
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }

        if (interactionSprite != null)
        {
            interactionSprite.gameObject.SetActive(false);
        }
    }
}
