using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileShoot : MonoBehaviour
{
    [SerializeField] private GameObject[] projectilePrefab;
    [SerializeField] private GameObject doubleDamageProjectilePrefab;

    public float fireRate = 0.5f;
    public float nextFireTime = 0f;

    private bool doubleDamageActive = false;

    void Update()
    {
        if (Gamepad.current != null &&
            Gamepad.current.buttonSouth.wasPressedThisFrame &&
            Time.time >= nextFireTime)
        {
            GameObject prefabToSpawn;

            if (doubleDamageActive)
            {
                prefabToSpawn = doubleDamageProjectilePrefab;
            }
            else
            {
                prefabToSpawn = projectilePrefab[
                    Random.Range(0, projectilePrefab.Length)
                ];
            }

            GameObject laser = Instantiate(
                prefabToSpawn,
                transform.position,
                Quaternion.identity
            );

            if (doubleDamageActive)
            {
                Projectile_Lazer normalLaser =
                    projectilePrefab[0].GetComponent<Projectile_Lazer>();

                Projectile_Lazer doubleLaser =
                    laser.GetComponent<Projectile_Lazer>();

                doubleLaser.Damage = normalLaser.Damage * 2f;
            }

            nextFireTime = Time.time + fireRate;
        }
    }

    public void SetDoubleDamage(bool value)
    {
        doubleDamageActive = value;
    }
}