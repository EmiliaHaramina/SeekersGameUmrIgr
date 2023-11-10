using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilityComponent : MonoBehaviour
{
    public Ability MyAbility;
    public GameObject Clone;

    public Button AbilityButton;

    bool onCooldown;
    void Start()
    {
        MyAbility = this.AddComponent<CloneAbility>();
        (MyAbility as CloneAbility).Clone = Clone;

        //MyAbility = this.AddComponent<InvisibilityAbility>();

        RefreshCooldown();
    }


    public void UseAbility()
    {
        if (onCooldown) return;

        onCooldown = true;
        AbilityButton.enabled = false;
        AbilityButton.image.color = Color.red;

        MyAbility.Use(this.gameObject);
        
        Invoke("RefreshCooldown", MyAbility.Cooldown);
    }

    void RefreshCooldown()
    {
        onCooldown = false;
        AbilityButton.enabled = true;
        AbilityButton.image.color = Color.green;
    }
}
