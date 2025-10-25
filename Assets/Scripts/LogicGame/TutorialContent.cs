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
        tutorialTextContent.Add(4, "Presiona F para utilizar el poder del amuleto, esto hara que las sombras retrocedan, ya que no pueden ser dañadas. Maneja el Mana y ten cuidado, cada vez que lo uses tendras que esperar un tiempo para volver a usarlo");
        tutorialTextContent.Add(5, "Potenciadores! Comer una manzana te hara recuperar Salud, y Pasar por las municiones te hara extender la distancia hasta donde puedes disparar, pero solo por unos segundos");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
