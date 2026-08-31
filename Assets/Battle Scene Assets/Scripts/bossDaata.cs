
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boss", menuName = "Battle/Boss Data")]
public class BossData :  ScriptableObject
{
  
    public string bossID;
    public string returnScene;
    public string bossName;
    public float waitDuration;
    [Header("Stats")]
    public int maxHP = 20;
    public int damage = 5;
    public List<ActionData> actions;
    
    [Header("Dialogue")]
    [TextArea]
    public List<string> introLines;

    [TextArea]
    public List<string> hitLines;

    [TextArea]
    public List<string> healLines;

    [TextArea]
    public List<string> defeatLines;
    
    
}


