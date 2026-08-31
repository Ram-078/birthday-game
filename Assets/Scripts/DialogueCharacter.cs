using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Character")]
public class DialogueCharacter : ScriptableObject
{
    public string characterName;
    public Sprite defaultSprite;

    public AudioClip voiceClip;
    public float voicePitch = 1f;
}