using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class SpeedAbility : Ability
{
    [SerializeField] private float _boostSpeed = 2.4f;

    private Transform _moveObjectTransform;
    private float _speedDuration = 2f;
    private readonly float hiderSpeed = 2f;

    public override string Name
    {
        get { return "Speed"; }
    }

    public override float Cooldown {
        get { return 5f; }
        set { }
    }

    public override Ability Get()
    {
        return this;
    }

    // Start is called before the first frame update
    void Start()
    {
        XROrigin rig = FindObjectOfType<XROrigin>();
        _moveObjectTransform = rig.gameObject.transform.GetChild(1).GetChild(1);
    }

    public override void Use(GameObject caller)
    {
        BoostSpeed(_boostSpeed);

        Invoke("RevertSpeed", _speedDuration);
    }

    private void BoostSpeed(float newSpeed)
    {
        DynamicMoveProvider moveProvider = _moveObjectTransform.GetComponent<DynamicMoveProvider>();
        moveProvider.moveSpeed = newSpeed;
    }

    private void RevertSpeed()
    {
        BoostSpeed(hiderSpeed);
    }
}
