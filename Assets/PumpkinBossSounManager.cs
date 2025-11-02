using UnityEngine;

public class PumpkinBossSounManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private float laughtCooldown = 4f;

    [SerializeField] AudioClip laught, hurt, death;
    float timer;
    private bool bossDead = false;
    private BossLifeControler bossLife;

    private float maxHealth;
    private float nextThreshold;
    private void Start()
    {
        bossLife = GetComponent<BossLifeControler>();
        maxHealth = bossLife.Health;
        nextThreshold = maxHealth * 0.75f;
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= laughtCooldown)
        {
            _audio.PlayOneShot(laught);
            timer = 0;
        }

        if (!bossDead && bossLife.Health <= nextThreshold && bossLife.Health > 0)
        {
            _audio.PlayOneShot(hurt);

            
            nextThreshold -= maxHealth * 0.25f;
        }

        if (bossLife.Health <= 0 && !bossDead)
        {
            _audio.PlayOneShot(death);
            bossDead = true;
        }
    }

}
