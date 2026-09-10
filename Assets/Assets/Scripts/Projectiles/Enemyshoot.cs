using UnityEngine;

public class Enemyshoot : MonoBehaviour
{
    public GameObject laserPrefab;
    public float fireRate = 2f;

    private float nextFireTime = 0f;

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Instantiate(laserPrefab, transform.position, Quaternion.identity);

            nextFireTime = Time.time + fireRate;
        }
    }
}