using system.Collections.generic;
using UnityEngine;
using UnityEngine.UI;
using Tmpro;
public class Healthbar : MonoBehaviour
{
    public Slider healthbar;

    public TextMeshProGUI;
    
    public int maxHealth;
    public int currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healtBarValueText.text = currentHealth.ToString() + "/" + maxHealth.toStrubg();

        HealthBarSlider.value = currentHealth;
        HealthBarSlider.maxValue = maxHealth;
    }
}
