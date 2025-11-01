using GLTFast.Schema;
using UnityEngine;
using UnityEngine.Audio;

public class EnviromentSound : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private string parameterName = "WindVolume";
    [SerializeField] private float oscilationSpeed = 1f;
    [SerializeField] private float minVolumeDb = -30f;
    [SerializeField] private float maxVolumeDb = 0f;

    void Start()
    {
        float value = Mathf.Lerp(minVolumeDb, maxVolumeDb, (Mathf.Sin(Time.time * oscilationSpeed) + 1f) / 2f);
        mixer.SetFloat(parameterName, value);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
