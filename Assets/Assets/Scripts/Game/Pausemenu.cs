using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausemenu : MonoBehaviour
{
    public GameObject pauseMenu;
    [Header("Level Data")]
    public LevelData levelData;

    [Header("Star Score Text")]
    public TMP_Text oneStarScoreText;
    public TMP_Text twoStarScoreText;
    public TMP_Text threeStarScoreText;

    public void Pause()
    {
        UpdateStarScores();
        AudioListener.pause = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        AudioListener.pause = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        AudioListener.pause = false;
        Time.timeScale = 1f;
        MusicManager.Instance.PlayMenuMusic();
        ScreenFader.Instance.FadeToScene("Main Menu");
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        MusicManager.Instance.PlayGameplayMusic();
        Scene currentScene = SceneManager.GetActiveScene();
        ScreenFader.Instance.FadeToScene(currentScene.name);
    }

    private void UpdateStarScores()
    {
        if (levelData == null)
            return;

        oneStarScoreText.text = levelData.oneStarScore.ToString();
        twoStarScoreText.text = levelData.twoStarScore.ToString();
        threeStarScoreText.text = levelData.threeStarScore.ToString();
    }
}