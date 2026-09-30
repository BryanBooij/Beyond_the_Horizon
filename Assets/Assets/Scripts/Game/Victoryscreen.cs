using Assets.Scripts.LevelSystem;
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
        ScreenFader.Instance.FadeToScene(currentScene.name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        ScreenFader.Instance.FadeToScene("Main Menu");
    }

    public void Continue()
    {
        Time.timeScale = 1f;

        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            CurrentLevel.level++;

            string nextSceneName =
                SceneUtility.GetScenePathByBuildIndex(currentIndex + 1);

            nextSceneName = System.IO.Path.GetFileNameWithoutExtension(nextSceneName);

            ScreenFader.Instance.FadeToScene(nextSceneName);
        }
    }
}
