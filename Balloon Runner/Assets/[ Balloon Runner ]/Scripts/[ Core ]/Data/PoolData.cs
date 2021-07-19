using UnityEngine;

[CreateAssetMenu(menuName = "Pool Data/Create New Pool Data", fileName = "New Pool Data")]
public class PoolData : ScriptableObject
{
    // One or more objects
    public Pool[] PoolObjects;
}
