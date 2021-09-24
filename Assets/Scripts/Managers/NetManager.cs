using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class NetManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Button button;
    [SerializeField] TMP_InputField inputFieldRoom;
    [SerializeField] TMP_InputField inputFieldPlayer;

    void Start()
    {
        button.interactable = false;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("Connected to master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        print("Connected to Lobby");
        button.interactable = true;
    }

    public void Connect()
    {
        if (string.IsNullOrWhiteSpace(inputFieldRoom.text) || string.IsNullOrEmpty(inputFieldRoom.text)) return;
        if (string.IsNullOrWhiteSpace(inputFieldPlayer.text) || string.IsNullOrEmpty(inputFieldPlayer.text)) return;

        PhotonNetwork.NickName = inputFieldPlayer.text;
        RoomOptions option = new RoomOptions();
        option.IsOpen = true;
        option.IsVisible = true;
        option.MaxPlayers = 4;

        PhotonNetwork.JoinOrCreateRoom(inputFieldRoom.text, option, TypedLobby.Default);
        button.interactable = false;
    }
    public override void OnCreatedRoom()
    {
        print("Created Room");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        button.interactable = true;
    }

    public override void OnJoinedRoom()
    {
        print("Joined Room");
        PhotonNetwork.LoadLevel("Western");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        button.interactable = true;
    }

}
