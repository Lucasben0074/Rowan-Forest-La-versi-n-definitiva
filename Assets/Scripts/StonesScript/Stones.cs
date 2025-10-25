using UnityEngine;

public class Stones : MonoBehaviour
{
    [SerializeField] private int idstone;
    public int IDstone => idstone;

    [SerializeField] private string stoneName;
    public string Name => stoneName;
}