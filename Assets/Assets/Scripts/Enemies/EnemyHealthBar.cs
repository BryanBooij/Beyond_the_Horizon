using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class EnemyHealthBar : MonoBehaviour
    {
        public Transform fillPivot;

        private float fullWidth;

        private void Awake()
        {
            fullWidth = fillPivot.localScale.x;
        }

        public void SetHealth(int current, int max)
        {
            float percent = Mathf.Clamp01((float)current / max);

            Vector3 scale = fillPivot.localScale;
            scale.x = fullWidth * percent;
            fillPivot.localScale = scale;
        }
    }
}