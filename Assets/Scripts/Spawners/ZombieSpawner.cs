using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ZombieSpawner : Spawner
{
    private float _currentSpawnTimer;
    private float _randomTimer;
    private void Start()
    {
        _randomTimer = 3;
    }
    private void Update()
    {
        if (_currentSpawnTimer < _randomTimer)
        {
            _currentSpawnTimer += Time.deltaTime;
        }
        else
        {
            Spawn();
            
            _currentSpawnTimer = 0;
        }

    }
    public override void Spawn()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (gm.ZombiesAlive < gm.MaxZombiesAlive)
            {
                base.Spawn();
                gm.ZombiesAlive++;
            }
        }
    }
}
