using UnityEngine;
using Assets.Scripts.Player;

public class Powerup : MonoBehaviour
{
    public PowerupEffect powerup;
    public float moveSpeed = 5f;
    private SpriteRenderer spriteRenderer;
    
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip pickupSound;
    
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
        if (collision.CompareTag("Player"))
        {
            // Play pickup sound
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            
            powerup.Apply(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("BulletBoundary"))
        {
            Destroy(gameObject);
        }
    }
}