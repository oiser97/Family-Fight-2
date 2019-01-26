
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : 
    MonoBehaviour
{

    public GameObject[] spawnPool;

    // Start is called before the first frame update.

    void Start()
    {
        Spawn();
    }

    // Update is called once per frame.

    void Update()
    {
        
    }

    private void Spawn()
    {
        int index = Random.Range(0, spawnPool.Length);
        Instantiate(spawnPool[index], transform.position, transform.rotation);
    }

}
