using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private AudioSource menuMusic;
    [SerializeField] private AudioSource gameplayMusic;
    [SerializeField] private AudioSource bossMusic;

    [SerializeField] private float fadeDuration = 1f;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayGameplayMusic()
    {
        StartCoroutine(CrossFade(menuMusic, gameplayMusic));
    }

    public void PlayMenuMusic()
    {
        StartCoroutine(CrossFade(gameplayMusic, menuMusic));
    }
    
    public void PlayBossMusic()
    {
        if (menuMusic.isPlaying)
        {
            StartCoroutine(CrossFade(menuMusic, bossMusic));
        }
        else if (gameplayMusic.isPlaying)
        {
            StartCoroutine(CrossFade(gameplayMusic, bossMusic));
        }
        else
        {
            bossMusic.Play();
        }
    }

    private IEnumerator CrossFade(AudioSource from, AudioSource to)
    {
        float startingVolume = from.volume;
        float targetVolume = to.volume;

        to.volume = 0f;
        to.Play();

        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            float t = time / fadeDuration;

            from.volume = Mathf.Lerp(startingVolume, 0f, t);
            to.volume = Mathf.Lerp(0f, targetVolume, t);

            yield return null;
        }

        from.Stop();
        from.volume = startingVolume;
        to.volume = targetVolume;
    }
}
