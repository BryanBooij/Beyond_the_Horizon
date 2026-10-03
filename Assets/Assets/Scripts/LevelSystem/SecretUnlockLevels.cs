using UnityEngine.EventSystems;
using UnityEngine;

namespace Assets.Scripts.LevelSystem
{
    public class SecretUnlockLevels : MonoBehaviour, IPointerClickHandler
    {
        public PlanetSelectorManager manager;
        public int tapsRequired = 10;
        public float maxTimeBetweenTaps = 1f;

        private int taps;
        private float lastTapTime;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Time.unscaledTime - lastTapTime > maxTimeBetweenTaps)
                taps = 0;

            lastTapTime = Time.unscaledTime;
            taps++;

            if (taps >= tapsRequired)
            {
                taps = 0;
                manager.ActivateUnlockAll();
            }
        }
    }
}