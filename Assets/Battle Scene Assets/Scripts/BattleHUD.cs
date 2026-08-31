using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public TMP_Text CurrentHP;
    public TMP_Text MaxHP;
    public TMP_Text nameText;
    public TMP_Text levelText;
    public Slider hpSlider;
    private string currentHpText;

    public void SetHUD(Unit unit)
    {
        CurrentHP.text = unit.currentHP.ToString();
        MaxHP.text = unit.maxHP.ToString();
        nameText.text = unit.unitName;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
        CurrentHP.text = hp.ToString();
    }

}