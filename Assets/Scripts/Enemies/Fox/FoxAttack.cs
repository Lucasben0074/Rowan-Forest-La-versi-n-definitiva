using UnityEngine;

public class FoxAttack : MonoBehaviour, IDamageMaker
{
    [SerializeField] private float foxDamage;
    private bool canAttack;
    private Animator anim;
    public bool CanAttack
    {
        get { return canAttack; }
        set { canAttack = value; }
    }

    public float MakeDamage()
    {
        return foxDamage;
    }
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if (canAttack)
        {
            anim.SetBool("isAttacking",true);
            Debug.Log("El zorro esta atacando");
        }
        else
        {
            anim.SetBool("isAttacking", false);
          
        }
            
        
    }



}
