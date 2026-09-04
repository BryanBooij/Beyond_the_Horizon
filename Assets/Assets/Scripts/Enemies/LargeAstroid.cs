using System;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class LargeAstroid : MonoBehaviour
    {
        public float moveSpeed = 200f;
        public float maxHP = 10;
        private float currentHP;

        void Start()
        {
            currentHP = maxHP; // set HP on first iteration
        }
        void Update()
        {
            transform.Translate(Vector2.left * (moveSpeed * Time.deltaTime)); // projectile goes from spawn position to the left times movementspeed
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Projectile_Lazer lazer = collision.GetComponent<Projectile_Lazer>();
            if (collision.CompareTag("Bullet"))
            {
                if (currentHP > lazer.Damage) // check if the current hp is higher then the damage a projectile lazer does
                {
                    currentHP -= lazer.Damage;
                    Destroy(collision.gameObject);
                }
                else // else destroy astroid and lazer
                {
                    Destroy(gameObject);
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}