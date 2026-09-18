using UnityEngine;

public class Enemyshoot : MonoBehaviour
{
    public GameObject laserPrefab;
    public float fireRate = 2f;
    
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip laserSound;

    private float nextFireTime = 0f;

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Instantiate(laserPrefab, transform.position, Quaternion.identity);
            
            // Laser sound when it is fired
            audioSource.PlayOneShot(laserSound);

            nextFireTime = Time.time + fireRate;
        }
    }
}