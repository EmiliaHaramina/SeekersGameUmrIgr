using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationAbility : Ability
{
    [SerializeField] private List<XRRayInteractor> _teleportInteractors;

    private float _teleportationDuration = 2f;

    public override string Name
    {
        get { return "Teleportation"; }
    }

    public override float Cooldown
    {
        get { return 30f; }
        set { }
    }

    public override Ability Get()
    {
        return this;
    }

    public override void Use(GameObject caller)
    {
        foreach (XRRayInteractor interactor in _teleportInteractors)
        {
            interactor.enabled = true;
        }

        Invoke("DisableTeleportation", _teleportationDuration);
    }

    private void DisableTeleportation()
    {
        foreach (XRRayInteractor interactor in _teleportInteractors)
        {
            interactor.enabled = false;
        }
    }
}
