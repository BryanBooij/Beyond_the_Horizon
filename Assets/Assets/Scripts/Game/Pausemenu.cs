using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausemenu : MonoBehaviour
{
    public GameObject pauseMenu;
    
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        MusicManager.Instance.PlayMenuMusic();
        ScreenFader.Instance.FadeToScene("Main Menu");
    }

    public void Restart()
    {
        Time.timeScale = 1f;

        Scene currentScene = SceneManager.GetActiveScene();
        ScreenFader.Instance.FadeToScene(currentScene.name);
    }
}