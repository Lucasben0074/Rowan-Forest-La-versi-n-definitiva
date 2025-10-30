using UnityEngine;
using UnityEngine.Audio;

public class LaberynthSanp : MonoBehaviour
{
    [SerializeField] private AudioMixerSnapshot laberynthSnapshot;
    [SerializeField] private AudioMixerSnapshot explorationSnapshot;
    [SerializeField] private float transitionTime = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.transform.root.CompareTag("Player"))
            laberynthSnapshot.TransitionTo(transitionTime);
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.transform.root.CompareTag("Player") && explorationSnapshot != null)
            explorationSnapshot.TransitionTo(transitionTime);
    }

}
