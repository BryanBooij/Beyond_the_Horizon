using UnityEngine;

public class AppLifeCycleManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;

    private void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
        {
            // App gaat naar de achtergrond
            AudioListener.pause = true;
            Time.timeScale = 0f;
        }
        else
        {
            // App komt terug naar de voorgrond
            AudioListener.pause = false;

            Time.timeScale = 0f;

            if (pauseMenuUI != null)
                pauseMenuUI.SetActive(true);
        }
    }
}