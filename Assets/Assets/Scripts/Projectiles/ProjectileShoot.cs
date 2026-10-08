using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class ProjectileShoot : MonoBehaviour
{
    [SerializeField] private GameObject[] projectilePrefab;
    [SerializeField] private GameObject doubleDamageProjectilePrefab;
    
    [Header("Shoot Button")]
    [SerializeField] private Image shootButtonImage;
    [SerializeField] private Sprite normalShootSprite;
    [SerializeField] private Sprite doubleDamageShootSprite;
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip laserSound;

    public float fireRate = 0.5f;
    public float nextFireTime = 0f;

    private bool doubleDamageActive = false;

    void Update()
    {
        // tap to shoot
        // if (Gamepad.current != null &&
        //     Gamepad.current.buttonSouth.wasPressedThisFrame &&
        //     Time.time >= nextFireTime)
        // hold to shoot
        if (Gamepad.current != null &&
            Gamepad.current.buttonSouth.isPressed &&
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
            
            // Laser sound when it is fired
            audioSource.PlayOneShot(laserSound);

            nextFireTime = Time.time + fireRate;
        }
    }

    public void SetDoubleDamage(bool value)
    {
        doubleDamageActive = value;

        if (shootButtonImage != null)
        {
            shootButtonImage.sprite = value
                ? doubleDamageShootSprite
                : normalShootSprite;
        }
    }
}