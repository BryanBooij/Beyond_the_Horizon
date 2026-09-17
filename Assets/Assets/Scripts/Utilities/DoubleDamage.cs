using System.Collections;
using UnityEngine;

[CreateAssetMenu(
    fileName = "DoubleDamage",
    menuName = "Scriptable Objects/Powerups/Double Damage"
)]
public class DoubleDamage : PowerupEffect
{
    public float duration = 5f;

    private Coroutine doubleDamageCoroutine;

    public override void Apply(GameObject target)
    {
        ProjectileShoot projectileShoot = target.GetComponent<ProjectileShoot>();
        SpriteRenderer spriteRenderer = target.GetComponent<SpriteRenderer>();

        if (projectileShoot == null || spriteRenderer == null)
            return;

        if (doubleDamageCoroutine != null)
        {
            projectileShoot.StopCoroutine(doubleDamageCoroutine);
        }

        doubleDamageCoroutine = projectileShoot.StartCoroutine(
            DoubleDamageCoroutine(projectileShoot, spriteRenderer)
        );
    }

    private IEnumerator DoubleDamageCoroutine(
        ProjectileShoot projectileShoot,
        SpriteRenderer spriteRenderer)
    {
        projectileShoot.SetDoubleDamage(true);

        // Player rood maken
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(duration);

        projectileShoot.SetDoubleDamage(false);

        // Player weer wit maken
        spriteRenderer.color = Color.white;

        doubleDamageCoroutine = null;
    }
}