using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private float maxHealth = 100;
    private float health;
    private float movementSpeed = 5;
    private float sprintSpeed = 7.5f;
    private float jumpForce = 15;

    public float MaxHealth => maxHealth;
    public float Health { get { return health; } set { health = value; } }
    public float MovementSpeed { get { return movementSpeed; } set { movementSpeed = value; } }
    public float SprintSpeed => sprintSpeed;
    public float JumpForce => jumpForce;

    private void Awake()
    {
        health = maxHealth;
    }
}
