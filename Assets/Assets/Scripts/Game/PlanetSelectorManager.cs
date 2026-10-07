using Assets.Scripts.LevelSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class PlanetData
{
    public string planetName;
    public string[] levelSceneNames;
}

[System.Serializable]
public class LevelSlot
{
    public GameObject root;
    public Button button;
    public GameObject lockIcon;
    public GameObject[] filledStars;
    public TMP_Text highscoreText;
}

public class PlanetSelectorManager : MonoBehaviour
{
    [Header("Planets")]
    public PlanetData[] planets;
    public Button[] planetButtons;
    public GameObject[] planetLocks;
    
    [Header("Test Release")]
    // TEST RELEASE Planets beyond planet 1 stay locked for now remove later when levels are finished
    public int availablePlanets = 1;

    public Button continueButton;

    [Header("Level Popup")]
    public GameObject levelPopup;
    public TMP_Text popupTitle;
    public LevelSlot[] levelSlots;

    private int selectedIndex = -1;
    private Button selectedButton;

    private void Start()
    {
        continueButton.interactable = false;
        levelPopup.SetActive(false);

        RefreshPlanetLocks();
        for (int i = 0; i < levelSlots.Length; i++)
        {
            int levelIndex = i;
            levelSlots[i].button.onClick.AddListener(() => PlayLevel(levelIndex));
        }
    }

        void RefreshPlanetLocks()
    {
        for (int i = 0; i < planets.Length; i++)
        {
            bool unlocked = (i == 0) || LevelProgress.IsPlanetCompleted(planets[i - 1].planetName);
            
            // TEST RELEASE Planets beyond planet 1 stay locked for now remove later when levels are finished
            if (i >= availablePlanets) unlocked = false;
            
            // DEBUG: pretend all progress is complete
            if (CheatActive) unlocked = true;

            planetButtons[i].interactable = unlocked;

            if (i < planetLocks.Length && planetLocks[i] != null)
                planetLocks[i].SetActive(!unlocked);
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
        if (selectedIndex < 0)
            return;

        popupTitle.text = planets[selectedIndex].planetName;

        RefreshLevelSlots();

        levelPopup.SetActive(true);
    }

    private void PlayLevel(int levelIndex)
    {
        CurrentLevel.planet = planets[selectedIndex].planetName;
        CurrentLevel.level = levelIndex + 1;

        string sceneName = planets[selectedIndex].levelSceneNames[levelIndex];
        
        Debug.Log("Planet" + sceneName);

        if (sceneName == "Level_Final_Boss")
        {
            MusicManager.Instance.PlayBossMusic();
        }
        else
        {
            MusicManager.Instance.PlayGameplayMusic();
        }
        
        ScreenFader.Instance.FadeToScene(sceneName);
    }

    public void ClosePopup()
    {
        levelPopup.SetActive(false);
    }

    private void RefreshLevelSlots()
    {
        string planetName = planets[selectedIndex].planetName;
        int levelCount = planets[selectedIndex].levelSceneNames.Length;

        for (int i = 0; i < levelSlots.Length; i++)
        {
            LevelSlot slot = levelSlots[i];
            bool exists = i < levelCount;
            slot.root.SetActive(exists);
            if (!exists) continue;
            int level = i + 1;
            
            // DEBUG unlock all levels for testing
            bool unlocked = CheatActive || LevelProgress.IsUnlocked(planetName, level);
            
            // let the player unlock the levels
            // bool unlocked = LevelProgress.IsUnlocked(planetName, level);
            int stars = LevelProgress.GetStars(planetName, level);
            int highscore = LevelProgress.GetHighscore(planetName, level);

            // Lock
            slot.button.interactable = unlocked;

            if (slot.lockIcon != null)
                slot.lockIcon.SetActive(!unlocked);

            // Stars
            for (int s = 0; s < slot.filledStars.Length; s++)
            {
                slot.filledStars[s].SetActive(s < stars);
            }

            // Highscore
            if (slot.highscoreText != null)
            {
                slot.highscoreText.gameObject.SetActive(unlocked);
                slot.highscoreText.text = "Highscore:\n" + highscore;
            }
        }
    }
    
    [Header("Debug")]
    public bool unlockEverything = false;
    
    private const string CheatKey = "UnlockAllCheat";
    private bool CheatActive => unlockEverything || PlayerPrefs.GetInt(CheatKey, 0) == 1;

    public void ActivateUnlockAll()
    {
        PlayerPrefs.SetInt(CheatKey, 1);
        PlayerPrefs.Save();

        RefreshPlanetLocks();
        if (selectedIndex >= 0)
            RefreshLevelSlots();
    }

    [Header("Reset Confirmation")]
    public GameObject resetConfirmPopup;
    
    public void ShowResetConfirmation()
    {
        resetConfirmPopup.SetActive(true);
    }

    public void ConfirmReset()
    {
        PlayerPrefs.DeleteKey(CheatKey);
        LevelProgress.ResetAllProgress();

        RefreshPlanetLocks();
        if (selectedIndex >= 0)
            RefreshLevelSlots();

        resetConfirmPopup.SetActive(false);
    }
    
    public void CancelReset()
    {
        resetConfirmPopup.SetActive(false);
    }
}