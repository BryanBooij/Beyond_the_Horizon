using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader Instance { get; private set; }

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 0.35f;

    private bool isLoading;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FadeToScene(string sceneName)
    {
        if (isLoading)
            return;

        isLoading = true;

        StartCoroutine(FadeRoutine(sceneName));
    }

    private IEnumerator FadeRoutine(string sceneName)
    {
        // Input direct blokkeren
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = false;

        // Fade naar zwart
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Clamp01(t / fadeDuration);

            yield return null;
        }

        canvasGroup.alpha = 1f;

        // Scene laden
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);

        // Fade weer terug
        t = 0f;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            canvasGroup.alpha = 1f - Mathf.Clamp01(t / fadeDuration);

            yield return null;
        }

        canvasGroup.alpha = 0f;

        // Input weer vrijgeven
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = true;

        isLoading = false;
    }
}