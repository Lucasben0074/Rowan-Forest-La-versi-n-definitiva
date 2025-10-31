using JetBrains.Annotations;
using UnityEngine;

public class AttackSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource m_SoundSource;
    [SerializeField] private AudioClip Amulet;
    [SerializeField] private AudioClip stones;
    [SerializeField] private AudioClip bulletPU;

    private AreaFear areaFear;
    private CharacterShooting characterShooting;
    public void PlayShootSound()
    {
        if (stones != null && m_SoundSource != null)
            m_SoundSource.PlayOneShot(stones);
    }
    public void PlayMagicSound()
    {
        if(Amulet != null && m_SoundSource != null) m_SoundSource.PlayOneShot(Amulet);
    }
    void Start()
    {    
        areaFear = GetComponent<AreaFear>();
        characterShooting = GetComponent<CharacterShooting>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BulletPowerUp"))
        {
            
            m_SoundSource.PlayOneShot(bulletPU);
        }
    }

}
