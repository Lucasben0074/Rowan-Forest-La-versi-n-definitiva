using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class TakeAmulet : MonoBehaviour
{
    [SerializeField] private Image panelNarrativo;
    [SerializeField] private Sprite imagenAmuleto;
    private bool isPanelActivated= false;
    private float timer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPanelActivated)
        {
            timer += Time.deltaTime;
            if(timer >= 5f)
            {
                panelNarrativo.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Amuleto"))
        {
            panelNarrativo.sprite = imagenAmuleto;
            panelNarrativo.gameObject.GetComponent<Button>().enabled = false;
            panelNarrativo.gameObject.SetActive(true);
            
            isPanelActivated = true;
        }
    }
}
