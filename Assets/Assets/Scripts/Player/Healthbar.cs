using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Healthbar : MonoBehaviour
{
    public Slider healthBarSlider;
    public TextMeshProUGUI healtBarValueText;
    
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
        healtBarValueText.text = currentHealth.ToString() + "/" + maxHealth.ToString();

        healthBarSlider.value = currentHealth;
        healthBarSlider.maxValue = maxHealth;
    }
}
