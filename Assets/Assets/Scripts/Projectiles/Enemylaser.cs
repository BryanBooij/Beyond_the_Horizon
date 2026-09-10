using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Projectiles
{
    public class EnemyLaser : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public int damage = 5;

        void Update()
        {
            transform.Translate(
                Vector2.left * (moveSpeed * Time.deltaTime)
            );
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }

                Destroy(gameObject);
            }
            else if (collision.CompareTag("BulletBoundary"))
            {
                Destroy(gameObject);
            }
        }
    }
}