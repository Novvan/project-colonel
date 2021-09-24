using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private List<GameObject> _zombieSpawners;
    [SerializeField] private float _secondsToStart = 2;
    [SerializeField] private int _numberOfPlayers;
    [SerializeField] private int _maxZombieCount;
    [SerializeField] private int _maxZombiesAlive;

    private float _spawnCooldown = 3;
    private float _currentSpawnCooldown = 0;
    private int _zombieCount;
    private int _zombiesAlive;
    private bool _hasWinner;
    public int ZombieCount { get => _zombieCount; set => _zombieCount = value; }
    public int MaxZombiesAlive { get => _maxZombiesAlive; set => _maxZombiesAlive = value; }
    public int ZombiesAlive { get => _zombiesAlive; set => _zombiesAlive = value; }

    public GameObject WinnerScreen;
    public GameObject LoserScreen;

    void Start()
    {
        foreach (GameObject spawner in _zombieSpawners)
        {
            spawner.SetActive(false);
        }
    }

    void Update()
    {
        if (_zombieCount >= _maxZombieCount && !_hasWinner)
        {
            _hasWinner = true;

            // Stop spawning
            foreach (GameObject spawner in _zombieSpawners) spawner.SetActive(false);
            photonView.RPC("killZombies", RpcTarget.All);

            // Check killed zombies
            float maxKillCount = 0;
            PhotonView winner = null;
            foreach (PhotonView player in PhotonNetwork.PhotonViewCollection)
            {
                if (player.gameObject.GetComponent<Character>() != null)
                {
                    Character character = player.gameObject.GetComponent<Character>();

                    if (character.Killcount >= maxKillCount)
                    {
                        winner = player;
                        maxKillCount = character.Killcount;
                    }
                }
                else Debug.Log("Character inexistente pa");
            }
            TriggerWin(winner);
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            var playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            if (playerCount >= _numberOfPlayers)
            {
                //start game
                // StartCoroutine(WaitToStart());
                photonView.RPC("StartGame", RpcTarget.All);
                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.CurrentRoom.IsVisible = false;
            }
        }
    }
    // IEnumerator WaitToStart()
    // {
    //     yield return new WaitForSeconds(_secondsToStart);
    //     photonView.RPC("StartGame", RpcTarget.All);
    // }

    [PunRPC]
    void StartGame()
    {
        float counter = 0;
        float waitTime = 0;
        if (counter <= waitTime)
        {
            counter += Time.deltaTime;
        }
        else
        {
            foreach (GameObject spawner in _zombieSpawners)
            {
                spawner.SetActive(true);
            }
        }

    }

    public void Win(Player player)
    {
        if (player == PhotonNetwork.LocalPlayer)
        {
            //Win
            WinnerScreen.SetActive(true);
            LoserScreen.SetActive(false);
        }
        else
        {
            //Lose
            LoserScreen.SetActive(true);
            WinnerScreen.SetActive(false);
        }
    }

    private void killZombies()
    {
        Character[] characterList = FindObjectsOfType<Character>();

        foreach (var item in characterList)
        {
            if (item.gameObject.CompareTag("Enemy")) Destroy(item.gameObject);
        }
    }

    private void TriggerWin(PhotonView currentWinner)
    {
        if (currentWinner != null)
        {
            photonView.RPC("Win", RpcTarget.All, currentWinner);
        }
    }
}
