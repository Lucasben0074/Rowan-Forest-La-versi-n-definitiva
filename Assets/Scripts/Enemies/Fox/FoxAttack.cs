using UnityEngine;

public class FoxAttack : MonoBehaviour, IDamageMaker
{
    [SerializeField] private float foxDamage = 10f;
    [SerializeField] private float attackCooldown = 1f;

    private bool canAttack;
    private bool isAttacking;
    private Animator anim;
    private FoxSoundManager foxSound;
    private float attackTimer;

    public bool CanAttack
    {
        get { return canAttack; }
        set { canAttack = value; }
    }

    public float MakeDamage() => foxDamage;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        foxSound = GetComponent<FoxSoundManager>();
    }

    private void Update()
    {
        attackTimer -= Time.deltaTime;

        if (canAttack && attackTimer <= 0f)
        {
            Attack();
        }
        else if (!canAttack)
        {
            anim.SetBool("isAttacking", false);
            isAttacking = false;
        }
    }

    private void Attack()
    {
        anim.SetBool("isAttacking", true);
        attackTimer = attackCooldown;
        isAttacking = true;

        if (foxSound != null)
        {
            Debug.Log(" Reproduciendo ataque");
            foxSound.PlayAttack();
        }

        Debug.Log(" El zorro ataca");
    }
}
