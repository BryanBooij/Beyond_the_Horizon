using System;
using Assets.Scripts.Game;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class LargeAstroid : MonoBehaviour
    {
        private Powerupdropper powerupDropper;
        [Header("Astroid HP, speed and Damage")]
        public float moveSpeed = 200f;
        public float maxHP = 10;
        private float currentHP;
        public float Points = 20f;

        [Header("Astroid Sprites")]
        public Sprite Astroid1;
        public Sprite Astroid_Crack;

        [Header("Explosion")]
        public GameObject explosionPrefab;

        [Header("Astroid Rotation")]
        public float rotationSpeed = 45f;
        public bool randomDirection = true;

        private SpriteRenderer _spriteRenderer;

        void Start()
        {
            currentHP = maxHP;

            // Get the SpriteRenderer from the asteroid
            _spriteRenderer = GetComponent<SpriteRenderer>();

            // Start with the normal asteroid
            _spriteRenderer.sprite = Astroid1;

            if (randomDirection && UnityEngine.Random.value > 0.5f)
            {
                rotationSpeed *= -1f;
            }
        }
        private void Awake()
        {
            powerupDropper = GetComponent<Powerupdropper>();
        }

        void Update()
        {
            transform.Translate(
                Vector2.left * (moveSpeed * Time.deltaTime),
                Space.World
            );

            transform.Rotate(
                Vector3.forward * (rotationSpeed * Time.deltaTime)
            );
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Projectile_Lazer lazer = collision.GetComponent<Projectile_Lazer>();

            if (collision.CompareTag("Bullet"))
            {
                currentHP -= lazer.Damage;

                Destroy(collision.gameObject);

                // Asteroid still has HP left
                if (currentHP > 0)
                {
                    // Change to broken sprite at 5 HP
                    if (currentHP <= 5)
                    {
                        _spriteRenderer.sprite = Astroid_Crack;
                    }
                }
                // Asteroid has 0 HP
                else
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
                    powerupDropper.DropPowerUp();
                }
            }
        }
    }
}