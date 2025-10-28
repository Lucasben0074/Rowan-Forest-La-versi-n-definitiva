using UnityEngine;
using UnityEngine.UI;

public class EventFunctionLVL2 : MonoBehaviour
{
    [SerializeField] private GameObject MindNarrative;
    [SerializeField] private GameObject narrativePanel;
    [SerializeField] private Transform returnPoint;
    [SerializeField] private Transform setPoint;
    [SerializeField] private Transform Rowan;
    [SerializeField] private GameObject bossSliderHealth;
    [SerializeField] private GameObject LVL3Gate;
    [SerializeField] private GameObject RowanLogic;

    private void Start()
    {

        narrativePanel.SetActive(false);
    }

    public void OnSetFinalScene()
    {
        Time.timeScale = 0;
        MindNarrative.SetActive(true);
        RowanLogic.GetComponent<PlayerInteractionLVL2>().NarrativePanelActivate = true;
        Rowan.position = setPoint.position;
        bossSliderHealth.SetActive(true);
    }

    public void OnBossDestroyed()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        narrativePanel.SetActive(true);
        RowanLogic.GetComponent<PlayerInteractionLVL2>().NarrativePanelActivate = true;
        Rowan.position = returnPoint.position;
        RowanLogic.GetComponent<PlayerInteractionLVL2>().CanAccesLvl3 = true;
        Debug.Log(RowanLogic.GetComponent<PlayerInteractionLVL2>().CanAccesLvl3);
        bossSliderHealth.SetActive(false);
        LVL3Gate.GetComponent<Renderer>().material.color = Color.green;
        
    }


}
