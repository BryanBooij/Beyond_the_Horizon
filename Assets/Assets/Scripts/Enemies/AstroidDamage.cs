using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class AstroidDamage: MonoBehaviour
    {
        public int damage;
        public PlayerHealth playerHealth;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                playerHealth.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}