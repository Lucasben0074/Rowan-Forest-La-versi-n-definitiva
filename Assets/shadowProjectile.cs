using UnityEngine;

public class shadowProjectile : MonoBehaviour, IDamageMaker
{
    [SerializeField] private float shadowProjetileDamage = 15f;

    public float MakeDamage()
    {
        return shadowProjetileDamage;
    }
}
