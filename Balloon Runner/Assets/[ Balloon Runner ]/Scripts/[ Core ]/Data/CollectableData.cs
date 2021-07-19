using UnityEngine;

[CreateAssetMenu(menuName = "Collectable Data/Create New Collectable Data", fileName = "New Collectable Data")]
public class CollectableData : ScriptableObject
{
    // You can seperate negative - positive collectables if you want.
    public Collectable[] CollectableObjects;
    public Collectable[] PositiveEffectCollectables;
    public Collectable[] NegativeEffectCollectables;
}
