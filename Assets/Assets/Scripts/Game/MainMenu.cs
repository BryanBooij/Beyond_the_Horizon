using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        ScreenFader.Instance.FadeToScene("Demo");
    }

    public void PlayLevel1()
    {
        ScreenFader.Instance.FadeToScene("Template");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}