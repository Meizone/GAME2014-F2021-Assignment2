using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinChanceSpawner : MonoBehaviour
{
    [SerializeField] private GameObject CoinSpawn;
    [SerializeField] private int spawnChance;

    // Start is called before the first frame update
    void Start()
    {
        if(spawnChance >= Random.Range(1,100))
        {
            Instantiate(CoinSpawn, transform.position, Quaternion.identity);
        }
    }
}
