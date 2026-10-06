using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance;

    [Header("Cutscene Image")]
    public GameObject cutsceneImage;

    [Header("Dialogue")]
    public DialogueManager dialogueManager;

    [Header("Start Cutscene")]
    public bool hasStartCutscene = false;
    public List<DialogueLine> startDialogueLines;

    [Header("End Cutscene")]
    public bool hasEndCutscene = false;
    public List<DialogueLine> endDialogueLines;

    private List<DialogueLine> currentDialogueLines;

    private int currentLineIndex = 0;
    private bool waitingForClick = false;
    private bool cutsceneFinished = false;

    private void Awake()
    {
        Instance = this;

        if (cutsceneImage != null)
        {
            cutsceneImage.SetActive(false);
        }
    }

    private void Start()
    {
        if (HasStartCutscene())
        {
            StartCutscene(startDialogueLines);
        }
    }

    public void StartCutscene(List<DialogueLine> lines)
    {
        if (lines == null || lines.Count == 0)
        {
            FinishCutscene();
            return;
        }

        currentDialogueLines = lines;
        currentLineIndex = 0;
        waitingForClick = false;
        cutsceneFinished = false;

        if (cutsceneImage != null)
        {
            cutsceneImage.SetActive(true);
        }

        PlayCurrentLine();
    }

    public void OnCutsceneImageClicked()
    {
        if (cutsceneFinished)
            return;

        if (!waitingForClick)
            return;

        waitingForClick = false;
        currentLineIndex++;

        if (currentLineIndex >= currentDialogueLines.Count)
        {
            FinishCutscene();
            return;
        }

        PlayCurrentLine();
    }

    private void PlayCurrentLine()
    {
        if (currentDialogueLines == null ||
            currentLineIndex >= currentDialogueLines.Count)
        {
            FinishCutscene();
            return;
        }

        waitingForClick = false;

        DialogueLine line = currentDialogueLines[currentLineIndex];

        dialogueManager.ShowDialogue(
            line,
            OnDialogueLineFinished
        );
    }

    private void OnDialogueLineFinished()
    {
        waitingForClick = true;
    }

    private void FinishCutscene()
    {
        if (cutsceneFinished)
            return;

        cutsceneFinished = true;
        waitingForClick = false;

        // Stop and hide all active dialogue boxes.
        if (dialogueManager != null)
        {
            dialogueManager.SendMessage(
                "HideAll",
                SendMessageOptions.DontRequireReceiver
            );
        }

        // Hide the cutscene image.
        if (cutsceneImage != null)
        {
            cutsceneImage.SetActive(false);
        }

        if (currentDialogueLines == startDialogueLines)
        {
            StartLevel();
        }
        else if (currentDialogueLines == endDialogueLines)
        {
            FinishLevelAfterCutscene();
        }
    }

    public bool HasStartCutscene()
    {
        return hasStartCutscene && HasDialogue(startDialogueLines);
    }

    public bool HasEndCutscene()
    {
        return hasEndCutscene && HasDialogue(endDialogueLines);
    }

    public void StartEndCutscene()
    {
        if (!HasEndCutscene())
        {
            FinishLevelAfterCutscene();
            return;
        }

        StartCutscene(endDialogueLines);
    }

    private bool HasDialogue(List<DialogueLine> lines)
    {
        return lines != null && lines.Count > 0;
    }

    private void StartLevel()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.StartLevelAfterCutscene();
        }
    }

    private void FinishLevelAfterCutscene()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.ShowVictoryScreen();
        }
    }
}