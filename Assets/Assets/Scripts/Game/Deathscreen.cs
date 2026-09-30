using UnityEngine;
using UnityEngine.SceneManagement;
public class Deathscreen : MonoBehaviour
{
    public GameObject deathScreen;

    public void ShowDeathscreen()
    {
        deathScreen.SetActive(true);
    }

    public void HideDeathscreen()
    {
        deathScreen.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1f;

        Scene currentScene = SceneManager.GetActiveScene(); ScreenFader.Instance.FadeToScene(currentScene.name);
    }

    public void MainMenu()
    {
        ScreenFader.Instance.FadeToScene("Main Menu");
        Time.timeScale = 1f;
    }
}