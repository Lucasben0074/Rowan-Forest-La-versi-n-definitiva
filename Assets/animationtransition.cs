using UnityEngine;

public class animationtransition : MonoBehaviour
{
    private Animator anim;
    private float timer;
    [SerializeField] private float animationduration = 2f;



    void Start()
    {
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= animationduration) anim.SetBool("idle", true);
        else anim.SetBool("idle", false);

        if (timer >= animationduration * 3) timer = 0;

        


    }
}
