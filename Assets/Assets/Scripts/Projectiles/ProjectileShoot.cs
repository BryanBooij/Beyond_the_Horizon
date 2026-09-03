using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float fireRate = 0.5f; // 0.5 seconds between shots
    public float nextFireTime = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && Time.time >= nextFireTime)
        {
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
    }
}
