using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSpawnerExample : MonoBehaviour
{
    private ObjectPooler objectPooler;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    private void Update()
    {
        objectPooler.SpawnFromPool("Something", transform.position, Quaternion.identity);
    }
}
