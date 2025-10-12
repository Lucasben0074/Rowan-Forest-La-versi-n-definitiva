
using UnityEngine;
using System.Collections.Generic;

public class BossFinal : MonoBehaviour
{
    [SerializeField] private GameObject pumpkins;
    [SerializeField] private GameObject tree;
    [SerializeField] private List<Transform> instancePositions = new List<Transform>();
    [SerializeField] private List<Transform> instanceTreePosition = new List<Transform>();
    private float timer = 0f;
    private List<GameObject> pumpkinsList = new List<GameObject>();
    private Dictionary<int, string> bossAttacks = new Dictionary<int, string>();
    private Animator animator;
    public void PumpkinsShoot()
    {
        
        for (int i = 0; i < 5; i++)
        {
            GameObject newPumpkin = Instantiate(pumpkins, instancePositions[i].position ,Quaternion.identity);
            pumpkinsList.Add(newPumpkin);
        }

    }

    public void TreeFall()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject newTree = Instantiate(tree, instanceTreePosition[Random.Range(1, instanceTreePosition.Count)].position, Quaternion.identity);
        }
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        bossAttacks.Add(1, "PumpkinsShoot");
        bossAttacks.Add(2, "TreeFall");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 4f)
        {
            animator.SetTrigger("attack");
            Invoke(bossAttacks[Random.Range(1, 3)],0.01f);
            timer = 0f;
        }
        
    }
}
