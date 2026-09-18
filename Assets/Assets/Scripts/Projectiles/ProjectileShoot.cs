using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileShoot : MonoBehaviour
{
    public GameObject[] projectilePrefab;
    public float fireRate = 0.5f; // 0.5 seconds between shots
    public float nextFireTime = 0f;
    
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip laserSound;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame && Time.time >= nextFireTime)
        {
            GameObject prefabToSpawnProjectile = projectilePrefab[Random.Range(0, projectilePrefab.Length)];
            Instantiate(prefabToSpawnProjectile, transform.position, Quaternion.identity);
            
            // Laser sound when it is fired
            audioSource.PlayOneShot(laserSound);
            
            nextFireTime = Time.time + fireRate;
        }
    }
}
