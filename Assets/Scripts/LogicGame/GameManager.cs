using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int playerLives = 3;
    private Vector3 checkpointPosition;
    private List<int> collectedStoneIDs = new(); //  guarda los IDs de las piedras recogidas

    public int PlayerLives => playerLives;
    public Vector3 CheckpointPosition => checkpointPosition;
    public IReadOnlyList<int> CollectedStoneIDs => collectedStoneIDs;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoseLife(int amount)
    {
        playerLives -= amount;
        if (playerLives < 0) playerLives = 0;
    }

    public void SaveCheckpoint(Vector3 pos)
    {
        checkpointPosition = pos;
        Debug.Log($"Checkpoint guardado en {pos}");
    }

    public void AddCollectedStone(int id)
    {
        if (!collectedStoneIDs.Contains(id))
        {
            collectedStoneIDs.Add(id);
            Debug.Log($"Piedra con ID {id} recogida");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && collectedStoneIDs.Count > 0)
        {
            player.transform.position = checkpointPosition;
            Debug.Log("Jugador restaurado en checkpoint");
        }

        // Elimina las piedras recogidas
        foreach (var stone in GameObject.FindGameObjectsWithTag("Stone"))
        {
            var s = stone.GetComponent<Stones>();
            if (s != null && collectedStoneIDs.Contains(s.IDstone))
            {
                Destroy(stone);
            }
        }
    }
}
