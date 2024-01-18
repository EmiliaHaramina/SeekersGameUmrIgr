using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.XR.CoreUtils;
using UnityEngine;

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
        get { return 30f; }
        set { }
    }

    public override void Use(GameObject caller)
    {
        _caller = caller;
        caller.transform.Find("Mesh").gameObject.SetActive(false);
        caller.transform.Find("Armors").gameObject.SetActive(false);
        Invoke("TurnVisible", _invisibilityDuration);
    }


    public void TurnVisible()
    {
        _caller.transform.Find("Mesh").gameObject.SetActive(true);
        _caller.transform.Find("Armors").gameObject.SetActive(true);

    }
}
