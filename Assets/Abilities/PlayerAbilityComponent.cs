using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerAbilityComponent : MonoBehaviour
{
    [SerializeField] private int _numberOfAbilities = 3;
    [SerializeField] private AbilityButton[] _abilityButtons;

    bool onCooldown;
    AbilityButton lastUsedAbility;

    //private void OnEnable()
    //{
    //    foreach (AbilityButton abilityButton in _abilityButtons)
    //    {
    //        abilityButton.Button.onClick.AddListener(() => UseAbility(abilityButton));
    //    }
    //}

    private void OnDisable()
    {
        foreach (AbilityButton abilityButton in _abilityButtons)
        {
            abilityButton.Button.onClick.RemoveListener(() => UseAbility(abilityButton));
        }
    }

    void Start()
    {
        _abilityButtons = new AbilityButton[_numberOfAbilities];

        ToggleCooldown(false);

        var buttons = GameObject.FindGameObjectsWithTag("ButtonTag");

        GameObject abilities = GameObject.Find("Abilities");

        CloneAbility cloneAbility = abilities.GetComponentInChildren<CloneAbility>();
        TeleportationAbility tpAbility = abilities.GetComponentInChildren<TeleportationAbility>();
        InvisibilityAbility invisAbility = abilities.GetComponentInChildren<InvisibilityAbility>();

        _abilityButtons[0] = new AbilityButton(cloneAbility, buttons[2].GetComponent<Button>());
        _abilityButtons[1] = new AbilityButton(invisAbility, buttons[1].GetComponent<Button>());
        _abilityButtons[2] = new AbilityButton(tpAbility, buttons[0].GetComponent<Button>());

        foreach (AbilityButton abilityButton in _abilityButtons)
        {
            abilityButton.Button.onClick.AddListener(() => UseAbility(abilityButton));
            SetButton(abilityButton);
        }
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
