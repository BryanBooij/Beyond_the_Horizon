using Assets.Scripts.Player;
using UnityEngine;

public class BossBeamDamage : MonoBehaviour
{
    public int damagePerTick = 10;
    public float tickInterval = 0.25f;
    private float nextTick;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Time.time < nextTick) return;

        PlayerHealth player = collision.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(damagePerTick);
            nextTick = Time.time + tickInterval;
        }
    }
}