using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MasterClient : MonoBehaviour
{
    public void Initialize()
    {
        StartCoroutine(PickSeeker());
    }

    private IEnumerator PickSeeker()
    {
        GameObject[] players;
        int tries = 0;

        // Get all the players in the game
        do
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            tries++;
            yield return new WaitForSeconds(0.25f);
        } while ((players.Length < PhotonNetwork.CurrentRoom.PlayerCount) && (tries < 5));

        // Assign the seeker
        GameObject pickedSeeker = players[Random.Range(0, players.Length)];

        PhotonView pv = pickedSeeker.GetComponent<PhotonView>();
        pv.RPC("SetSeeker", RpcTarget.All);
    }

    [PunRPC]
    public void SeekerPicked()
    {

    }
}
