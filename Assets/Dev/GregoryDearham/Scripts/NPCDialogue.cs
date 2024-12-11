using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public float paddingX = 1f; // Padding around the text
    public float paddingY = 0.5f;

    //[Header("Icon Settings")]
    ////public IconType iconType;
    //public SpriteRenderer iconSpriteRenderer;
    ////public Sprite sideQuestIconSprite;
    ////public Sprite dialogueIconSprite;
    ////public Sprite mainQuestIconSprite;

    //public enum IconType
    //{
    //    SideQuest,
    //    MainQuest,
    //    Dialogue
    //}

    private Transform player;
    private bool isPlayerNearby = false;
    private bool isTyping = false;
    private int currentLineIndex = 0;
    private Coroutine typingCoroutine;
    private bool chatLock = false;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        chatBubble.SetActive(false);
        //iconSpriteRenderer.sprite = GetIconSprite(iconType);
        textMeshPro.text = "";

        
    }

    private void Update()
    {
        if (!chatLock)
        {

           CheckPlayerDistance();
            if (isPlayerNearby && Input.GetKeyDown(KeyCode.E)) //Change for controller
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = isPlayerNearby ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
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
            //ResizeBackgroundToFitText();
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

    //private void ResizeBackgroundToFitText()
    //{
    //    textMeshPro.ForceMeshUpdate(); // Ensure the text layout is up-to-date
    //    Vector2 textSize = textMeshPro.GetRenderedValues(true); // Get text size
    //    Vector2 padding = new Vector2(paddingX, paddingY);
    //    Vector2 backgroundSize = textSize + (2f * padding);
    //    backgroundSpriteRenderer.size = backgroundSize; // Adjust background size
    //    backgroundSpriteRenderer.transform.localPosition = new Vector3(
    //        backgroundSize.x / 0.5f,
    //        0f,
    //        0f
    //    );
    //}

    //private Sprite GetIconSprite(IconType iconType)
    //{
    //    switch (iconType)
    //    {
    //        default:
    //        case IconType.Dialogue:
    //            return dialogueIconSprite;
    //        case IconType.MainQuest:
    //            return mainQuestIconSprite;
    //        case IconType.SideQuest:
    //            return sideQuestIconSprite;
    //    }
    //}

    public void delock()
    {

        chatLock = false;   



    }


}