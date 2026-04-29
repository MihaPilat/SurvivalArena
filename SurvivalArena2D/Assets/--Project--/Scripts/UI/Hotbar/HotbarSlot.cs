public class HotbarSlot
{
    public IWeapon Weapon { get; set; }
    public bool IsEmpty => Weapon == null;

    public void Clear() => Weapon = null;
}