using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

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
    [SerializeField] private AudioMixerSnapshot bossSnapshot;
    [SerializeField] private AudioMixerSnapshot explorationSnapshot;
    [SerializeField] private GameObject SuspenseCollider;
    private void Start()
    {

        narrativePanel.SetActive(false);
    }

    public void OnSetFinalScene()
    {
        Time.timeScale = 0;
        SuspenseMusic.IsLocked = true;

        MindNarrative.SetActive(true);
        RowanLogic.GetComponent<PlayerInteractionLVL2>().NarrativePanelActivate = true;
        Rowan.position = setPoint.position;
        bossSliderHealth.SetActive(true);
        if (bossSnapshot != null)
            bossSnapshot.TransitionTo(1.5f);

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
        if (explorationSnapshot != null)
            explorationSnapshot.TransitionTo(0f);
        Destroy(SuspenseCollider);
    }


}
