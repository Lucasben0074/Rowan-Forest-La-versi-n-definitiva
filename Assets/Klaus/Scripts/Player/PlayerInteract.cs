using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private IInteractable selectedNPC;

    private void Update()
    {
        if (selectedNPC == null) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            selectedNPC.Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            selectedNPC = other.GetComponent<IInteractable>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            selectedNPC = null;
        }
    }

}
