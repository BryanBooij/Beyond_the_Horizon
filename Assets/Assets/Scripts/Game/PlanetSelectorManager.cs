using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public class PlanetData
{
    public string planetName;
    public string[] levelSceneNames;
}

[System.Serializable]
public class LevelSlot
{
    public Button button;
    public GameObject lockIcon;
    public Image[] stars;
}

public class PlanetSelectorManager : MonoBehaviour
{
    [Header("Planets (same order as the buttons)")]
    public PlanetData[] planets;
    public Button[] planetButtons;

    public Button continueButton;

    [Header("Level popup")]
    public GameObject levelPopup;
    public TMP_Text popupTitle;
    public LevelSlot[] levelSlots;

    private int selectedIndex = -1;
    private Button selectedButton;

    void Start()
    {
        continueButton.interactable = false;
        levelPopup.SetActive(false);
        
        for (int i = 0; i < levelSlots.Length; i++)
        {
            int levelIndex = i; 
            levelSlots[i].button.onClick.AddListener(() => PlayLevel(levelIndex));
        }
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

        popupTitle.text = planets[selectedIndex].planetName;
        RefreshLevelSlots(); 
        levelPopup.SetActive(true);
    }

    void PlayLevel(int levelIndex)
    {
        string sceneName = planets[selectedIndex].levelSceneNames[levelIndex];
        SceneManager.LoadScene(sceneName);
    }
    
    public void ClosePopup()
    {
        levelPopup.SetActive(false);
    }
    
    void RefreshLevelSlots()
    {
        string planetName = planets[selectedIndex].planetName;

        for (int i = 0; i < levelSlots.Length; i++)
        {
            bool unlocked = LevelProgress.IsUnlocked(planetName, i + 1);

            levelSlots[i].button.interactable = unlocked;
            
            if (levelSlots[i].lockIcon != null)
                levelSlots[i].lockIcon.SetActive(!unlocked);
        }
    }
}