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

        // Typewriter effect
        foreach (char letter in text)
        {
            dialogueText.text += letter;

            yield return new WaitForSeconds(textSpeed);
        }

        // Wacht nadat de volledige tekst geschreven is
        yield return new WaitForSeconds(duration);

        dialoguePanel.SetActive(false);

        dialogueCoroutine = null;

        onComplete?.Invoke();
    }
}