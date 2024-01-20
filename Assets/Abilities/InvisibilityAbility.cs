using UnityEngine;
using Photon.Pun;

public class InvisibilityAbility : Ability
{
    private GameObject _caller;
    private float _invisibilityDuration = 3f;

    public override string Name
    {
        get { return "Invisibility"; }
    }

    public override float Cooldown
    {
        get { return 5f; }
        set { }
    }

    public override void Use(GameObject caller)
    {
        _caller = caller;
        PhotonView _photonView = caller.GetComponent<PhotonView>();
        _photonView.RPC("RPC_TurnInvisible", RpcTarget.All, caller);

        //caller.transform.Find("PlayerTest").gameObject.SetActive(false);
        //caller.transform.Find("Armors").gameObject.SetActive(false);
        Invoke("TurnVisible", _invisibilityDuration);
    }

    [PunRPC]
    public void RPC_TurnInvisible(GameObject caller) {
        caller.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        //transform.Find("Armors").gameObject.SetActive(false);
    }

    public void TurnVisible()
    {
        PhotonView _photonView = _caller.GetComponent<PhotonView>();
        _photonView.RPC("RPC_TurnVisible", RpcTarget.All, _caller);

        _caller.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        //_caller.transform.Find("Armors").gameObject.SetActive(true);

    }

    [PunRPC]
    public void RPC_TurnVisible(GameObject caller)
    {
        _caller.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        //transform.Find("Armors").gameObject.SetActive(true);
    }

    public override Ability Get()
    {
        return this;
    }
}
