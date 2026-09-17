using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        private bool isImmune = false;
        public bool IsImmune => isImmune;
        public Slider healthBarSlider;
        public TextMeshProUGUI healthBarValueText;
        public GameObject deathScreen;
        public int maxHealth = 100;
        public int currentHealth;
        private SpriteRenderer spriteRenderer;
        private Color originalColor;
        [SerializeField] private float flashDuration = 0.1f;
        [SerializeField] private float shakeAmount = 0.1f;
        [SerializeField] private float shakeDuration = 0.1f;
        private Vector3 originalPosition;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            currentHealth = maxHealth;
            deathScreen.SetActive(false);
            spriteRenderer = GetComponent<SpriteRenderer>();
            originalColor = spriteRenderer.color;
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

            if (value)
            {
                spriteRenderer.color = Color.blue;
            }
            else
            {
                spriteRenderer.color = originalColor;
            }
        }

        public void TakeDamage(int damage)
        {
            if (isImmune)
                return;
            
            FlashRed();
            StartCoroutine(Shake());
            currentHealth -= damage;

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
    }
}
