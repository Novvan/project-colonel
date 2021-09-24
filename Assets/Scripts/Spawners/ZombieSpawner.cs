using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ZombieSpawner : Spawner
{
    private float currentSpawnTimer;
    private float randomTimer;
    private void Start()
    {
        randomTimer = Random.Range(30, 50);
    }
    private void Update()
    {
        if (currentSpawnTimer < randomTimer)
        {
            currentSpawnTimer += Time.deltaTime;
        }
        else 
        {
            Spawn();
            randomTimer = Random.Range(20, 50);
            currentSpawnTimer = 0;
        }
        
    }
    public override void Spawn()
    {
        base.Spawn();
        gm.ZombieCount += 1;
    }
}
