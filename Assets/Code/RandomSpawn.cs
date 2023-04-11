using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    //Place of a spawnpoint for items
    [SerializeField] private Transform[] location;

    //Items that can spawn on spawnpoints
    [SerializeField] private GameObject[] spawnObject;

    private void Awake()
    {
        for (int i = 0; i < location.Length; i++)
        {
            //choosing object to spawn by random, then instantiating it to spawnpoint
            int randomizeLocation = Random.Range(0, spawnObject.Length);
            Instantiate(spawnObject[randomizeLocation], location[i].position, Quaternion.identity);
        }
    }
}
