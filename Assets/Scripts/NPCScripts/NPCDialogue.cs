using UnityEngine;


public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private int dialogueID = 1;
    public int DialogueID
    {
        get { return dialogueID; }
        set { dialogueID = value; }
    }

    public void TriggerDialogue()
    {
        DialogueSistem.Instance.StartDialogue(dialogueID);
    }
}
