using UnityEngine;

public class GameManager : MonoBehaviour
{

    
    public static GameManager Instance { get; private set; }

    private int playerLives = 3;
    public int PlayerLives
    {
        get { return playerLives; }
        set { playerLives = value; }
    }

    //[SerializeField] GameObject eventLoreImage;

    //public void OnEventLore()
    //{
    //    eventLoreImage.SetActive(true);

    //}
    //public void OnContinueEventLore()
    //{
    //    Debug.Log("Click detectado en OnContinueEventLore");
    //    eventLoreImage.SetActive(false);
    //}
    public void LoseLifes(int live)
    {
        playerLives -= live;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        
    }
}
