using System.Collections.Generic;
using UnityEngine;

public class TutorialContent : MonoBehaviour
{
    private Dictionary<int, string> tutorialTextContent = new Dictionary<int, string>();
    public Dictionary<int,string> TutorialTextContent => tutorialTextContent;
    void Start()
    {
        tutorialTextContent.Add(0, "Muevete con ASDW, Orientate con el Mouse");
        tutorialTextContent.Add(1, "Salta con BARRA ESPACIADORA, cuidado con esas plantas");
        tutorialTextContent.Add(2, "Manten presionado SHIFT IZQ para correr");
        tutorialTextContent.Add(3, "Apunta con CLICK DERECHO / Dispara con CLICK IZQUIERDO");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
