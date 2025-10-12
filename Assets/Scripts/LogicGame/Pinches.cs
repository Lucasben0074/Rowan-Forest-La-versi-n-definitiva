using UnityEngine;

public class Pinches : MonoBehaviour,IDamageMaker
{
    [SerializeField] private float pinchesDamage;
    public float MakeDamage()
    {
        return pinchesDamage;
    }
}
