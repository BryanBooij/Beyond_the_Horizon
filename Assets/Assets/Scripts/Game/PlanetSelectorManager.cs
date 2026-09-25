using UnityEngine;
using UnityEngine.UI;

public class PlanetSelectorManager : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public GameObject[] planetPanels;   
    public Button[] planetButtons;      
    public Button continueButton;

    private GameObject selectedLevel;
    private Button selectedButton;

    void Start()
    {
        continueButton.interactable = false;
    }

    
    public void SelectPlanet(int index)
    {
        
        if (selectedButton != null)
            selectedButton.OnDeselect(null);

        selectedLevel = planetPanels[index];
        selectedButton = planetButtons[index];

        selectedButton.Select();
        continueButton.interactable = true;
    }

    
    public void ContinueToLevel()
    {
        if (selectedLevel == null) return;

        gameObject.SetActive(false);   
        selectedLevel.SetActive(true);
    }
}