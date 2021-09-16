using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject zombieSpawner;
    [SerializeField] private float secondsToStart = 2;
    [SerializeField] private int numberOfPlayers;

    private float spawnCooldown = 3;
    private float currentSpawnCooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        zombieSpawner.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSpawnCooldown < spawnCooldown)
        {
            currentSpawnCooldown += Time.deltaTime;
        }
        else 
        {
            zombieSpawner.GetComponent<ZombieSpawner>().Spawn();
            currentSpawnCooldown = 0;
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            var playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            if (playerCount >= numberOfPlayers)
            {
                //start game
                StartCoroutine(WaitToStart());
                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.CurrentRoom.IsVisible = false;
            }
        }
    }
    IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(secondsToStart);
        photonView.RPC("StartGame", RpcTarget.All);
    }
    [PunRPC]
    void StartGame()
    {
        zombieSpawner.SetActive(true);
    }
}
