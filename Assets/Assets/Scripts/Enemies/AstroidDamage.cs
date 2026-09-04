using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class AstroidDamage : MonoBehaviour
    {
        public int damage;

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
            else if (collision.CompareTag("Boundary"))
            {
                Destroy(gameObject);
            }
        }
    }
}