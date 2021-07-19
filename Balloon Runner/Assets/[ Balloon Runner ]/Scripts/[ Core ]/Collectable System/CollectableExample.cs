
public class CollectableExample : CollectableBase
{
    public override void Collect()
    {
        base.Collect();

        // Use when picked up.
        Use();
    }

    public override void Use()
    {
        // What does this collectable do

        // Dispose it after using.
        Dispose();
    }
}
