using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class NPC : MonoBehaviour, Interactable
{
    public NPCDialogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image dialogueImage;
    public GameObject fightButton;
    public GameObject teleportButton;
    public GameObject playerUnit;
    public GameObject teleportPrefab;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;
    private Animator animator;
    private Animator sceneAnimator;
    public bool CanInteract()
    {
        return !isDialogueActive;
    }

    public void Interact()
    {
        if (dialogueData == null || (PauseController.isPaused && !isDialogueActive))
            return;

        if (isDialogueActive)
            NextLine();
        else
            StartDialogue();
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;

        fightButton.SetActive(false);
        
        dialoguePanel.SetActive(true);
        PauseController.SetPause(true);

        ShowLine();
    }

    void ShowLine()
    {
        DialogueLine line = dialogueData.lines[dialogueIndex];
        DialogueCharacter speaker = line.speaker;

        nameText.SetText(speaker.characterName);
        dialogueImage.sprite = speaker.defaultSprite;
        StopAllCoroutines();
        StartCoroutine(TypeLine(line));
    }

    IEnumerator TypeLine(DialogueLine line)
    {
        isTyping = true;
        dialogueText.SetText("");

        DialogueCharacter speaker = line.speaker;

        foreach (char letter in line.text)
        {
            dialogueText.text += letter;

            if (speaker.voiceClip != null)
                SoundFxManager.PlayVoice(speaker.voiceClip, speaker.voicePitch);

            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;

        if (line.autoProgress)
        {
            yield return new WaitForSeconds(line.autoProgressDuration);
            NextLine();
        }
    }

    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.lines[dialogueIndex].text);
            isTyping = false;
        }

        dialogueIndex++;

        if (dialogueIndex < dialogueData.lines.Length)
        {
            ShowLine();
        }
        else
        {
            EndNPC endNPC = GetComponent<EndNPC>();

            if (endNPC != null)
            {
                SceneManager.LoadScene("END");
                return;
            }
            
            BossNPC bossNPC = GetComponent<BossNPC>();
            TeleportNPC teleportNPC = GetComponent<TeleportNPC>();
            if (bossNPC != null)
            {
                fightButton.SetActive(true);
            }
           else if (teleportNPC != null)
            {
                teleportButton.SetActive(true);
            }
            
            else
            {
                EndDialogue();
            }
        }
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;

        dialogueText.SetText("");
        dialoguePanel.SetActive(false);

        PauseController.SetPause(false);
    }
    
    public void StartBossBattle()
    {
        BossNPC bossNPC = GetComponent<BossNPC>();
        if (bossNPC != null)
        {
            SaveManager.Instance.playerPosition =
                GameObject.FindGameObjectWithTag("Player").transform.position;

            PauseController.SetPause(false);
            SceneManager.LoadScene(bossNPC.battleSceneName);
        }
    }
    public void Teleport()
    {
        StartCoroutine(TeleportRoutine());
    }

    IEnumerator TeleportRoutine()
    {
        Instantiate(teleportPrefab, playerUnit.transform.position+Vector3.down*0.3f, Quaternion.identity);
        SoundFxManager.Play("Teleport");
        
        yield return new WaitForSecondsRealtime(3f);

        TeleportNPC teleportNPC = GetComponent<TeleportNPC>();

        if (teleportNPC != null)
        {
            PauseController.SetPause(false);
            SceneManager.LoadScene(teleportNPC.SceneName);
        }
    }

    public void End()
    {
        StartCoroutine(EndRoutine());
    }
    
    IEnumerator EndRoutine()
    {
        
        yield return new WaitForSecondsRealtime(3f);
            PauseController.SetPause(false);
            SceneManager.LoadScene("END");
        
    }

}