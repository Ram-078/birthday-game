using UnityEngine;

[CreateAssetMenu(fileName = "New NPC Dialogue", menuName = "NPC Dialogue")]
public class NPCDialogue : ScriptableObject
{
    public DialogueCharacter npcCharacter;

    public DialogueLine[] lines;

    public float typingSpeed = 0.05f;
}