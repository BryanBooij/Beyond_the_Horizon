using Assets.Scripts.Game;
using UnityEngine;

public class EnemySpaceship : MonoBehaviour
{
    public float speed = 200f;
    public float waitTime = 2f;
    public int health = 20;
    public float Points = 100f;

    private Vector2 destination;
    private float waitTimer;
    private bool isWaiting;
    
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    void Start()
    {
        CalculateScreenBounds();
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
    void CalculateScreenBounds()
    {
        // use main camera to adjust spawning for enemy ships so they always spawn the full length of the screen
        Camera cam = Camera.main;
        float cameraHeight = cam.orthographicSize;
        float cameraWidth = cameraHeight * cam.aspect;
        Collider2D enemyCollider = GetComponent<Collider2D>();
        float halfWidth = 0f;
        float halfHeight = 0f;

        // subtract enemy model so a half of the ship cannot be outside of the screen this keeps the enemies perfectly in frame
        if (enemyCollider != null)
        {
            halfWidth = enemyCollider.bounds.extents.x;
            halfHeight = enemyCollider.bounds.extents.y;
        }
        minX = cam.transform.position.x + halfWidth;
        maxX = cam.transform.position.x + cameraWidth - halfWidth;
        
        minY = cam.transform.position.y - cameraHeight + halfHeight;
        maxY = cam.transform.position.y + cameraHeight - halfHeight;
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