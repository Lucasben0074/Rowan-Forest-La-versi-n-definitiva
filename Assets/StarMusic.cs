using UnityEngine;
using UnityEngine.Audio;

public class StartMusic : MonoBehaviour
{
    [SerializeField] private AudioMixerSnapshot startSnapshot;

    private void Start()
    {
        startSnapshot.TransitionTo(0f);
    }
}
