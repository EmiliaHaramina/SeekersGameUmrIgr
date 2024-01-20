using UnityEngine.UI;

[System.Serializable]
public class AbilityButton
{
    public Ability Ability;
    public Button Button;

    public AbilityButton(Ability ability, Button button)
    {
        Ability = ability;
        Button = button;
    }
}
