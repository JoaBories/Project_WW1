using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Detection : MonoBehaviour
{
    [SerializeField] public Light2D Light;

    public Slider questionMarkSlider;
    public Slider exclamationMarkSlider;

    public float baseDetectionSpeed = 1f; 
    public float speedBoostMultiplier = 2f;  
    public float decaySpeed = 0.5f;  

    public Image questionMarkIcon;
    public Image exclamationMarkIcon;
    public Color normalColor = Color.white;
    public Color activeColor = Color.red;

    public AudioSource suspicionSound;
    public AudioSource alertSound;

    public float resetDelay = 2f;

    private enum DetectionPhase { Question, Exclamation }
    private DetectionPhase currentPhase = DetectionPhase.Question;

    private bool inDetectionZone = false;
    private bool inSpeedBoostZone = false;
    private bool suspicionTriggered = false;
    private bool alertTriggered = false;

    private float CurrentDetectionSpeed
    {
        get
        {
            return inSpeedBoostZone
                ? baseDetectionSpeed * speedBoostMultiplier
                : baseDetectionSpeed;
        }
    }

    private void Update()
    {
        if (inDetectionZone && Light.enabled)
        {
            if (currentPhase == DetectionPhase.Question)
            {
                questionMarkSlider.value += CurrentDetectionSpeed * Time.deltaTime;
                questionMarkSlider.value = Mathf.Clamp01(questionMarkSlider.value);

                if (questionMarkIcon != null)
                {
                    questionMarkIcon.color = questionMarkSlider.value > 0 ? activeColor : normalColor;
                }

                if (questionMarkSlider.value >= 1f && !suspicionTriggered)
                {
                    TriggerSuspicion();
                }
            }
            else if (currentPhase == DetectionPhase.Exclamation)
            {
                exclamationMarkSlider.value += CurrentDetectionSpeed * Time.deltaTime;
                exclamationMarkSlider.value = Mathf.Clamp01(exclamationMarkSlider.value);

                if (exclamationMarkIcon != null)
                {
                    exclamationMarkIcon.color = exclamationMarkSlider.value > 0 ? activeColor : normalColor;
                }

                if (exclamationMarkSlider.value >= 1f && !alertTriggered)
                {
                    TriggerAlert();
                }
            }
        }
        else
        {
            if (currentPhase == DetectionPhase.Question)
            {
                questionMarkSlider.value -= decaySpeed * Time.deltaTime;
                questionMarkSlider.value = Mathf.Clamp01(questionMarkSlider.value);

                if (questionMarkIcon != null)
                {
                    questionMarkIcon.color = questionMarkSlider.value > 0 ? activeColor : normalColor;
                }
            }
            else if (currentPhase == DetectionPhase.Exclamation)
            {
                exclamationMarkSlider.value -= decaySpeed * Time.deltaTime;
                exclamationMarkSlider.value = Mathf.Clamp01(exclamationMarkSlider.value);

                if (exclamationMarkIcon != null)
                {
                    exclamationMarkIcon.color = exclamationMarkSlider.value > 0 ? activeColor : normalColor;
                }

                if (exclamationMarkSlider.value <= 0)
                {
                    questionMarkSlider.value = 1f;

                    currentPhase = DetectionPhase.Question;

                    exclamationMarkSlider.gameObject.SetActive(false);
                    exclamationMarkIcon.gameObject.SetActive(false);

                    questionMarkSlider.gameObject.SetActive(true);
                    questionMarkIcon.gameObject.SetActive(true);

                    suspicionTriggered = false;
                    alertTriggered = false;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DetectionZone"))
        {
            inDetectionZone = true;
        }
        else if (other.CompareTag("SpeedBoostZone"))
        {
            inSpeedBoostZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DetectionZone"))
        {
            inDetectionZone = false;
        }
        else if (other.CompareTag("SpeedBoostZone"))
        {
            inSpeedBoostZone = false;
        }
    }

    private void TriggerSuspicion()
    {
        Debug.Log("Suspicion Maxed! Moving to alert phase...");
        suspicionTriggered = true;

        if (suspicionSound != null)
        {
            suspicionSound.Play();
        }

        currentPhase = DetectionPhase.Exclamation;

        questionMarkSlider.gameObject.SetActive(false);
        questionMarkIcon.gameObject.SetActive(false);

        exclamationMarkSlider.gameObject.SetActive(true);
        exclamationMarkIcon.gameObject.SetActive(true);
    }

    private void TriggerAlert()
    {
        Debug.Log("Alert Maxed! Enemy is attacking...");
        alertTriggered = true;

        LifeManager.instance.Die();

        if (alertSound != null)
        {
            alertSound.Play();
        }

        
    }
}