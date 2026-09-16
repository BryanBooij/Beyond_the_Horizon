using UnityEngine;

[CreateAssetMenu(fileName = "PowerupEffect", menuName = "Scriptable Objects/Powerups")]
public abstract class PowerupEffect : ScriptableObject
{
    public abstract void Apply(GameObject target);
}
