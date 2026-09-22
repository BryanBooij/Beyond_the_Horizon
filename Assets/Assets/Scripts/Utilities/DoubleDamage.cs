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

        if (projectileShoot == null)
            return;

        if (doubleDamageCoroutine != null)
        {
            projectileShoot.StopCoroutine(doubleDamageCoroutine);
        }

        doubleDamageCoroutine = projectileShoot.StartCoroutine(
            DoubleDamageCoroutine(projectileShoot)
        );
    }

    private IEnumerator DoubleDamageCoroutine(ProjectileShoot projectileShoot)
    {
        projectileShoot.SetDoubleDamage(true);

        Transform aura = projectileShoot.transform.Find("Aura");

        if (aura != null)
        {
            aura.gameObject.SetActive(true);

            SpriteRenderer auraRenderer = aura.GetComponent<SpriteRenderer>();

            float elapsed = 0f;

            while (elapsed < duration)
            {
                float alpha = Mathf.Lerp(
                    0.2f,
                    1f,
                    (Mathf.Sin(elapsed * 5f) + 1f) / 2f
                );

                Color color = auraRenderer.color;
                color.a = alpha;
                auraRenderer.color = color;

                elapsed += Time.deltaTime;

                yield return null;
            }

            aura.gameObject.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(duration);
        }

        projectileShoot.SetDoubleDamage(false);

        doubleDamageCoroutine = null;
    }
}