using System;
using Assets.Scripts.Game;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    
    public class Asteroid : MonoBehaviour
    {
        private Powerupdropper powerupDropper;
        [Header("Astroid HP, speed and Damage")]
        public float moveSpeed = 5f;
        public float maxHP = 5;
        private float currentHP;
        public float Points = 10f;
        
        [Header("Astroid Rotation")]
        public float rotationSpeed = 90f; 
        public bool randomDirection = true;
        
        [Header("Explosion")]
        public GameObject explosionPrefab;

        void Start()
        {
            currentHP = maxHP; // set HP on first iteration
            
            if (randomDirection && UnityEngine.Random.value > 0.5f)
            {
                rotationSpeed *= -1f; // spin the other way
            }
        }
        private void Awake()
        {
            powerupDropper = GetComponent<Powerupdropper>();
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
                    ScoreManager.Instance.AddPoints(Points);
                    if (explosionPrefab != null)
                    {
                        Instantiate(
                            explosionPrefab,
                            transform.position,
                            Quaternion.identity
                        );
                    }
                    Destroy(gameObject);
                    Destroy(collision.gameObject);
                    powerupDropper.DropPowerUp();
                }
            }
        }
    }
}