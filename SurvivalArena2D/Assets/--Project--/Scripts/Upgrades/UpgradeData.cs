using UnityEngine;

public abstract class UpgradeData : ScriptableObject
{
    public string Title;
    public string Description;
    public Sprite Icon;

    public abstract void Apply(Character character);
}
