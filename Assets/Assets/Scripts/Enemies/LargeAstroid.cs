using System;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class LargeAstroid : MonoBehaviour
    {
        [Header("Astroid HP and speed")]
        public float moveSpeed = 200f;
        public float maxHP = 10;
        private float currentHP;
        
        [Header("Astroid Rotation")]
        public float rotationSpeed = 45f; 
        public bool randomDirection = true;

        void Start()
        {
            currentHP = maxHP; // set HP on first iteration
            
            if (randomDirection && UnityEngine.Random.value > 0.5f)
            {
                rotationSpeed *= -1f; // spin the other way
            }
        }
        void Update()
        {
            transform.Translate(Vector2.left * (moveSpeed * Time.deltaTime), Space.World); // projectile goes from spawn position to the left times movementspeed
            transform.Rotate(Vector3.forward * (rotationSpeed * Time.deltaTime)); // rotate png
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