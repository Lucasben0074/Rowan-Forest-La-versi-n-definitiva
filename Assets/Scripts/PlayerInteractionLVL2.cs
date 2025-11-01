using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerInteractionLVL2 : MonoBehaviour
{
    [SerializeField] private GameObject Gate;
    public UnityEvent OnBossActivate;
    [SerializeField] private GameObject narrativePanel;
    [SerializeField] private GameObject mindNarrativePanel;
    private NPCDialogue currentNPC;
    private bool canAccesLvl3 = true;
    private bool narrativePanelActivate = false;
    public bool NarrativePanelActivate
    {
        get { return narrativePanelActivate; }
        set { narrativePanelActivate = value; }
        }
    public bool CanAccesLvl3
    {
        get { return canAccesLvl3; }
        set { canAccesLvl3 = value; } 
    }
    private bool moveGate = false;

    [SerializeField] private Canvas Interaction; // Canvas del NPC
    private Canvas torchInteraction;
    private Light torchLight;
    private ParticleSystem torchFlame;
    private int torchCount = 0;
    private float timer = 0;
    public void DisableNarrativePanel()
    {
        narrativePanel.SetActive(false);
        mindNarrativePanel.SetActive(false);
    }
    private void Start()
    {
        // Asegura que el cartel del NPC arranque oculto
        if (Interaction != null)
            Interaction.gameObject.SetActive(false);
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && narrativePanelActivate)
        {
            DisableNarrativePanel();
            narrativePanelActivate = false;
            Time.timeScale = 1.0f;
        }

        //  Interacción con NPC
        if (currentNPC != null && Input.GetKeyDown(KeyCode.E))
            {
             if(torchCount >= 3)
             {
                currentNPC.DialogueID = 8;
              currentNPC.TriggerDialogue();
             }
            else
            {
                currentNPC.TriggerDialogue();
            }
            }


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
                torchCount++;
                Debug.Log(torchCount);
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
        //Inicio del evento boss;
        if(torchCount == 3)
        {
            OnBossActivate.Invoke();
            torchCount++;
        }

        if (moveGate)
        {
           
            Gate.transform.Translate(Vector3.left * 7 * Time.deltaTime);
            timer += Time.deltaTime;
            if(timer > 6f)
            {
                moveGate = false;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("LVL3Door"))
        {
            if (canAccesLvl3)
            {
                Gate.GetComponent<AudioSource>().Play();
                moveGate = true;               
            }
            else
            {
                Debug.Log("Esta cerrado maestro, volve mas tarde");
            }
        }
    }
}
