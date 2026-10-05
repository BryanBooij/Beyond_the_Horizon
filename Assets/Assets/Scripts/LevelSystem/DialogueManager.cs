using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;

    [Header("Typewriter")]
    [SerializeField] private float textSpeed = 0.05f;
    
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip typingSound;

    private Coroutine dialogueCoroutine;

    public void ShowDialogue(
        string text,
        float duration,
        Action onComplete)
    {
        if (dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine);
        }

        dialogueCoroutine = StartCoroutine(
            DialogueRoutine(text, duration, onComplete)
        );
    }

    private IEnumerator DialogueRoutine(
        string text,
        float duration,
        Action onComplete)
    {
        dialoguePanel.SetActive(true);

        dialogueText.text = "";
        
        // Start typing sound
        if (audioSource != null && typingSound != null)
        {
            audioSource.clip = typingSound;
            audioSource.loop = true;
            audioSource.Play();
        }

        // Typewriter effect
        foreach (char letter in text)
        {
            dialogueText.text += letter;

            yield return new WaitForSeconds(textSpeed);
        }
        
        // Stop typing sound
        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.loop = false;
        }

        // Wait until the full text is written
        yield return new WaitForSeconds(duration);

        dialoguePanel.SetActive(false);

        dialogueCoroutine = null;

        onComplete?.Invoke();
    }
}