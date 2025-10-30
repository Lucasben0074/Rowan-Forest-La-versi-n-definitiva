//using TMPro;
//using UnityEngine;
//using System.Collections;

//public class NPC2 : MonoBehaviour, IInteractable
//{
//    [SerializeField]
//    private CharacterMovement playerMovement;

//    [SerializeField]
//    private GameObject dialoguePanel;
//    [SerializeField]
//    private TMP_Text dialogueText;

//    [SerializeField]
//    private Dialogue[] dialogues;

//    private int dialogueNumber = 0;

//    private int dialogueIndex;

//    private bool isTyping;
//    private bool isDialogueActive;

//    public void Interact()
//    {
//        if (dialogues == null) return;

//        if (isDialogueActive)
//        {
//            NextLine();
//        }
//        else
//        {
//            StartDialogue();
//        }
//    }

//    private void StartDialogue()
//    {
//        isDialogueActive = true;
//        dialogueIndex = 0;

//        dialoguePanel.SetActive(true);

//        StartCoroutine(TypeLine());
//    }

//    private void NextLine()
//    {
//        if (isTyping)
//        {
//            StopAllCoroutines();
//            dialogueText.SetText(dialogues[dialogueNumber].dialogueLines[dialogueIndex]);
//            isTyping = false;
//        }
//        else if (++dialogueIndex < dialogues[dialogueNumber].dialogueLines.Length)
//        {
//            StartCoroutine(TypeLine());
//        }
//        else
//        {
//            EndDialogue();
//        }
//    }

//    private IEnumerator TypeLine()
//    {
//        isTyping = true;
//        dialogueText.SetText(string.Empty);

//        foreach (char c in dialogues[dialogueNumber].dialogueLines[dialogueIndex])
//        {
//            dialogueText.text += c;
//            yield return new WaitForSeconds(dialogues[dialogueNumber].typingSpeed);
//        }

//        isTyping = false;

//        if (dialogues[dialogueNumber].autoProgressLines.Length > dialogueIndex && dialogues[dialogueNumber].autoProgressLines[dialogueIndex])
//        {
//            yield return new WaitForSeconds(dialogues[dialogueNumber].autoProgressDelay);
//            NextLine();
//        }
//    }

//    public void EndDialogue()
//    {
//        StopAllCoroutines();
//        isDialogueActive = false;
//        dialogueText.SetText(string.Empty);
//        dialoguePanel.SetActive(false);
//        //playerMovement.IsInDialogue = false;
//    }
//}
