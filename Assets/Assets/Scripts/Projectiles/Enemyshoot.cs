using UnityEngine;

public class Enemyshoot : MonoBehaviour
{
    public GameObject laserPrefab;
    public float minFireRate = 4f;
    public float maxFireRate = 7f;
    
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

            nextFireTime = Time.time + Random.Range(minFireRate, maxFireRate);
        }
    }
}