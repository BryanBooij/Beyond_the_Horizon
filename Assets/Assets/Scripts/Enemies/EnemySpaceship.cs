using Assets.Scripts.Game;
using UnityEngine;

public class EnemySpaceship : MonoBehaviour
{
    public float speed = 200f;
    public float waitTime = 2f;
    public float minX = 650f;
    public float maxX = 940f;
    public float minY = 0f;
    public float maxY = 440f;
    public int health = 20;
    public float Points = 50f;
    private Vector2 destination;
    private float waitTimer;
    private bool isWaiting;

    void Start()
    {
        ChooseRandomDestination();
    }

    void Update()
    {
        if (isWaiting)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
            {
                isWaiting = false;
                ChooseRandomDestination();
            }
            return;
        }
        transform.position = Vector2.MoveTowards(
            transform.position,
            destination,
            speed * Time.deltaTime
        );
        if (Vector2.Distance(transform.position, destination) < 0.1f)
        {
            isWaiting = true;
            waitTimer = waitTime;
        }
    }
    void ChooseRandomDestination()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        destination = new Vector2(randomX, randomY);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            ScoreManager.Instance.AddPoints(Points);
            Destroy(gameObject);
        }
    }
}