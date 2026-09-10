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
        SceneManager.LoadSceneAsync("Playerhealth");
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }
}