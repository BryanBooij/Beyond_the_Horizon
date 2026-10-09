using UnityEngine;

public class AppLifeCycleManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;

    private void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
        {
            // App goes to background
            AudioListener.pause = false;
            Time.timeScale = 0f;
        }
        else
        {
            // App comes back
            AudioListener.pause = true;

            Time.timeScale = 0f;

            if (pauseMenuUI != null)
                pauseMenuUI.SetActive(true);
        }
    }
}