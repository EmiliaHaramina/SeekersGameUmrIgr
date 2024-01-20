using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilityComponent : MonoBehaviour
{
    [SerializeField] private int _numberOfAbilities = 3;
    private List<AbilityButton> _abilityButtons;

    bool onCooldown;
    AbilityButton lastUsedAbility;

    private void OnEnable()
    {
        foreach (AbilityButton abilityButton in _abilityButtons)
        {
            abilityButton.Button.onClick.AddListener(() => UseAbility(abilityButton));
        }
    }

    private void OnDisable()
    {
        foreach (AbilityButton abilityButton in _abilityButtons)
        {
            abilityButton.Button.onClick.RemoveListener(() => UseAbility(abilityButton));
        }
    }

    void Start()
    {
        ToggleCooldown(false);

        foreach (AbilityButton abilityButton in _abilityButtons)
        {
            SetButton(abilityButton);
        }

        _abilityButtons = new List<AbilityButton>(_numberOfAbilities);

        XROrigin xrRig = FindObjectOfType<XROrigin>();

        Button cloneAbilityButton = xrRig.transform.Find("Camera Offset/Main Camera/Follow GameObject/Hand Scroll View/Panel/Scroll View/Viewport/Content/CloneButton").GetComponent<Button>();
        Button invisAbilityButton = xrRig.transform.Find("Camera Offset/Main Camera/Follow GameObject/Hand Scroll View/Panel/Scroll View/Viewport/Content/InvisButton").GetComponent<Button>();
        Button tpAbilityButton = xrRig.transform.Find("Camera Offset/Main Camera/Follow GameObject/Hand Scroll View/Panel/Scroll View/Viewport/Content/TpButton").GetComponent<Button>();

        CloneAbility cloneAbility = FindObjectOfType<CloneAbility>();
        TeleportationAbility tpAbility = FindObjectOfType<TeleportationAbility>();
        InvisibilityAbility invisAbility = FindObjectOfType<InvisibilityAbility>();

        _abilityButtons[0].Ability = cloneAbility;
        _abilityButtons[0].Button = cloneAbilityButton;

        _abilityButtons[1].Ability = invisAbility;
        _abilityButtons[1].Button = invisAbilityButton;

        _abilityButtons[2].Ability = tpAbility;
        _abilityButtons[2].Button = tpAbilityButton;
    }

    public void UseAbility(AbilityButton abilityButton)
    {
        if (onCooldown || this.gameObject.tag == "seeker") return;
        

        ToggleCooldown(true);
        SetButton(abilityButton);
        lastUsedAbility = abilityButton;

        abilityButton.Ability.Use(this.gameObject);

        Invoke("RefreshCooldown", abilityButton.Ability.Cooldown);
    }

    private void RefreshCooldown()
    {
        ToggleCooldown(false);
        SetButton(lastUsedAbility);
    }

    private void ToggleCooldown(bool value)
    {
        onCooldown = value;
    }

    private void SetButton(AbilityButton abilityButton)
    {
        if (onCooldown)
        {
            abilityButton.Button.image.color = Color.red;
        } 
        else
        {
            abilityButton.Button.image.color = Color.green;
        }
        abilityButton.Button.enabled = !onCooldown;
    }
}
