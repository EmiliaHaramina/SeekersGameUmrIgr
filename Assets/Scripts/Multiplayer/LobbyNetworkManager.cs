using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LobbyNetworkManager : MonoBehaviourPunCallbacks
{
    //[SerializeField] private TMP_InputField _roomInput;
    [SerializeField] private RoomItemUI _roomItemUIPrefab;
    [SerializeField] private Transform _roomListParent;

    [SerializeField] private RoomItemUI _playerItemUIPrefab;
    [SerializeField] private Transform _playerListParent;

    [SerializeField] private TMP_Text _statusField;
    [SerializeField] private Button _leaveRoomButton;
    [SerializeField] private Button _startGameButton;

    [SerializeField] private TMP_Text _currentLocationText;

    [SerializeField] private GameObject _roomListWindow;
    [SerializeField] private GameObject _playerListWindow;
    [SerializeField] private GameObject _createRoomWindow;

    private List<RoomItemUI> _roomList = new List<RoomItemUI>();
    private List<RoomItemUI> _playerList = new List<RoomItemUI>();

    // Player Name Functionality
    //[SerializeField] private TMP_InputField _playerNameInput;
    [SerializeField] private TMP_Text _playerNameLabel;
    //private bool _isPlayerNameChanging;
    
    void Start()
    {
        Connect();
        Initialize();
    }

    #region PhotonCallbacks

    public override void OnConnectedToMaster()
    {
        _statusField.text = "Connected to master server";
        //PhotonNetwork.NickName = "Player" + Random.Range(0, 5000);
        _playerNameLabel.text = PhotonNetwork.NickName;
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateRoomList(roomList);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        if (_statusField == null)
        {
            return;
        }
        _statusField.text = "Disconnected.";
    }

    public override void OnJoinedLobby()
    {
        _currentLocationText.text = "Rooms";
        Debug.Log("Joined lobby.");
    }

    public override void OnJoinedRoom()
    {
        _statusField.text = "Joined " + PhotonNetwork.CurrentRoom.Name;
        _currentLocationText.text = PhotonNetwork.CurrentRoom.Name;

        _leaveRoomButton.interactable = true;

        if (PhotonNetwork.IsMasterClient)
        {
            _startGameButton.interactable = true;
        }

        ShowWindow(false);
        UpdatePlayerList();
    }

    public override void OnLeftRoom()
    {
        // TODO: Destroy room? It stays for other players, but they cant start the game
        if (_statusField != null)
        {
            _statusField.text = "LOBBY";
        }

        if (_currentLocationText != null)
        {
            _currentLocationText.text = "Rooms";
        }

        _leaveRoomButton.interactable = false;
        _startGameButton.interactable = false;

        ShowWindow(true);
        UpdatePlayerList();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }

    public void OnChangePlayerNamePressed()
    {
        //if (_isPlayerNameChanging == false)
        //{
        //    _playerNameInput.text = _playerNameLabel.text;
        //    _playerNameLabel.gameObject.SetActive(false);
        //    _playerNameInput.gameObject.SetActive(true);
        //    _isPlayerNameChanging = true;
        //}
        //else
        //{
        //    // check for empty or long names
        //    if (string.IsNullOrEmpty(_playerNameInput.text) == false && _playerNameInput.text.Length <= 12)
        //    {
        //        _playerNameLabel.text = _playerNameInput.text;
        //        PhotonNetwork.LocalPlayer.NickName = _playerNameInput.text;
        //        if (photonView.IsMine)
        //            Debug.Log("IsMine");
        //        else
        //            Debug.Log("Isn't mine");

        //        Debug.Log(photonView);
        //        // TODO: Doesn't work, don't know how to set photonView, but maybe in VR it is not needed
        //        // since it is difficult to type
        //        //photonView.RPC("ForcePlayerListUpdate", RpcTarget.All);
        //    }

        //    _playerNameLabel.gameObject.SetActive(true);
        //    _playerNameInput.gameObject.SetActive(false);
        //    _isPlayerNameChanging = false;
        //}
    }

    [PunRPC]
    public void ForcePlayerListUpdate()
    {
        UpdatePlayerList();
    }

    #endregion

    private void Initialize()
    {
        _leaveRoomButton.interactable = false;
    }

    private void Connect()
    {
        PhotonNetwork.NickName = "Player" + Random.Range(0, 5000);
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void UpdateRoomList(List<RoomInfo> roomList)
    {
        // Clear the current list of rooms
        for (int i = 0; i < _roomList.Count; i++)
        {
            Destroy(_roomList[i].gameObject);
        }

        _roomList.Clear();

        // Generate a new list with the updated info
        for (int i = 0; i < roomList.Count; i++)
        {
            // skip empty rooms
            if (roomList[i].PlayerCount == 0)
            {
                continue;
            }

            RoomItemUI newRoomItem = Instantiate(_roomItemUIPrefab);
            newRoomItem.lobbyNetworkParent = this;
            newRoomItem.SetName(roomList[i].Name);
            newRoomItem.transform.SetParent(_roomListParent);

            _roomList.Add(newRoomItem);
        }
    }

    private void UpdatePlayerList()
    {
        // Clear the current player list
        for (int i = 0; i < _playerList.Count; i++)
        {
            Destroy(_playerList[i].gameObject);
        }

        _playerList.Clear();

        if (PhotonNetwork.CurrentRoom == null)
        {
            return;
        }

        // Generate a new list of players
        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            RoomItemUI newPlayerItem = Instantiate(_playerItemUIPrefab);

            newPlayerItem.transform.SetParent(_playerListParent);
            newPlayerItem.SetName(player.Value.NickName);

            _playerList.Add(newPlayerItem);
        }
    }

    private void ShowWindow(bool isRoomList)
    {
        _roomListWindow.SetActive(isRoomList);
        _playerListWindow.SetActive(!isRoomList);
        _createRoomWindow.SetActive(isRoomList);
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void CreateRoom()
    {
        //if (!string.IsNullOrEmpty(_roomInput.text))
        //{
        PhotonNetwork.CreateRoom(PhotonNetwork.NickName + "'s Room", new RoomOptions() { MaxPlayers = 5 }, null);
        //}
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    //public void OnBackToLobbyPressed()
    //{
    //    PhotonNetwork.Disconnect();
    //    SceneManager.LoadScene("LobbyScene");
    //}

    public void OnStartGamePressed()
    {
        PhotonNetwork.LoadLevel("Market");
    }
}
