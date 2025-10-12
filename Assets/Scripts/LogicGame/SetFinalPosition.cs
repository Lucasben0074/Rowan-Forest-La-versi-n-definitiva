using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SetFinalPosition : MonoBehaviour
{
    [SerializeField] private GameObject player, boss, NPC;
    [SerializeField] private Transform playerFinalPosition, NPCFinalPosition;
    [SerializeField] private Transform BossInstantiatePosition;
    [SerializeField] private GameObject enemies;
    [SerializeField] private GameObject bossSlider;
    void Start()
    {
        enemies.SetActive(false);
    }
    public void SetNPC()
    {
        NPC.transform.position = NPCFinalPosition.position;
    }
    public void SetBoss()
    {
        Instantiate(boss,BossInstantiatePosition.position,Quaternion.identity);
        bossSlider.SetActive(true);
    }
    public void SetRowan()
    {
        player.transform.position = playerFinalPosition.position;
    }
    public void SetEnemies()
    {
        enemies.SetActive(true);
    }
    void Update()
    {
        
    }
}
