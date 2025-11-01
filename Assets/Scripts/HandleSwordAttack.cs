using GLTFast.Schema;
using UnityEngine;

public class HandleSwordAttack : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameObject.GetComponent<CharacterShooting>().IsAiming)
        {
            animator.SetTrigger("swordAttack");
        }
    }
}
