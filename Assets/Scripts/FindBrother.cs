using UnityEngine;
using UnityEngine.SceneManagement;

public class FindBrother : MonoBehaviour
{
    private bool findBrother = false;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (findBrother)
        {
            SceneManager.LoadScene("Final");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Brother"))
        {
            findBrother=true;
        }
    }
}
