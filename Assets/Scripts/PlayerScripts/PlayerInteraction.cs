using UnityEngine;
using UnityEngine.Events;

public class PlayerInteraction : MonoBehaviour
{

    [SerializeField] private GameObject eventActivator;

    private NPCDialogue currentNPC;
    [SerializeField] private Canvas Interaction;
    private bool specialKey;
    public bool SpecialKey => specialKey;
    private int levelKeyDialogue = 5;
    private bool isLevelKey = false;
    public bool IsLevelKey => isLevelKey;   
    private string stoneName = null;
    private int stoneNumber;
 
    [SerializeField] private GameObject threeBlock;
    [SerializeField] private GameObject secondThreeBlock;
    private void Start()
    {
        eventActivator.SetActive(false);

    }

    void Update()
    {

        Debug.Log(stoneNumber + "stoneNumber");
        if (currentNPC != null && Input.GetKeyDown(KeyCode.E))
        {
            if(stoneName != null)
            {
                currentNPC.DialogueID = stoneNumber;
                currentNPC.TriggerDialogue();
                stoneName = null;
            }
            else currentNPC.TriggerDialogue();

            if (isLevelKey)
            {
                currentNPC.DialogueID = levelKeyDialogue;
                currentNPC.TriggerDialogue();
            }

        }



        if (DialogueSistem.Instance != null && Input.GetMouseButtonDown(0))
        {
            DialogueSistem.Instance.DisplayNextSentence();
        }

        if (stoneNumber == 3)
        {
            Destroy(secondThreeBlock);
        }

        if (stoneNumber == 4)
        {
            Destroy(threeBlock);
        }

        if (stoneNumber == 5 && eventActivator != null)
        {
            eventActivator.SetActive(true);
            
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            NPCDialogue npc = other.GetComponent<NPCDialogue>();
            if (npc != null)
            {
                
                Canvas canvasHijo = npc.GetComponentInChildren<Canvas>(true);
                if (canvasHijo != null)
                {
                    canvasHijo.enabled = true;
                }
                
                currentNPC = npc;
                Debug.Log("Presiona E para hablar.");
            }
        }


        if (other.gameObject.CompareTag("SpecialKey"))
        {

            specialKey = true;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Stone"))
        {
            var stone = other.GetComponent<Stones>();
            if (stone != null)
            {
                GameManager.Instance.AddCollectedStone(stone.IDstone);
                GameManager.Instance.SaveCheckpoint(transform.position);
                stoneNumber = stone.IDstone + 2;
                stoneName = stone.Name;
            }

            Destroy(other.gameObject);
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            if (other.GetComponent<NPCDialogue>() == currentNPC)
            {
                Canvas canvasHijo = other.GetComponentInChildren<Canvas>(true);
                if (canvasHijo != null)
                {
                    canvasHijo.enabled = false;
                }
                
                currentNPC = null;
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("LevelKey"))
        {
            isLevelKey = true;
            Destroy(collision.gameObject);
            Debug.Log("ya tengo la llave");
            Debug.Log(isLevelKey);
        }

        if (collision.gameObject.CompareTag("Amuleto"))
        {
            Destroy(collision.gameObject);
        }
    }
}
