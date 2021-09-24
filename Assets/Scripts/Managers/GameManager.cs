using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private List<GameObject> zombieSpawners;
    [SerializeField] private float secondsToStart = 2;
    [SerializeField] private int numberOfPlayers;
    [SerializeField] private int maxZombieCount;

    private float spawnCooldown = 3;
    private float currentSpawnCooldown = 0;
    private int zombieCount;
    private bool killZombies;
    private bool _hasWinner;
    public int ZombieCount { get => zombieCount; set => zombieCount = value; }
    public bool KillZombies { get => killZombies; set => killZombies = value; }

    public GameObject winnerScreen;
    public GameObject loserScreen;

    void Start()
    {
       /*foreach (GameObject spawner in zombieSpawners)
        {
            spawner.SetActive(false);
        }*/
    }

    void Update()
    {
        if (zombieCount >= maxZombieCount && !_hasWinner) 
        {
            _hasWinner = true;
            foreach (GameObject spawner in zombieSpawners)
            {
                spawner.SetActive(false);
            }
            killZombies = true;
            float maxKillCount = 0;
            PhotonView currentWinner = null;
            foreach (PhotonView player in PhotonNetwork.PhotonViews) 
            {
                if (player.gameObject.GetComponent<Character>().Killcount >= maxKillCount) 
                {
                    currentWinner = player;
                    maxKillCount = player.gameObject.GetComponent<Character>().Killcount;
                }
            }
            photonView.RPC("Win", RpcTarget.All, currentWinner);
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
        foreach (GameObject spawner in zombieSpawners)
        {
            spawner.SetActive(true);
        }
    }
    public void Win(Player player)
    {
        if (player == PhotonNetwork.LocalPlayer)
        {
            //Win
            winnerScreen.SetActive(true);
            loserScreen.SetActive(false);
        }
        else
        {
            //Lose
            loserScreen.SetActive(true);
            winnerScreen.SetActive(false);
        }
    }

}
