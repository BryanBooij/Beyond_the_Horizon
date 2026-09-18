using UnityEngine;

public class Enemyshoot : MonoBehaviour
{
    public GameObject laserPrefab;
    public float minFireRate = 4f;
    public float maxFireRate = 7f;

    private float nextFireTime = 0f;

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Instantiate(laserPrefab, transform.position, Quaternion.identity);

            nextFireTime = Time.time + Random.Range(minFireRate, maxFireRate);
        }
    }
}