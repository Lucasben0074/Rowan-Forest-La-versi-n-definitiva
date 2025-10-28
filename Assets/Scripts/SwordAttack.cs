using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float swordDamage = 35;
    public float SwordDamage => swordDamage;
    
}
