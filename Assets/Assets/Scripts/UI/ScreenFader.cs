using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader Instance;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 1f;

    private bool isLoading;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // new scene starts on blackscreen
        canvasGroup.alpha = 1f;

        StartCoroutine(FadeIn());
    }

    public void FadeToScene(string sceneName)
    {
        if (isLoading)
            return;

        isLoading = true;

        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    private IEnumerator FadeOutAndLoad(string sceneName)
    {
        float timer = 0f;
        float startAlpha = canvasGroup.alpha;
        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime;
            float progress = Mathf.Clamp01(timer / fadeDuration);

            // Smooth transition
            progress = progress * progress * (3f - 2f * progress);
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, progress);
            yield return null;
        }
        canvasGroup.alpha = 1f;
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeIn()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime;
            float progress = Mathf.Clamp01(timer / fadeDuration);
            // Smooth transition
            progress = progress * progress * (3f - 2f * progress);
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, progress);
            yield return null;
        }

        canvasGroup.alpha = 0f;
    }
}