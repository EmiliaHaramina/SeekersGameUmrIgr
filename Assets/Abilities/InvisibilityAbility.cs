using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.XR.CoreUtils;
using UnityEngine;

public class InvisibilityAbility : Ability
{
    public override string Name
    {
        get { return "Invisibility"; }
    }
    public override float Cooldown
    {
        get { return 20f; }
        set { }
    }

    float invisibilityDuration = 4f;

    public GameObject Caller;

    public override void Use(GameObject caller)
    {
        Caller = caller;
        caller.transform.Find("Mesh").gameObject.SetActive(false);
        caller.transform.Find("Armors").gameObject.SetActive(false);
        Invoke("TurnVisible", invisibilityDuration);
    }


    public void TurnVisible()
    {
        Caller.transform.Find("Mesh").gameObject.SetActive(true);
        Caller.transform.Find("Armors").gameObject.SetActive(true);

    }
}
