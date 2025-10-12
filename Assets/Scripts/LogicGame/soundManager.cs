using UnityEngine;

public class soundManager : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip ambientalMusic;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = ambientalMusic;
        audioSource.loop = true; 
        audioSource.playOnAwake = false;
        audioSource.Play();
    }
}
