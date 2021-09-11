using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayersNicknames : MonoBehaviour
{
    [SerializeField] private Text text;

    void Update()
    {
        Player[] players = PhotonNetwork.PlayerList;
        text.text = "Players:" + "\n";
        for (int i = 0; i < players.Length; i++)
        {
            var curr = players[i];
            text.text += curr.NickName + "\n";
        }
    }
}
