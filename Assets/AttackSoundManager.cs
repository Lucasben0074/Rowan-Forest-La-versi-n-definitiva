using UnityEngine;

public class AttackSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource m_SoundSource;
    [SerializeField] private AudioClip Amulet;
    [SerializeField] private AudioClip stones;
    [SerializeField] private AudioClip bulletPU;

    private AreaFear areaFear;
    private CharacterShooting characterShooting;
    void Start()
    {
        
        areaFear = GetComponent<AreaFear>();
        characterShooting = GetComponent<CharacterShooting>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && areaFear != null)
        {
            if(areaFear.Canfear)
                m_SoundSource.PlayOneShot(Amulet);
        }

        if (Input.GetMouseButtonDown(0) && characterShooting.IsAiming && characterShooting.Canshoot)
        {
            m_SoundSource.PlayOneShot(stones);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BulletPowerUp"))
        {
            
            m_SoundSource.PlayOneShot(bulletPU);
        }
    }

}
