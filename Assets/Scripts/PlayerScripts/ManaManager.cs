using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour
{
    [SerializeField] private Slider manaSlider;
    [SerializeField] private float startMana;
    private float currentMana;
    public float CurrentMana
    {
        get { return currentMana; }
        set { currentMana = value;}            
    }



    void Start()
    {
        currentMana = startMana;
    }

    
    void Update()
    {

        if(currentMana < startMana)
        {
            currentMana += Time.deltaTime * 4;
        }
        
        manaSlider.value = currentMana/startMana;

    }
}
