using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueMain : MonoBehaviour
{
    [Header("Dialogue TMP Text")]
    [SerializeField] private float typingSpeed = 0.05f;

    [Header("Continue")]
    [SerializeField] private TextMeshProUGUI nPCDialogueText;

    [Header("Dialogue Sentences")]
    [SerializeField] private string[] nPCDialogueSentences;

    [SerializeField] private GameObject nPCTextButton;

    private int nPCIndex;

    void Start()
    {
        StartDialogue();
    }

    public void StartDialogue()
    {
        StartCoroutine(TypeNPCDialogue());
    }

    private IEnumerator TypeNPCDialogue()
    {
        nPCDialogueText.text = ""; // Clear previous text

        foreach (char letter in nPCDialogueSentences[nPCIndex].ToCharArray())
        {
            nPCDialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        nPCTextButton.SetActive(true); // Enable the button after typing is complete
    }

    public void ContinueDialogue()
    {
        if (nPCIndex < nPCDialogueSentences.Length - 1)
        {
            nPCIndex++;
            nPCTextButton.SetActive(false);
            StartCoroutine(TypeNPCDialogue());
        }
        else
        {
            nPCTextButton.SetActive(false); // Hide button when dialogue ends
        }
    }
}

