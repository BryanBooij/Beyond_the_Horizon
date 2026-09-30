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

        ScreenFader.Instance.FadeToScene(sceneName);
    }

    public void ClosePopup()
    {
        levelPopup.SetActive(false);
    }

    private void RefreshLevelSlots()
    {
        string planetName = planets[selectedIndex].planetName;

        for (int i = 0; i < levelSlots.Length; i++)
        {
            int level = i + 1;
            LevelSlot slot = levelSlots[i];

            bool unlocked = LevelProgress.IsUnlocked(planetName, level);
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

    public void ResetLevelProgress()
    {
        LevelProgress.ResetAllProgress();

        RefreshPlanetLocks();
        if (selectedIndex >= 0)
            RefreshLevelSlots();
    }
}