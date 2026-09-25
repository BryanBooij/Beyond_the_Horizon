using UnityEngine;

public class Storyscreen : MonoBehaviour
{
    public GameObject storyMenu;
    
    public void StoryMenu()
    {
        storyMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void Back()
    {
        storyMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
