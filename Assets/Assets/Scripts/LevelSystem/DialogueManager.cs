using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[Serializable]
public class DialogueBox
{
    public GameObject panel;
    public TMP_Text text;
    public AudioClip typingSound;
    public float textSpeed = 0.05f;
}

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue Boxes")]
    [SerializeField] private DialogueBox playerBox;
    [SerializeField] private DialogueBox enemyBox;
    [SerializeField] private DialogueBox systemBox;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    private Coroutine dialogueCoroutine;

    public void ShowDialogue(DialogueLine line, Action onComplete = null)
    {
        StartRoutine(SingleRoutine(line, onComplete));
    }

    public void ShowSequence(List<DialogueLine> lines, Action onComplete = null)
    {
        StartRoutine(SequenceRoutine(lines, onComplete));
    }

    private void StartRoutine(IEnumerator routine)
    {
        if (dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine);
        }

        StopTypingSound();
        HideAll();

        dialogueCoroutine = StartCoroutine(routine);
    }

    private IEnumerator SingleRoutine(DialogueLine line, Action onComplete)
    {
        yield return PlayLine(line);

        dialogueCoroutine = null;
        onComplete?.Invoke();
    }

    private IEnumerator SequenceRoutine(List<DialogueLine> lines, Action onComplete)
    {
        if (lines != null)
        {
            foreach (DialogueLine line in lines)
            {
                yield return PlayLine(line);
            }
        }

        dialogueCoroutine = null;
        onComplete?.Invoke();
    }

    private IEnumerator PlayLine(DialogueLine line)
    {
        DialogueBox box = GetBox(line.speaker);

        HideAll();

        box.panel.SetActive(true);
        box.text.text = "";

        // Start typing sound
        if (audioSource != null && box.typingSound != null)
        {
            audioSource.clip = box.typingSound;
            audioSource.loop = true;
            audioSource.Play();
        }

        // Typewriter effect
        foreach (char letter in line.text)
        {
            box.text.text += letter;
            yield return new WaitForSeconds(box.textSpeed);
        }

        StopTypingSound();

        if (line.waitForInput)
        {
            // Keep the dialogue visible until external input continues it.
            yield return null;
        }
        else
        {
            // Normal dialogue keeps using its duration.
            yield return new WaitForSeconds(line.duration);

            box.panel.SetActive(false);
        }
    }

    private DialogueBox GetBox(DialogueSpeaker speaker)
    {
        switch (speaker)
        {
            case DialogueSpeaker.Player:
                return playerBox;

            case DialogueSpeaker.Enemy:
                return enemyBox;

            default:
                return systemBox;
        }
    }

    private void HideAll()
    {
        if (playerBox.panel != null)
            playerBox.panel.SetActive(false);

        if (enemyBox.panel != null)
            enemyBox.panel.SetActive(false);

        if (systemBox.panel != null)
            systemBox.panel.SetActive(false);
    }

    private void StopTypingSound()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.loop = false;
        }
    }
}