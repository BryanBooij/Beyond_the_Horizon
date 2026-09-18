using UnityEngine;
using Assets.Scripts.Player;

public class Powerup : MonoBehaviour
{
    public PowerupEffect powerup;
    public float moveSpeed = 5f;
    private SpriteRenderer spriteRenderer;
    
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (powerup != null)
        {
            spriteRenderer.sprite = powerup.icon;
        }
    }
    private void Update()
    {
        transform.Translate(
            Vector2.left * (moveSpeed * Time.deltaTime),
            Space.World
        );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth player = collision.GetComponent<PlayerHealth>();

        if (player == null)
            return;

        powerup.Apply(collision.gameObject);
        Destroy(gameObject);
    }
}