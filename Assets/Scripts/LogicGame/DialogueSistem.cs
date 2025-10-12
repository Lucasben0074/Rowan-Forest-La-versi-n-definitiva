using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueSistem : MonoBehaviour
{
    public static DialogueSistem Instance;

    [Header("UI Elements")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text speakerName;

    private Queue<string> sentences; // Cola para almacenar frases
    private string currentSpeaker;

    // Diccionario de di�logos por ID
    private Dictionary<int, Dialogue> dialogues = new Dictionary<int, Dialogue>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        sentences = new Queue<string>();
        dialoguePanel.SetActive(false);

        // Ac� registramos los di�logos
        RegisterDialogues();
    }

    void RegisterDialogues()
    {
        // ID 1 = di�logo del NPC1
        dialogues.Add(1, new Dialogue(
            new string[]
            {
                "Hola, eres nueva por aqu�... Nunca te hab�a visto.",
                "Hola, mi nombre es Rowan. Estoy buscando a mi hermano �Lo has visto?",
                "No he visto otro humano por aqu�, pero NPC2 podr�a ayudarte. Es muy bueno recordando rostros.",
                "�En serio?, �D�nde puedo encontrarlo?",
                "Debes seguir el camino y lo hallar�s en el c�rculo de las runas.",
                "�Gracias!",
                "(Advertencia) �Ten cuidado!, el bosque esconde muchos peligros, no bajes la guardia."
            },
            new string[]
            {
                "NPC 1",
                "Rowan",
                "NPC 1",
                "Rowan",
                "NPC 1",
                "Rowan",
                "NPC 1"
            }
        ));
        dialogues.Add(2, new Dialogue(
        new string[]
        {
            "Gracias por salvarme peque�a pero no podr� quedarme a charlar contigo.",
            "Estos maleantes se llevaron mis runas y ahora no podr� salir del bosque.",
            "Qu� pena... Yo podr�a ayudarte a recuperarlas pero tambi�n necesito tu ayuda.",
            "�Qu� valiente! Haciendo tratos con un desconocido. Muy bien ni�a deber�s revelar los secretos de este bosque para encontrarlas.",
            "Las flores azules se inclinan hacia lo que guardan. Sigue su mirada y lo encontrar�s.",
            "[PRIMER DESAF�O]"
        },
        new string[]
        {
            "NPC 2",
            "NPC 2",
            "Rowan",
            "NPC 2",
            "NPC 2",
            "NPC 2"
        }
        ));
        dialogues.Add(3, new Dialogue(
            new string[]
            {
                " �Lo conseguiste!. Estoy impresionado para alguien de tu tama�o.",
                "Bien pero eso no es todo.\r\n",
                "�Entre tres guardianes de madera ca�dos hallar�s el fruto que no crece en rama\r\nalguna, cuidado con las termitas.\r\n� ",

            },
            new string[]
            {
                "NPC2",
                "NPC2",
                "NPC2"
            }
            ));
        dialogues.Add(4, new Dialogue(
            new string[]
            {
                "�Bravo!. Estamos cada vez mas cerca.",
                " :D *feliz*\r\n",
                "�No siempre lo nuevo abre caminos; a veces, el regreso trae la salida.\r\nVuelve sobre tus huellas y hallar�s lo que antes estaba sellado.\r\n�\r\n",

            },
            new string[]
            {
                "NPC2",
                "Rowan",
                "NPC2",
            }));
        dialogues.Add(5, new Dialogue(
            new string[]
            {
                "Lo lograste!. Derrotaste a la gran calabaza.  Ahora... �en que necesitas ayuda?\r\n",
                "Nunca vi un ser tan diminuto peliando asi",
                " No fue facil.. pero �que abre esta llave?\r\n",
                 "necesito encontrar a mi hermano\r\n",
                 "Lo que persigues est� al otro lado. Tu hermano aguarda� aunque ya no sea el mismo que recuerdas",

            },
            new string[]
            {
                "NPC2",
                "NPC2",
                "Rowan",
                "Rowan",
                "NPC2",
            }
            ));
    
    }

    public void StartDialogue(int dialogueID)
    {
        if (!dialogues.ContainsKey(dialogueID))
        {
            Debug.LogWarning("No existe el di�logo con ID " + dialogueID);
            return;
        }

        dialoguePanel.SetActive(true);
        sentences.Clear();

        Dialogue dialogue = dialogues[dialogueID];
        for (int i = 0; i < dialogue.sentences.Length; i++)
        {
            sentences.Enqueue(dialogue.speakers[i] + "|" + dialogue.sentences[i]);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string rawSentence = sentences.Dequeue();
        string[] parts = rawSentence.Split('|');
        currentSpeaker = parts[0];
        string sentence = parts[1];

        speakerName.text = currentSpeaker;
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        Debug.Log("Fin del di�logo.");
    }
}

// Clase simple para almacenar di�logos
[System.Serializable]
public class Dialogue
{
    public string[] sentences;
    public string[] speakers;

    public Dialogue(string[] s, string[] sp)
    {
        sentences = s;
        speakers = sp;
    }
}
