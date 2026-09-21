using UnityEngine;
using UnityEngine.SceneManagement;

public class Victoryscreen : MonoBehaviour
{
    public GameObject continueButton;

    private void OnEnable()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        bool hasNextScene = currentIndex + 1 < SceneManager.sceneCountInBuildSettings;

        continueButton.SetActive(hasNextScene);
    }

    public void Restart()
    {
        Time.timeScale = 1f;

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void Continue()
    {
        Time.timeScale = 1f;

        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentIndex + 1);
        }
    }
}