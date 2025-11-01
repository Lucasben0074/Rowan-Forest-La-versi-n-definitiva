using UnityEngine;
using UnityEngine.Audio;

public class SuspenseMusic : MonoBehaviour
{
    public static bool IsLocked = false; 

    [Header("Snapshot al entrar")]
    [SerializeField] private AudioMixerSnapshot enterSnapshot;
    [SerializeField] private float fadeInTime = 1.5f;

    [Header("Snapshot al salir (opcional)")]
    [SerializeField] private AudioMixerSnapshot exitSnapshot;
    [SerializeField] private float fadeOutTime = 1.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (IsLocked) return;                       
        if (other.transform.root.CompareTag("Player"))
            enterSnapshot.TransitionTo(fadeInTime);
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsLocked) return;                      
        if (other.transform.root.CompareTag("Player") && exitSnapshot != null)
            exitSnapshot.TransitionTo(fadeOutTime);
    }
}
