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
    
    void Start()
    {
        ScheduleNextShot();
    }

    void Update()
    {
        if (Time.time < nextFireTime) return;

        Shoot();
        ScheduleNextShot();
    }

    void Shoot()
    {
        Instantiate(laserPrefab, transform.position, Quaternion.identity);
        audioSource.PlayOneShot(laserSound);
    }

    void ScheduleNextShot()
    {
        nextFireTime = Time.time + Random.Range(minFireRate, maxFireRate);
    }
}