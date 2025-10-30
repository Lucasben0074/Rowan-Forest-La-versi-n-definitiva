//using UnityEngine;
//using TMPro;
//using System.Collections;
//using Unity.VisualScripting;

//public class NPC1 : MonoBehaviour, IInteractable
//{
//    [SerializeField]
//    private CharacterMovement playerMovement;

//    [SerializeField]
//    private GameObject dialoguePanel;
//    [SerializeField]
//    private Dialogue dialogueData;
//    [SerializeField]
//    private TMP_Text dialogueText;

//    private int dialogueIndex;

//    private bool isTyping;
//    private bool isDialogueActive;

//    public void Interact()
//    {
//        if (dialogueData == null) return;

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
//            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]); 
//            isTyping = false;
//        }
//        else if (++dialogueIndex < dialogueData.dialogueLines.Length)
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

//        foreach (char c in dialogueData.dialogueLines[dialogueIndex])
//        {
//            dialogueText.text += c;
//            yield return new WaitForSeconds(dialogueData.typingSpeed);
//        }

//        isTyping = false;

//        if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
//        {
//            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
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
