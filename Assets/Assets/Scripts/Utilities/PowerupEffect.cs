using UnityEngine;

[CreateAssetMenu(fileName = "PowerupEffect", menuName = "Scriptable Objects/Powerups")]
public abstract class PowerupEffect : ScriptableObject
{
    public Sprite icon;
    public GameObject powerupPrefab;

    public abstract void Apply(GameObject target);
}

