using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("Demo");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}