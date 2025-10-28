using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartGame()
    {
        SceneManager.LoadScene("LVL1 INTRODUCTION");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
