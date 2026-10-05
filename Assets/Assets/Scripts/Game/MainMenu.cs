using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        MusicManager.Instance.PlayGameplayMusic();
        ScreenFader.Instance.FadeToScene("Demo");
    }

    public void PlayLevel1()
    {
        MusicManager.Instance.PlayGameplayMusic();
        ScreenFader.Instance.FadeToScene("Template");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}