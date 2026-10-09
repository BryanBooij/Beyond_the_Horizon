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
            transform.Translate(Vector2.left * (moveSpeed * DifficultyManager.Instance.astroidSpeedMultiplier * Time.deltaTime), Space.World); // projectile goes from spawn position to the left times movementspeed
            transform.Rotate(Vector3.forward * (rotationSpeed * Time.deltaTime)); // rotate png
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            ProjectileLaser laser = collision.GetComponent<ProjectileLaser>();
            if (collision.CompareTag("Bullet"))
            {
                if (currentHP > laser.Damage) // check if the current hp is higher then the damage a projectile lazer does
                {
                    currentHP -= laser.Damage;
                    Destroy(collision.gameObject);
                }
                else // else destroy astroid and lazer
                {
                    ScoreHelper.AddPoints(Points);
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