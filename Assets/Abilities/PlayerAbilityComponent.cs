using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityComponent : MonoBehaviour
{
    [SerializeField] private List<AbilityButton> _abilityButtons;

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
    }

    public void UseAbility(AbilityButton abilityButton)
    {
        if (onCooldown) return;

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
