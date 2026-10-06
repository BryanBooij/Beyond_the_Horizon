using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Boss
{
    public class BossAnimation : MonoBehaviour
    {
        public RectTransform leftImage;
        public RectTransform rightImage;

        public float animationTime = 2f;

        public float leftStartX = -2000f;
        public float rightStartX = 2000f;

        public float leftEndX = -530f;
        public float rightEndX = 530f;

        private void Start()
        {
            StartCoroutine(PlayVSAnimation());
        }

        private IEnumerator PlayVSAnimation()
        {
            // Starting positions
            Vector2 leftStart = new Vector2(leftStartX, 0);
            Vector2 rightStart = new Vector2(rightStartX, 0);

            Vector2 leftEnd = new Vector2(leftEndX, 0);
            Vector2 rightEnd = new Vector2(rightEndX, 0);

            leftImage.anchoredPosition = leftStart;
            rightImage.anchoredPosition = rightStart;

            float timer = 0f;

            while (timer < animationTime)
            {
                timer += Time.deltaTime;

                float t = timer / animationTime;
                
                t = Mathf.SmoothStep(0f, 1f, t);

                leftImage.anchoredPosition =
                    Vector2.Lerp(leftStart, leftEnd, t);

                rightImage.anchoredPosition =
                    Vector2.Lerp(rightStart, rightEnd, t);

                yield return null;
            }
            
            leftImage.anchoredPosition = leftEnd;
            rightImage.anchoredPosition = rightEnd;

            // Wait after the impact and deactivate
            yield return new WaitForSeconds(3f);
            gameObject.SetActive(false);
            
        }
    }
}

