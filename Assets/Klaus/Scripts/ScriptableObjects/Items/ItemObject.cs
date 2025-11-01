using UnityEngine;

public enum ItemType
{
    Default,
    Potion,
    Stone
}
public abstract class ItemObject : ScriptableObject
{
    public ItemType type;
    [TextArea(15, 15)]
    public string description;
}
