using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public abstract string Name { get; }
    public abstract float Cooldown { get; set; }
    public abstract void Use(GameObject caller);

}
