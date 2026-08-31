using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter speaker;

    [TextArea(2, 5)]
    public string text;


    public bool autoProgress;
    public float autoProgressDuration = 1.5f;
}