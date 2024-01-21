using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void OnBackToLobbyPressed()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("LobbyScene");
    }
}
