using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public bool isSeeker = false;

    [PunRPC]
    public void SetSeeker()
    {
        isSeeker = true;
        gameObject.tag = "seeker";
        gameObject.transform.position = GameObject.FindGameObjectWithTag("SeekerSpawnPoint").transform.position;
        gameObject.transform.GetComponent<PlayerGameLogic>().SetMoveSpeed(2.2f);
    }

    [PunRPC]
    public void SetHider()
    {
        Debug.Log("Hiders set");
        gameObject.tag = "hider";
    }
}
