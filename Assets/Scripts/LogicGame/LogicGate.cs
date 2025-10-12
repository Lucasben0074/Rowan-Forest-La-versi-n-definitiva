using TMPro;
using UnityEngine;

public class LogicGate : MonoBehaviour
{
    [SerializeField] private GameObject leftDoor;
    [SerializeField] private GameObject rightDoor;
    [SerializeField] private float OpenVelocity;
    [SerializeField] private float distance;
    [SerializeField] private GameObject panel;
    Vector3 leftPosition;
    Vector3 rightPosition;
    private bool openDoor = false;
    private bool gotKey = false;
    private float timer = 0;
    TMP_Text tMP_Text;

    public void OpenDoor()
    {
        Debug.Log("Entro al OpenDoor()");
        if (Vector3.Distance(leftPosition, leftDoor.transform.position) < distance && Vector3.Distance(rightPosition, rightDoor.transform.position) < distance)
        {
            leftDoor.transform.Translate(Vector3.right * OpenVelocity*Time.deltaTime);
            rightDoor.transform.Translate(Vector3.left * OpenVelocity*Time.deltaTime);
            Debug.Log("Entro al if del openDoor");
        }
    }
    void Start()
    {
        leftPosition = leftDoor.transform.position;
        rightPosition = rightDoor.transform.position;
        tMP_Text = panel.GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (openDoor && gotKey)
        {
            OpenDoor();
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else if(openDoor && !gotKey)
        {
            timer += Time.deltaTime;
            tMP_Text.text = "PUERTA CERRADA.  Para abrir se necesita una llave especial";
            panel.SetActive(true);
            if(timer > 6f)
            {
                panel.SetActive(false);
                timer = 0;
                openDoor = false;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            gotKey = collision.gameObject.GetComponent<PlayerInteraction>().SpecialKey;
            openDoor = true;
            
            
        }
    }

}
