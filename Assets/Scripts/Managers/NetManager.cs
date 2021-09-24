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
    [SerializeField] private Button _button;
    [SerializeField] private TMP_InputField _inputFieldRoom;
    [SerializeField] private TMP_InputField _inputFieldPlayer;

    void Start()
    {
        _button.interactable = false;
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
        _button.interactable = true;
    }

    public void Connect()
    {
        if (string.IsNullOrWhiteSpace(_inputFieldRoom.text) || string.IsNullOrEmpty(_inputFieldRoom.text)) return;
        if (string.IsNullOrWhiteSpace(_inputFieldPlayer.text) || string.IsNullOrEmpty(_inputFieldPlayer.text)) return;

        PhotonNetwork.NickName = _inputFieldPlayer.text;
        RoomOptions option = new RoomOptions();
        option.IsOpen = true;
        option.IsVisible = true;
        option.MaxPlayers = 4;

        PhotonNetwork.JoinOrCreateRoom(_inputFieldRoom.text, option, TypedLobby.Default);
        _button.interactable = false;
    }
    public override void OnCreatedRoom()
    {
        print("Created Room");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        _button.interactable = true;
    }

    public override void OnJoinedRoom()
    {
        print("Joined Room");
        PhotonNetwork.LoadLevel("Western");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        _button.interactable = true;
    }

}
