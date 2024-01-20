using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationAbility : Ability
{
    [SerializeField] private List<XRRayInteractor> _teleportInteractors;

    private Transform leftHandRig;
    private Transform rightHandRig;
    private float _teleportationDuration = 2f;

    private void Start()
    {
        XROrigin rig = FindObjectOfType<XROrigin>();
        leftHandRig = rig.transform.Find("Camera Offset/Left Controller");
        rightHandRig = rig.transform.Find("Camera Offset/Right Controller");
    }

    public override string Name
    {
        get { return "Teleportation"; }
    }

    public override float Cooldown
    {
        get { return 5f; }
        set { }
    }

    public override Ability Get()
    {
        return this;
    }

    public override void Use(GameObject caller)
    {
        XRRayInteractor leftRay = leftHandRig.GetComponentInChildren<XRRayInteractor>();
        leftRay.enabled = true;

        Debug.Log(leftRay);

        XRRayInteractor rightRay = rightHandRig.GetComponentInChildren<XRRayInteractor>() ;
        rightRay.enabled = true;

        Invoke("DisableTeleportation", _teleportationDuration);
    }

    private void DisableTeleportation()
    {
        XRRayInteractor leftRay = leftHandRig.GetComponentInChildren<XRRayInteractor>();
        leftRay.enabled = false;

        XRRayInteractor rightRay = rightHandRig.GetComponentInChildren<XRRayInteractor>();
        rightRay.enabled = false;
    }
}
