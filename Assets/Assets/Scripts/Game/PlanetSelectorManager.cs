using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlanetSelector : MonoBehaviour
{
    [Header("Assign Scenes on the same spot as Buttons")]
    public string[] levelSceneNames;
    public Button[] planetButtons;

    public Button continueButton;

    private int selectedIndex = -1;
    private Button selectedButton;

    void Start()
    {
        continueButton.interactable = false;
    }
    
    public void SelectPlanet(int index)
    {
        if (selectedButton != null)
            selectedButton.OnDeselect(null);

        selectedIndex = index;
        selectedButton = planetButtons[index];

        selectedButton.Select();
        continueButton.interactable = true;
    }
    
    public void ContinueToLevel()
    {
        if (selectedIndex < 0) return;

        SceneManager.LoadScene(levelSceneNames[selectedIndex]);
    }
}