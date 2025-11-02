using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PausedFunction : MonoBehaviour
{

    [SerializeField] AudioMixer mainMixer;
    [SerializeField] string nameParameter = "mainMixerVolume";

    [Header("Panel y Botones")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

    private Button[] buttons;
    private int selectedIndex = 0;
    private bool isPaused = false;

    void Start()
    {
        pausePanel.SetActive(false);
        buttons = new Button[] { continueButton, restartButton, quitButton };
        HighlightButton(selectedIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isPaused)
            {
                Paused();
                mainMixer.SetFloat(nameParameter, -8f);
            }                
            else
            {
                NotPaused();
                mainMixer.SetFloat(nameParameter, 2f);
            }
                
        }

        if (!isPaused) return;


        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            ChangeSelection(1);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            ChangeSelection(-1);
        }


        if (Input.GetKeyDown(KeyCode.Return))
        {
            ActivateSelected();
        }
    }

    private void ChangeSelection(int direction)
    {
        buttons[selectedIndex].image.color = Color.white;
        selectedIndex = (selectedIndex + direction + buttons.Length) % buttons.Length;
        HighlightButton(selectedIndex);
    }

    private void HighlightButton(int index)
    {

        buttons[index].image.color = Color.red;
    }

    private void ActivateSelected()
    {
        if (buttons[selectedIndex] == continueButton)
        {
            NotPaused();
        }
        else if (buttons[selectedIndex] == restartButton)
        {
            Time.timeScale = 1f;
            Scene current = SceneManager.GetActiveScene();
            SceneManager.LoadScene(current.name);
        }
        else if (buttons[selectedIndex] == quitButton)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("VentanaInicio");
        }
    }

    public void Paused()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        selectedIndex = 0;
        HighlightButton(selectedIndex);
    }

    public void NotPaused()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}

