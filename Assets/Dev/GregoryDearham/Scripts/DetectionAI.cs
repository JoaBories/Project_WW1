using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DetectionAI : MonoBehaviour
{

    [SerializeField] QuestionZone DetectionCollider;
    [SerializeField] EnemyZone EnemyCollider;

    [Header("Light Reference")]

    public Light2D detectionLight;

    [Header("Sprites")]
    public SpriteRenderer questionFillSprite;
    public SpriteRenderer questionOutlineSprite;
    public SpriteRenderer exclamationFillSprite;
    public SpriteRenderer exclamationOutlineSprite;

    [Header("Detection Settings")]
    public float baseDetectionSpeed = 1f;
    public float speedBoostMultiplier = 2f;
    public float decaySpeed = 0.5f;

    [Header("Sounds")]
    public AudioSource suspicionSound;
    public AudioSource alertSound;

    private enum DetectionPhase { Question, Exclamation, Cooldown }
    private DetectionPhase currentPhase = DetectionPhase.Question;

    private bool inDetectionZone = false;
    private bool inSpeedBoostZone = false;

    private float questionProgress = 0f;
    private float exclamationProgress = 0f;

    private Material questionFillMat;
    private Material exclamationFillMat;

    private void Start()
    {
        // Instantiate materials for prefab safety
        questionFillMat = Instantiate(questionFillSprite.material);
        exclamationFillMat = Instantiate(exclamationFillSprite.material);

        questionFillSprite.material = questionFillMat;
        exclamationFillSprite.material = exclamationFillMat;

        SetSpritesVisibility(false, false);
    }

    private void Update()
    {
        // Only process if the detection light is enabled
        if (!detectionLight.enabled)
            return;

        if (EnemyCollider == null)
        {
            Debug.Log("Fucked");
            return;
        }

        if (EnemyCollider.KillZone)
        {

            TriggerAlert();


        }


        switch (currentPhase)
        {
            case DetectionPhase.Question:
                if (DetectionCollider.inDetectionZone && !GetComponent<EnemyStun>().Stun)
                {
                    IncreaseQuestionProgress();
                }
                else
                {
                    DecayQuestionProgress();
                }
                break;

            case DetectionPhase.Exclamation:
                if (DetectionCollider.inDetectionZone && !GetComponent<EnemyStun>().Stun)
                {
                    IncreaseExclamationProgress();
                    
                }
                else
                {
                    DecayExclamationProgress();
                }
                break;

           

            case DetectionPhase.Cooldown:
                DecayQuestionProgress();
                break;
        }

        // Update materials for visual feedback
        UpdateShaderFill(questionFillMat, questionProgress);
        UpdateShaderFill(exclamationFillMat, exclamationProgress);

      
    }

  
  

    private void IncreaseQuestionProgress()
    {
        float speed = inSpeedBoostZone ? baseDetectionSpeed * speedBoostMultiplier : baseDetectionSpeed;
        questionProgress = Mathf.Clamp01(questionProgress + speed * Time.deltaTime);

        SetSpritesVisibility(true, false);

        if (questionProgress >= 1f)
        {
            TriggerSuspicion();
        }
    }

    private void IncreaseExclamationProgress()
    {
        float speed = inSpeedBoostZone ? baseDetectionSpeed * speedBoostMultiplier : baseDetectionSpeed;
        exclamationProgress = Mathf.Clamp01(exclamationProgress + speed * Time.deltaTime);

        SetSpritesVisibility(false, true);

        if (exclamationProgress >= 1f)
        {
            TriggerAlert();
        }
    }

    private void DecayQuestionProgress()
    {
        questionProgress = Mathf.Max(0f, questionProgress - decaySpeed * Time.deltaTime);

        if (currentPhase == DetectionPhase.Cooldown && questionProgress <= 0f)
        {
            ResetToQuestionPhase();
        }

        if (questionProgress <= 0f && exclamationProgress <= 0f)
        {
            SetSpritesVisibility(false, false);
        }
    }

    private void DecayExclamationProgress()
    {
        exclamationProgress = Mathf.Max(0f, exclamationProgress - decaySpeed * Time.deltaTime);

        if (exclamationProgress <= 0f)
        {
            StartCooldown();
        }
    }

    private void TriggerSuspicion()
    {
        currentPhase = DetectionPhase.Exclamation;
        questionProgress = 0f;
        //suspicionSound?.Play();
        SetSpritesVisibility(false, true);
    }

    private void TriggerAlert()
    {
        //alertSound?.Play();
        LifeManager.instance.Die();
        //Destroy(gameObject); // Destroy this enemy on alert
    }

    private void StartCooldown()
    {
        currentPhase = DetectionPhase.Cooldown;
        questionProgress = 1f;
        SetSpritesVisibility(true, false);
    }

    private void ResetToQuestionPhase()
    {
        currentPhase = DetectionPhase.Question;
        questionProgress = 0f;
        exclamationProgress = 0f;
        SetSpritesVisibility(false, false);
    }

    private void SetSpritesVisibility(bool showQuestion, bool showExclamation)
    {
        questionFillSprite.enabled = showQuestion;
        questionOutlineSprite.enabled = showQuestion;

        exclamationFillSprite.enabled = showExclamation;
        exclamationOutlineSprite.enabled = showExclamation;
    }

    private void UpdateShaderFill(Material mat, float progress)
    {
        if (mat != null)
        {
            mat.SetFloat("_FillAmount", progress);
        }
    }
}
