using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialInteraction : MonoBehaviour
{
    [SerializeField] GameObject dictionaryTutorial;
    [SerializeField] GameObject tutorialPanel;
    [SerializeField] TMP_Text tutorialText;
    private int cartelNumber;


    void Start()
    {
        
        tutorialPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Cartel"))
        {
            cartelNumber = other.gameObject.GetComponent<CartelTutorial>().CartelId;
            tutorialPanel.SetActive(true);
            tutorialText.text = dictionaryTutorial.GetComponent<TutorialContent>().TutorialTextContent[cartelNumber];
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cartel"))
        {
            tutorialPanel.SetActive(false);
        }       
    }
}
