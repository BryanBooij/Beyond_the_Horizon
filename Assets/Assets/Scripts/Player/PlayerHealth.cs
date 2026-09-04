using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public Slider healthBarSlider;
        public TextMeshProUGUI healthBarValueText;

        public int maxHealth = 100;
        public int currentHealth;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            currentHealth = maxHealth;
        }

        private void Update()
        {
            healthBarValueText.text = currentHealth + "/" + maxHealth;
            healthBarSlider.value = currentHealth;
            healthBarSlider.maxValue = maxHealth;
        }

        public void TakeDamage(int damage) 
        { 
            currentHealth -= damage;
            
            if (currentHealth <= 0) 
            { 
                currentHealth = 0;
                
                Destroy(gameObject); 
            } 
        }
    }
}
