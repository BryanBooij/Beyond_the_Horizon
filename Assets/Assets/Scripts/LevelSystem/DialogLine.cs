using System;
using UnityEngine;

public enum DialogueSpeaker
{
    Player,
    Enemy,
    System
}

[Serializable]
public class DialogueLine
{
    public DialogueSpeaker speaker;
    [TextArea(2, 5)]
    public string text;
    public float duration = 3f;
}