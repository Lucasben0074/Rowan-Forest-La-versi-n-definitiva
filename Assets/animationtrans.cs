using UnityEngine;

public class animationtrans : MonoBehaviour
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
        if (timer >= animationduration) anim.SetBool("coso1", true);
        else anim.SetBool("coso1", false);

        if (timer >= animationduration * 3) timer = 0;



    }
}
