
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

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        bossAttacks.Add(1, "PumpkinsShoot");
        bossAttacks.Add(2, "TreeFall");
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2f)
        {
            animator.SetTrigger("attack");
            Invoke(bossAttacks[Random.Range(1, 3)], 0.01f);
            timer = 0f;
        }
    }

    public void PumpkinsShoot()
    {
        for (int i = 0; i < 5; i++)
        {
            Transform pos = instancePositions[i];

            
            GameObject frontPumpkin = Instantiate(pumpkins, pos.position, pos.rotation);
            pumpkinsList.Add(frontPumpkin);

            
            Quaternion backRotation = pos.rotation * Quaternion.Euler(0, 180f, 0);
            GameObject backPumpkin = Instantiate(pumpkins, pos.position, backRotation);
            pumpkinsList.Add(backPumpkin);
        }
    }

    public void TreeFall()
    {
        for (int i = 0; i < 5; i++)
        {
            Transform randomPos = instanceTreePosition[Random.Range(0, instanceTreePosition.Count)];
            Instantiate(tree, randomPos.position, Quaternion.identity);
        }
    }
}
