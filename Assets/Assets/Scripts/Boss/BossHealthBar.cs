using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts.Boss
{
    public class BossHealthBar : MonoBehaviour
    {
        public Image fillImage;
        public TMP_Text nameLabel;
        public string bossName = "BOSS";

        private void Awake()
        {
            if (nameLabel != null)
                nameLabel.text = bossName;
        }

        public void SetHealth(int current, int max)
        {
            if (fillImage == null)
            {
                Debug.LogError("BossHealthBar: Fill Image is NOT assigned on " + name, this);
                return;
            }

            fillImage.fillAmount = max > 0 ? Mathf.Clamp01((float)current / max) : 0f;
        }
    }
}