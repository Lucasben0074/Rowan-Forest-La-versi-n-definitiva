using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalScene : MonoBehaviour
{
    [SerializeField] private Image panel1;
    [SerializeField] private Image panel2;
    [SerializeField] private Image panel3;

    [Header("Configuración de transición")]
    [SerializeField] private float fadeDuration = 2f;       // Duración del fade
    [SerializeField] private float waitBetweenFades = 1f;   // Pausa entre cambios
    [SerializeField] private float initialWaitTime = 2f;    // Tiempo que se mantiene visible el panel1 antes de iniciar

    private int currentPhase = -1; // -1 = espera inicial
    private float timer = 0f;
    private bool isWaiting = false;

    private void Start()
    {
        // Inicializamos las opacidades
        SetAlpha(panel1, 1f);
        SetAlpha(panel2, 0f);
        SetAlpha(panel3, 0f);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Creditos");
        }

        switch (currentPhase)
        {
            case -1: // Espera inicial con panel1 visible
                timer += Time.deltaTime;
                if (timer >= initialWaitTime)
                {
                    timer = 0f;
                    currentPhase = 0; // Ahora empieza el primer fade
                }
                break;

            case 0: // panel1 -> panel2
                FadeBetween(panel1, panel2);
                break;

            case 1: // Espera antes de siguiente transición
                WaitPhase();
                break;

            case 2: // panel2 -> panel3
                FadeBetween(panel2, panel3);
                break;

            case 3: // Espera final (opcional)
                WaitPhase();
                break;

            default:
                // Fin de la secuencia: podés cargar la escena, mostrar texto, etc.
                break;
        }
    }

    private void FadeBetween(Image from, Image to)
    {
        timer += Time.deltaTime;
        float t = timer / fadeDuration;

        SetAlpha(from, Mathf.Lerp(1f, 0f, t));
        SetAlpha(to, Mathf.Lerp(0f, 1f, t));

        if (t >= 1f)
        {
            SetAlpha(from, 0f);
            SetAlpha(to, 1f);
            timer = 0f;
            isWaiting = true;
            currentPhase++;
        }
    }

    private void WaitPhase()
    {
        if (!isWaiting) return;

        timer += Time.deltaTime;
        if (timer >= waitBetweenFades)
        {
            timer = 0f;
            isWaiting = false;
            currentPhase++;
        }
    }

    private void SetAlpha(Image img, float alpha)
    {
        if (img == null) return;
        Color c = img.color;
        c.a = alpha;
        img.color = c;
    }
}
