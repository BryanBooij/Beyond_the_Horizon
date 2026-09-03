using System;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class Asteroid : MonoBehaviour
    {
        public float moveSpeed = 3f;
        void Update()
        {
            transform.Translate(Vector2.left * (moveSpeed * Time.deltaTime));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bullet"))
            {
                Destroy(gameObject);
                Destroy(collision.gameObject);
            }
        }
    }
}