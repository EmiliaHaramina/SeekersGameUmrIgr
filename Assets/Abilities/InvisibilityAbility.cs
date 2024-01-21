using UnityEngine;
using Photon.Pun;

public class InvisibilityAbility : Ability
{
    private GameObject _caller;
    private float _invisibilityDuration = 5f;

    public override string Name
    {
        get { return "Invisibility"; }
    }

    public override float Cooldown
    {
        get { return 15f; }
        set { }
    }

    public override void Use(GameObject caller)
    {
        _caller = caller;
        PhotonView _photonView = caller.GetComponent<PhotonView>();
        _photonView.RPC("RPC_TurnInvisible", RpcTarget.All);

        //caller.transform.Find("PlayerTest").gameObject.SetActive(false);
        //caller.transform.Find("Armors").gameObject.SetActive(false);
        Invoke("TurnVisible", _invisibilityDuration);
    }

    [PunRPC]
    public void RPC_TurnInvisible() {
        this.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        this.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        //transform.Find("Armors").gameObject.SetActive(false);
    }

    public void TurnVisible()
    {
        PhotonView _photonView = _caller.GetComponent<PhotonView>();
        _photonView.RPC("RPC_TurnVisible", RpcTarget.All);
        //_caller.transform.Find("Armors").gameObject.SetActive(true);

    }

    [PunRPC]
    public void RPC_TurnVisible()
    {
        this.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        this.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        //transform.Find("Armors").gameObject.SetActive(true);
    }

    public override Ability Get()
    {
        return this;
    }
}
