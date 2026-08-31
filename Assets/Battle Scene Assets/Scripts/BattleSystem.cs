using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public BossData bossData;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    [Header("Player Actions")]
    [SerializeField] private int attackDamage = 5;
    [SerializeField] private int healAmount = 5;
    [SerializeField] private int biteDamage = 10;
    [SerializeField] private int hugPower = 20;
    [SerializeField] private int kissPower = 30;
    

    private Animator animator;
    private Animator eneanimator;
    private int hitLineIndex = 0;
    private Unit playerUnit;
    private Unit enemyUnit;

    public TMP_Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;
    [SerializeField] private GameObject clawEffectPrefab;
    [SerializeField] private GameObject biteEffectPrefab;
    [SerializeField] private GameObject healEffectPrefab;
    [SerializeField] private GameObject hugEffectPrefab;
    [SerializeField] private GameObject kissEffectPrefab;
    public GameObject GameOverScreen;



    public BattleState state;

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation.position+Vector3.up, playerBattleStation.rotation);
        playerUnit = playerGO.GetComponent<Unit>();
        animator = playerGO.GetComponent<Animator>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation.position+Vector3.up,enemyBattleStation.rotation);
        enemyUnit = enemyGO.GetComponent<Unit>();
        eneanimator = enemyGO.GetComponent<Animator>();


        enemyUnit.unitName = bossData.bossName;
        enemyUnit.maxHP = bossData.maxHP;
        enemyUnit.currentHP = bossData.maxHP;
        enemyUnit.damage = bossData.damage;

        if (bossData.introLines.Count > 0)
        {
            dialogueText.text = bossData.introLines[0];
            yield return new WaitForSeconds(bossData.waitDuration);
        }
        else
        {
            dialogueText.text = enemyUnit.unitName + " approaches...";
        }

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(1f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    string GetRandomHitLine()
    {
        if (bossData.hitLines.Count == 0)
            return "The attack landed!";

        string line = bossData.hitLines[hitLineIndex];

        hitLineIndex++;

        if (hitLineIndex >= bossData.hitLines.Count)
            hitLineIndex = 0;

        return line;
    }
    
    string GetRandomHealLine()
    {
        if (bossData.healLines.Count == 0)
            return " ";

        return bossData.healLines[Random.Range(0, bossData.healLines.Count)];
    }

    IEnumerator PlayerAttack()
    {
        
        bool isDead = enemyUnit.TakeDamage(attackDamage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        
        dialogueText.text = "You scratch "+enemyUnit.unitName+"!" ;
        
        SoundFxManager.Play("Attack");
        
        Instantiate(clawEffectPrefab, enemyUnit.transform.position, Quaternion.identity);

        eneanimator.SetTrigger("Hurt");


        yield return new WaitForSeconds(1f);
        dialogueText.text = GetRandomHitLine();

        eneanimator.SetTrigger("Idle");

        yield return new WaitForSeconds(2f);


        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerHeal()
    {
        Instantiate(healEffectPrefab, playerUnit.transform.position, Quaternion.identity);
        
        playerUnit.Heal(healAmount);

        playerHUD.SetHP(playerUnit.currentHP);
        
        SoundFxManager.Play("Heal");
        animator.SetTrigger("IsHeal");
        
        dialogueText.text = "You feel hydrated!";

        
        yield return new WaitForSeconds(1.5f);

        animator.SetTrigger("IsIdle");

        dialogueText.text = GetRandomHealLine();

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator PlayerBite()
    {
        Instantiate(biteEffectPrefab, enemyUnit.transform.position, Quaternion.identity);
        
        SoundFxManager.Play("Bite");

        bool isDead = enemyUnit.TakeDamage(biteDamage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        
        dialogueText.text = "You bite "+enemyUnit.unitName+"!" ;
        eneanimator.SetTrigger("Hurt");
        
        yield return new WaitForSeconds(1.5f);


        dialogueText.text = GetRandomHitLine();
        eneanimator.SetTrigger("Idle");


        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerHug()
    
    {
        Instantiate(hugEffectPrefab, enemyUnit.transform.position, Quaternion.identity);
        SoundFxManager.Play("Hug");
        
        bool isDead = enemyUnit.TakeDamage(hugPower);

        enemyHUD.SetHP(enemyUnit.currentHP);

        dialogueText.text = "You give "+enemyUnit.unitName+" warm hug.";

        yield return new WaitForSeconds(1.5f);
        dialogueText.text = GetRandomHitLine();
        yield return new WaitForSeconds(1.5f);


        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerKiss()
    {
        Instantiate(kissEffectPrefab, enemyUnit.transform.position, Quaternion.identity);
        SoundFxManager.Play("Kiss");

        bool isDead = enemyUnit.TakeDamage(kissPower);

        enemyHUD.SetHP(enemyUnit.currentHP);

        dialogueText.text = "You give "+enemyUnit.unitName+" kiss.";
        yield return new WaitForSeconds(1.5f);
        dialogueText.text = GetRandomHitLine();
        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        Instantiate(clawEffectPrefab, playerUnit.transform.position, Quaternion.identity);

        SoundFxManager.Play("Attack");

        dialogueText.text = enemyUnit.unitName + " attacks!";
        animator.SetTrigger("IsHurt");
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("IsIdle");


        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }
    
    IEnumerator WinRoutine()
    {
        SaveManager.Instance.defeatedBosses.Add(bossData.bossID);
        MusicManager.PauseBGM();
        SoundFxManager.Play("Win");
            
        animator.SetTrigger("Dance");

        eneanimator.SetTrigger("Death");

        dialogueText.text = "You won the battle!";
        yield return new WaitForSeconds(4f);
            
        SceneManager.LoadScene(bossData.returnScene);
    }
    
    IEnumerator LoseRoutine()
    {
        MusicManager.PauseBGM();

        SoundFxManager.Play("Lose");

        animator.SetTrigger("IsDead");
        dialogueText.text = "You were defeated.";
        yield return new WaitForSeconds(4f);
        GameOverScreen.SetActive(true);
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            StartCoroutine(WinRoutine());
        }
        else if (state == BattleState.LOST)
        {
            StartCoroutine(LoseRoutine());
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action:";
    }

    public void OnAttackButton()
    {
        
        if (state != BattleState.PLAYERTURN)
            return;
        state = BattleState.ENEMYTURN;

        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        state = BattleState.ENEMYTURN;
        StartCoroutine(PlayerHeal());
    }

    public void OnBiteButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        state = BattleState.ENEMYTURN;
        StartCoroutine(PlayerBite());
    }

    public void OnHugButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        state = BattleState.ENEMYTURN;
        StartCoroutine(PlayerHug());
    }

    public void OnKissButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        state = BattleState.ENEMYTURN;
        StartCoroutine(PlayerKiss());
    }
}