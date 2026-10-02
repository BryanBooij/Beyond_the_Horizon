using System.Collections;
using Assets.Scripts.Game;
using Assets.Scripts.Enemies;
using UnityEngine;

public class EnemyBossShip : MonoBehaviour
{
    private Collider2D myCollider;
    private SpriteRenderer _spriteRenderer;

    [Header("Movement")]
    public float speed = 3f;
    public float waitTime = 2f;
    public float Points = 1000f;
    [HideInInspector] public bool movementPaused;

    [Header("Health")]
    public int health = 200;
    public EnemyHealthBar healthBar;
    public bool showHealthBarFromStart = true;
    private int maxHealth;

    [Header("Sprites")]
    public Sprite normalSprite;
    public Sprite damagedSprite;
    [Range(0f, 1f)] public float damagedSpriteThreshold = 0.5f;

    [Header("Rocket (bottom)")]
    [SerializeField] private GameObject rocketPrefab;
    [SerializeField] private Transform rocketFirePoint;
    [SerializeField] private float rocketCooldown = 4f;
    [SerializeField] private float rocketStartDelay = 2f;
    [SerializeField] private AudioClip rocketSound;

    [Header("Laser (top)")]
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Transform laserFirePoint;
    [SerializeField] private float laserCooldown = 1.5f;
    [SerializeField] private float laserStartDelay = 1f;
    [SerializeField] private AudioClip laserSound;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip hitSound;

    [Header("Explosion")]
    public GameObject explosionPrefab;

    private Vector2 destination;
    private float waitTimer;
    private bool isWaiting;
    private float minX, maxX, minY, maxY;
    private float rocketTimer;
    private float laserTimer;

    private void Awake()
    {
        myCollider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        maxHealth = health;
    }

    private void Start()
    {
        CalculateScreenBounds();
        ChooseRandomDestination();

        if (normalSprite != null)
            _spriteRenderer.sprite = normalSprite;

        if (healthBar != null)
        {
            healthBar.gameObject.SetActive(showHealthBarFromStart);
            if (showHealthBarFromStart)
                healthBar.SetHealth(health, maxHealth);
        }

        rocketTimer = rocketStartDelay;
        laserTimer = laserStartDelay;
    }

    private void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    private void HandleMovement()
    {
        if (movementPaused) return;
        if (isWaiting)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
            {
                isWaiting = false;
                ChooseRandomDestination();
            }
            return;
        }

        float multiplier = DifficultyManager.Instance != null
            ? DifficultyManager.Instance.enemySpeedMultiplier
            : 1f;

        transform.position = Vector2.MoveTowards(transform.position, destination, speed * multiplier * Time.deltaTime);

        if (Vector2.Distance(transform.position, destination) < 0.1f)
        {
            isWaiting = true;
            waitTimer = waitTime;
        }
    }

    private void HandleShooting()
    {
        if (movementPaused) return;
        rocketTimer -= Time.deltaTime;
        if (rocketTimer <= 0f)
        {
            Fire(rocketPrefab, rocketFirePoint, rocketSound);
            rocketTimer = rocketCooldown;
        }

        laserTimer -= Time.deltaTime;
        if (laserTimer <= 0f)
        {
            Fire(laserPrefab, laserFirePoint, laserSound);
            laserTimer = laserCooldown;
        }
    }

    private void Fire(GameObject prefab, Transform firePoint, AudioClip sound)
    {
        if (prefab == null || firePoint == null) return;

        Instantiate(prefab, firePoint.position, firePoint.rotation);

        if (audioSource != null && sound != null)
            audioSource.PlayOneShot(sound);
    }

    private void CalculateScreenBounds()
    {
        Camera cam = Camera.main;
        float cameraHeight = cam.orthographicSize;
        float cameraWidth = cameraHeight * cam.aspect;

        float halfWidth = 0f;
        float halfHeight = 0f;
        if (myCollider != null)
        {
            halfWidth = myCollider.bounds.extents.x;
            halfHeight = myCollider.bounds.extents.y;
        }

        minX = cam.transform.position.x + halfWidth;
        maxX = cam.transform.position.x + cameraWidth - halfWidth;
        minY = cam.transform.position.y - cameraHeight + halfHeight;
        maxY = cam.transform.position.y + cameraHeight - halfHeight;
    }

    private void ChooseRandomDestination()
    {
        Vector2 newDestination;
        do
        {
            newDestination = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        } while (ShootButtonCollider.Instance != null &&
                 ShootButtonCollider.Instance.OverlapPoint(newDestination));

        destination = newDestination;
    }

    private IEnumerator FlashRed()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.color = Color.white;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (healthBar != null)
        {
            healthBar.gameObject.SetActive(true);
            healthBar.SetHealth(health, maxHealth);
        }

        if (audioSource != null && hitSound != null)
            audioSource.PlayOneShot(hitSound);

        StartCoroutine(FlashRed());

        if (damagedSprite != null && health <= maxHealth * damagedSpriteThreshold)
            _spriteRenderer.sprite = damagedSprite;

        if (health <= 0)
        {
            ScoreHelper.AddPoints(Points);

            if (explosionPrefab != null)
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}