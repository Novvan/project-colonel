using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

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

    public GameObject Hud;
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
            photonView.RPC("KillZombies", RpcTarget.All);

            // Check killed zombies
            Player winner = null;
            foreach (PhotonView player in PhotonNetwork.PhotonViewCollection)
            {
                if (player.gameObject.GetComponent<Character>() != null)
                {
                    winner = player.Controller;
                }
            }
            TriggerWin();
        }
        if (Hud != null)
        {
            Hud.GetComponent<TextMeshPro>().text = _zombieCount.ToString();
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
                StartCoroutine("WaitToStart");
                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.CurrentRoom.IsVisible = false;
            }
        }
    }

    IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(_secondsToStart);
        if (photonView != null) photonView.RPC("StartGame", RpcTarget.All);
    }

    private void TriggerWin()
    {
        photonView.RPC("Win", RpcTarget.All);
    }

    [PunRPC]
    public void StartGame()
    {
        foreach (GameObject spawner in _zombieSpawners)
        {
            spawner.SetActive(true);
        }
    }

    [PunRPC]
    public void Win()
    {
        //Win
        WinnerScreen.SetActive(true);
        LoserScreen.SetActive(false);

        Time.timeScale = 0;
    }

    [PunRPC]
    public void KillZombies()
    {
        Character[] characterList = FindObjectsOfType<Character>();

        foreach (var item in characterList)
        {
            if (item.gameObject.CompareTag("Enemy")) Destroy(item.gameObject);
        }
    }
}
