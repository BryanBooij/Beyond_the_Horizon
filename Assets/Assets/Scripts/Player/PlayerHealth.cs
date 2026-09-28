using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private Sprite normalSprite;
        [SerializeField] private Sprite halfHealthSprite;
        [SerializeField] private Sprite lowHealthSprite;
        [SerializeField] private GameObject shieldObject;
        private bool isImmune = false;
        public bool IsImmune => isImmune;
        public Slider healthBarSlider;
        public TextMeshProUGUI healthBarValueText;
        public GameObject deathScreen;
        public int maxHealth = 100;
        public int currentHealth;
        private SpriteRenderer spriteRenderer;
        private Color originalColor;
        private bool doubleDamage = false;
        public bool DoubleDamageActive => doubleDamage;
        [SerializeField] private float flashDuration = 0.1f;
        [SerializeField] private float shakeAmount = 0.1f;
        [SerializeField] private float shakeDuration = 0.1f;
        private Vector3 originalPosition;
        
        [Header("Audio")]
        public AudioSource audioSource;
        public AudioClip hitSound;
        public AudioClip shieldHitSound;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            currentHealth = maxHealth;
            deathScreen.SetActive(false);
            spriteRenderer = GetComponent<SpriteRenderer>();
            originalColor = spriteRenderer.color;
            UpdateHealthSprite();
            if (shieldObject != null)
            {
                shieldObject.SetActive(false);
            }
        }

        private void Update()
        {
            healthBarValueText.text = currentHealth + "/" + maxHealth;
            healthBarSlider.value = currentHealth;
            healthBarSlider.maxValue = maxHealth;
        }
        public void SetImmunity(bool value)
        {
            isImmune = value;
            shieldObject.SetActive(value);
        }
        public void SetDoubleDamage(bool value)
        {
            doubleDamage = value;
        }

        public void TakeDamage(int damage)
        {
            if (isImmune)
            {
                audioSource.PlayOneShot(shieldHitSound);
                return;
            }
            
            // Play hit sound
            if (audioSource != null && hitSound != null)
            {
                audioSource.PlayOneShot(hitSound);
            }
            
            FlashRed();
            StartCoroutine(Shake());
            currentHealth -= damage;
            UpdateHealthSprite();

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                healthBarValueText.text = currentHealth + "/" + maxHealth;
                healthBarSlider.value = currentHealth;
                Destroy(gameObject);
                deathScreen.SetActive(true);
                Time.timeScale = 0f;
            }
        }
        public void FlashRed()
        {
            StartCoroutine(FlashRedCoroutine());
            StartCoroutine(Shake());
        }

        private IEnumerator FlashRedCoroutine()
        {
            spriteRenderer.color = Color.red;

            yield return new WaitForSeconds(flashDuration);

            spriteRenderer.color = originalColor;
        }
        private IEnumerator Shake()
        {
            originalPosition = transform.localPosition;

            float elapsed = 0f;

            while (elapsed < shakeDuration)
            {
                float x = Random.Range(-shakeAmount, shakeAmount);
                float y = Random.Range(-shakeAmount, shakeAmount);

                transform.localPosition = originalPosition + new Vector3(x, y, 0f);

                elapsed += Time.deltaTime;

                yield return null;
            }

            transform.localPosition = originalPosition;
        }
        
        public void UpdateHealthSprite()
        {
            if (currentHealth > maxHealth * 0.5f)
            {
                spriteRenderer.sprite = normalSprite;
            }
            else if (currentHealth > maxHealth * 0.25f)
            {
                spriteRenderer.sprite = halfHealthSprite;
            }
            else
            {
                spriteRenderer.sprite = lowHealthSprite;
            }
        }
    }
}
