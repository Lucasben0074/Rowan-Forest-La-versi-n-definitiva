using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private bool canNextLevel = false;
    private bool playerContact = false;
    private void Update()
    {
        if(playerContact && canNextLevel)
        {
            SceneManager.LoadScene("Level2");
        }
        else if( playerContact && !canNextLevel)
        {
            Debug.Log("te falta la llave maestro.");
        }
        

       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerContact = true;
            canNextLevel = collision.gameObject.GetComponent<PlayerInteraction>().IsLevelKey;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerContact = false;  
        }
    }
}
