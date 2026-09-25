using UnityEngine;

public class Storyscreen : MonoBehaviour
{
    public GameObject storyMenu;
    
    public void StoryMenu()
    {
        storyMenu.SetActive(true);
    }
    
    public void Back()
    {
        storyMenu.SetActive(false);
    }
}
