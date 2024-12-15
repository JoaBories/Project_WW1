using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem; // Required for the new Input System

public class NPCDialogue : MonoBehaviour
{
    [Header("Dialogue Settings")]
    [TextArea(2, 5)] public List<string> dialogueLines;
    public float typingSpeed = 0.1f;
    public float interactionDistance = 3f;

    [Header("Chat Bubble Components")]
    public GameObject chatBubble;
    public TextMeshPro textMeshPro;
    public SpriteRenderer backgroundSpriteRenderer;
    public float paddingX = 1f;
    public float paddingY = 0.5f;

    private Transform player;
    private bool isPlayerNearby = false;
    private bool isTyping = false;
    private int currentLineIndex = 0;
    private Coroutine typingCoroutine;
    private bool chatLock = false;

    [Header("Input System")]
    public InputActionAsset inputActions; 
    private InputAction interactAction;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        chatBubble.SetActive(false);
        textMeshPro.text = "";

        interactAction = inputActions.FindActionMap("Gameplay").FindAction("Interact");

        interactAction.Enable();
    }

    private void Update()
    {
        if (!chatLock)
        {
            CheckPlayerDistance();

            if (isPlayerNearby && interactAction.WasPressedThisFrame())
            {
                if (!isTyping)
                {
                    DisplayNextLine();
                }
            }
        }
    }

    private void CheckPlayerDistance()
    {
        if (player == null) return;
        float distance = Vector3.Distance(transform.position, player.position);
        isPlayerNearby = distance <= interactionDistance;
        if (!isPlayerNearby && chatBubble.activeSelf)
        {
            EndDialogue();
        }
    }

    public void DisplayNextLine()
    {
        if (!chatBubble.activeSelf)
        {
            chatBubble.SetActive(true);
            chatLock = true;
        }

        if (currentLineIndex < dialogueLines.Count)
        {
            if (typingCoroutine != null) StopCoroutine(typingCoroutine);
            typingCoroutine = StartCoroutine(TypeText(dialogueLines[currentLineIndex]));
            currentLineIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    private IEnumerator TypeText(string line)
    {
        isTyping = true;
        textMeshPro.text = "";

        foreach (char c in line)
        {
            textMeshPro.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    private void EndDialogue()
    {
        currentLineIndex = 0;
        chatBubble.SetActive(false);
        textMeshPro.text = "";
    }

    public void delock()
    {
        chatLock = false;
    }
}
