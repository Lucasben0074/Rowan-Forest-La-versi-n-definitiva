using UnityEngine;

public class Stones : MonoBehaviour
{
    [SerializeField] private int IdStone;
    [SerializeField] private string NameStone;

    public int IDstone => IdStone;
    public string Name => NameStone;
}
