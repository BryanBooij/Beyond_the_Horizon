using System.Collections;
using Assets.Scripts.Player;
using UnityEngine;

[CreateAssetMenu(
    fileName = "DamageImmunity",
    menuName = "Scriptable Objects/Powerups/Damage Immunity"
)]
public class DamageImmunity : PowerupEffect
{
    public float duration = 5f;

    private Coroutine immunityCoroutine;

    public override void Apply(GameObject target)
    {
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();

        if (playerHealth == null)
            return;

        if (immunityCoroutine != null)
        {
            playerHealth.StopCoroutine(immunityCoroutine);
        }

        immunityCoroutine = playerHealth.StartCoroutine(
            ImmunityCoroutine(playerHealth)
        );
    }

    private IEnumerator ImmunityCoroutine(PlayerHealth playerHealth)
    {
        playerHealth.SetImmunity(true);

        yield return new WaitForSeconds(duration);

        playerHealth.SetImmunity(false);

        immunityCoroutine = null;
    }
}