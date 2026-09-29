using Assets.Scripts.LevelSystem;
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
    public GameObject[] filledStars;
    public TMP_Text highscoreText;
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
        CurrentLevel.planet = planets[selectedIndex].planetName;
        CurrentLevel.level = levelIndex + 1;
        
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

            int earned = LevelProgress.GetStars(planetName, i + 1);

            for (int s = 0; s < levelSlots[i].filledStars.Length; s++)
            {
                levelSlots[i].filledStars[s].SetActive(s < earned);
            }

            if (levelSlots[i].highscoreText != null)
            {
                levelSlots[i].highscoreText.text = "Highscore:\n" + LevelProgress.GetHighscore(planetName, i + 1);
            }
            Debug.Log("Highscore " + planetName + " Level " + (i + 1) + ": " + LevelProgress.GetHighscore(planetName, i + 1));
        }
    }
    public void ResetLevelProgress()
    {
        LevelProgress.ResetAllProgress();
        
        if (selectedIndex >= 0)
            RefreshLevelSlots();
    }
}