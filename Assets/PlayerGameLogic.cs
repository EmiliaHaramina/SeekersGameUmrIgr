using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class PlayerGameLogic : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameLogic gameLogic;
    [SerializeField] Transform _position;
    [SerializeField] PositionConstraint _posCon;
    [SerializeField] Canvas _canvas;
    [SerializeField] GameObject _moveSpeed;
    void Start() { 
    
        gameLogic = GameObject.Find("GameLogicObject").GetComponent<GameLogic>();
        _moveSpeed = GameObject.FindGameObjectWithTag("MoveSpeed");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SetMoveSpeed(float speed) {
        _moveSpeed.transform.GetComponent<DynamicMoveProvider>().moveSpeed = speed;
    }

    public void GotCaught(GameObject go)
    {
        //Prebacit kameru u zrak i freezat joj poziciju, dopustit okretanje da se gleda mapa?
        //lokalno
        PhotonView _photonView =  go.GetComponent<PhotonView>();
        _photonView.RPC("RPC_Died", RpcTarget.Others);

        go.transform.position = new Vector3(-14.9803352f, 16.507f, 9.3464632f);
        go.GetComponent<PositionConstraint>().enabled = true;
        go.SetActive(false);

        gameLogic.PlayerCaught();
    }


    [PunRPC]
    void RPC_Died()
    {
        if (!GetComponent<PhotonView>().IsMine) { return; }

        _canvas.gameObject.SetActive(true);
        //prebacit kameru svim ostalima
        transform.position = new Vector3(-14.9803352f, 16.507f, 9.3464632f);
        GetComponent<PositionConstraint>().enabled = true;
        gameObject.SetActive(false);
        gameLogic.PlayerCaught();
    }


}