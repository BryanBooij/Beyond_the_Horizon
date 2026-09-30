using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader Instance { get; private set; }

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 0.5f;

    private bool isLoading;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
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
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = false;

        // Fade naar zwart
        yield return Fade(0f, 1f);

        Time.timeScale = 1f;

        // Scene laden
        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        // Begin volledig zwart
        canvasGroup.alpha = 1f;

        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = false;

        // Fade van zwart naar de nieuwe scene
        yield return Fade(1f, 0f);

        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = true;

        isLoading = false;
    }

    private IEnumerator Fade(float from, float to)
    {
        float timer = 0f;

        canvasGroup.alpha = from;

        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime;

            float progress = Mathf.Clamp01(timer / fadeDuration);

            // Smooth easing
            progress = progress * progress * (3f - 2f * progress);

            canvasGroup.alpha = Mathf.Lerp(from, to, progress);

            yield return null;
        }

        canvasGroup.alpha = to;
    }
}