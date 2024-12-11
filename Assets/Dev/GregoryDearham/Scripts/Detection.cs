using UnityEngine;
using UnityEngine.UI;

public class Detection : MonoBehaviour
{
    public Slider questionMarkSlider; 
    public Slider exclamationMarkSlider; 
    public float detectionSpeed = 1f; 
    public float decaySpeed = 0.5f; 
    public Image questionMarkIcon;
    public Image exclamationMarkIcon; 
    public Color normalColor = Color.white; 
    public Color activeColor = Color.red; 
    public AudioSource suspicionSound; 
    public AudioSource alertSound; 
    public float resetDelay = 2f; 

    private bool inQuestionZone = false;
    private bool inExclamationZone = false;
    private bool suspicionTriggered = false;
    private bool alertTriggered = false;

    private void Update()
    {
        // Increase detection in Question Mark zone
        if (inQuestionZone)
        {
            questionMarkSlider.value += detectionSpeed * Time.deltaTime;
            questionMarkSlider.value = Mathf.Clamp01(questionMarkSlider.value);
            UpdateIconColor(questionMarkIcon, questionMarkSlider.value > 0);
        }
        else
        {
            // Decay suspicion level if not in the zone
            questionMarkSlider.value -= decaySpeed * Time.deltaTime;
            questionMarkSlider.value = Mathf.Clamp01(questionMarkSlider.value);
            UpdateIconColor(questionMarkIcon, false);
        }

        // Trigger suspicion action when slider fills
        if (questionMarkSlider.value >= 1f && !suspicionTriggered)
        {
            TriggerSuspicion();
        }

        // Increase detection in Exclamation Mark zone
        if (inExclamationZone)
        {
            exclamationMarkSlider.value += detectionSpeed * Time.deltaTime;
            exclamationMarkSlider.value = Mathf.Clamp01(exclamationMarkSlider.value);
            UpdateIconColor(exclamationMarkIcon, exclamationMarkSlider.value > 0);
        }
        else
        {
            // Decay alert level if not in the zone
            exclamationMarkSlider.value -= decaySpeed * Time.deltaTime;
            exclamationMarkSlider.value = Mathf.Clamp01(exclamationMarkSlider.value);
            UpdateIconColor(exclamationMarkIcon, false);
        }

        // Trigger alert action when slider fills
        if (exclamationMarkSlider.value >= 1f && !alertTriggered)
        {
            TriggerAlert();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("QuestionZone"))
        {
            inQuestionZone = true;
            //suspicionSound?.Play(); // Play suspicion sound
        }

        if (other.CompareTag("ExclamationZone"))
        {
            inExclamationZone = true;
            //alertSound?.Play(); // Play alert sound
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("QuestionZone"))
        {
            inQuestionZone = false;
        }

        if (other.CompareTag("ExclamationZone"))
        {
            inExclamationZone = false;
        }
    }

    private void TriggerSuspicion()
    {
        Debug.Log("Suspicion Maxed! Enemy is watching...");
        suspicionTriggered = true;

        // Optional: Add additional behavior here (e.g., call for backup).
    }

    private void TriggerAlert()
    {
        Debug.Log("Alert Maxed! Enemy is attacking...");
        alertTriggered = true;

        // Optional: Add additional behavior here (e.g., start combat).
        Invoke(nameof(ResetSliders), resetDelay); // Reset after delay
    }

    private void UpdateIconColor(Image icon, bool isActive)
    {
        if (icon != null)
        {
            icon.color = isActive ? activeColor : normalColor;
        }
    }

    private void ResetSliders()
    {
        questionMarkSlider.value = 0;
        exclamationMarkSlider.value = 0;
        suspicionTriggered = false;
        alertTriggered = false;
    }
}
