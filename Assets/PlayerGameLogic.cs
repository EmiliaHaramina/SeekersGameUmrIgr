using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerGameLogic : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameLogic gameLogic;
    [SerializeField] Transform _position;
    [SerializeField] PositionConstraint _posCon;
    void Start() { 
    
        gameLogic = GameObject.Find("GameLogicObject").GetComponent<GameLogic>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void GotCaught(GameObject go)
    {
        //Prebacit kameru u zrak i freezat joj poziciju, dopustit okretanje da se gleda mapa?
        //lokalno
        go.transform.position = new Vector3(-14.9803352f, 16.507f, 9.3464632f);
        go.GetComponent<PositionConstraint>().enabled = true;
        go.SetActive(false);

        gameLogic.PlayerCaught();
        PhotonView _photonView =  go.GetComponent<PhotonView>();
        _photonView.RPC("RPC_Died", RpcTarget.Others);
    }


    [PunRPC]
    void RPC_Died()
    {
        if (!GetComponent<PhotonView>().IsMine) { return; }

        //prebacit kameru svim ostalima
        transform.position = new Vector3(-14.9803352f, 16.507f, 9.3464632f);
        GetComponent<PositionConstraint>().enabled = true;
        gameObject.SetActive(false);
        gameLogic.PlayerCaught();
    }

}