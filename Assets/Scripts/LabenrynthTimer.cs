using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LabenrynthTimer : MonoBehaviour
{
    [SerializeField] private GameObject timerImage;
    [SerializeField] private TMP_Text restTime;
    private float timer;
    private float limitTimer = 120f;
    private bool isInLaberynth = false;
    private bool timeOver = false;
    public bool TimeOver => timeOver;
    private void TimerActivate()
    {
        timerImage.SetActive(true);

    }

    void Start()
    {
        
        timerImage.SetActive(false);
    }


    void Update()
    {
        if (isInLaberynth)
        {
            TimerActivate();
            timer += Time.deltaTime;
            restTime.text = Mathf.Ceil(limitTimer - timer).ToString("0"); 

            if(limitTimer - timer <= 0)
            {
                timeOver = true;
            }
            

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LabCollider"))
        {
            isInLaberynth = true;
            Destroy(other.gameObject);
        }
    }
}
