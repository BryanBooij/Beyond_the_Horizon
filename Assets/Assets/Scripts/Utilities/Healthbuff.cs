using Assets.Scripts.Player;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthBuff", menuName = "Scriptable Objects/Powerups/Health Buff")]
public class HealthBuff : PowerupEffect
{
    public int amount;

    public override void Apply(GameObject target)
    {
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();

        if (playerHealth == null)
            return;

        playerHealth.currentHealth = Mathf.Min(
            playerHealth.currentHealth + amount,
            playerHealth.maxHealth
        );
        playerHealth.UpdateHealthSprite();

        Transform healthEffect = target.transform.Find("HealthEffect");

        if (healthEffect == null)
            return;

        ParticleSystem particles = healthEffect.GetComponent<ParticleSystem>();

        if (particles == null)
            return;

        particles.gameObject.SetActive(true);
        particles.Stop();
        particles.Clear();
        particles.Play();
    }
}