using UnityEngine;

public class PlayerInteractionLVL2 : MonoBehaviour
{
    private NPCDialogue currentNPC;

    [SerializeField] private Canvas Interaction; // Canvas del NPC
    private Canvas torchInteraction;
    private Light torchLight;
    private ParticleSystem torchFlame;

    private void Start()
    {
        // Asegura que el cartel del NPC arranque oculto
        if (Interaction != null)
            Interaction.gameObject.SetActive(false);
    }

    private void Update()
    {
        //  Interacción con NPC
        if (currentNPC != null && Input.GetKeyDown(KeyCode.E))
            currentNPC.TriggerDialogue();

        if (DialogueSistem.Instance != null && Input.GetMouseButtonDown(0))
            DialogueSistem.Instance.DisplayNextSentence();

        //  Encender antorcha si estamos cerca y presionamos E
        if (torchInteraction != null && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Intentando encender antorcha...");
            torchInteraction.enabled = false;

            if (torchLight != null)
            {
                torchLight.enabled = true;
                Debug.Log(" Luz encendida");
            }
            else
            {
                Debug.LogWarning(" No encontró la luz en la antorcha");
            }

            if (torchFlame != null)
            {
                torchFlame.Play();
                Debug.Log(" Fuego encendido");
            }
            else
            {
                Debug.LogWarning(" No encontró el ParticleSystem en la antorcha");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //  Detección de NPC
        if (other.CompareTag("NPC"))
        {
            var npc = other.GetComponent<NPCDialogue>();
            if (npc != null)
            {
                Interaction.gameObject.SetActive(true);
                currentNPC = npc;
                Debug.Log("Presiona E para hablar con NPC");
            }
        }

        //  Detección de antorcha
        if (other.CompareTag("Torch"))
        {
            Debug.Log("Entró en rango de antorcha (buscando hijos por nombre)");

            // Busca los hijos específicos por nombre exacto
            Transform lightChild = other.transform.Find("Point Light");
            Transform fireChild = other.transform.Find("Particle System");
            Transform canvasChild = other.transform.Find("TorchInteractHud");

            if (lightChild != null)
                torchLight = lightChild.GetComponent<Light>();
            else
                Debug.LogWarning("No se encontró el hijo 'Point Light'");

            if (fireChild != null)
                torchFlame = fireChild.GetComponent<ParticleSystem>();
            else
                Debug.LogWarning("No se encontró el hijo 'Particle System'");

            if (canvasChild != null)
            {
                torchInteraction = canvasChild.GetComponent<Canvas>();
                torchInteraction.enabled = true;
            }
            else
            {
                Debug.LogWarning("No se encontró el hijo 'Canvas'");
            }

            // Aseguramos que empiece apagada (por si estaban encendidas)
            if (torchLight != null) torchLight.enabled = false;
            if (torchFlame != null) torchFlame.Stop();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Salir del rango del NPC
        if (other.CompareTag("NPC"))
        {
            if (other.GetComponent<NPCDialogue>() == currentNPC)
            {
                Interaction.gameObject.SetActive(false);
                currentNPC = null;
            }
        }

        // Salir del rango de la antorcha
        if (other.CompareTag("Torch"))
        {
            if (torchInteraction != null)
                torchInteraction.enabled = false;

            torchInteraction = null;
            torchLight = null;
            torchFlame = null;

            Debug.Log("Salió del rango de la antorcha");
        }
    }
}
