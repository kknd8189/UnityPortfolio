using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : Entity
{
    public GameObject Exp;
    public GameObject HP;
    public GameObject Level;
    public GameObject Gold;

    private TextMeshProUGUI EXPtm;
    private TextMeshProUGUI HPtm;
    private TextMeshProUGUI Leveltm;
    private TextMeshProUGUI Goldtm;

    public int level;
    public int maxExp;
    public int currentExp;
    public int gold;

//    레벨 필요 경험치
//Lv.1  Lv.2	-
//Lv.2  Lv.3	2 XP
//Lv.3  Lv.4	6 XP
//Lv.4  Lv.5	10 XP
//Lv.5  Lv.6	20 XP
//Lv.6  Lv.7	36 XP
//Lv.7  Lv.8	56 XP
//Lv.8  Lv.9	80 XP

    private void Start()
    {
        maxHP = 100;
        currentHP = maxHP;
        currentExp = 0;
        level = 1;
        maxExp = 2;
        gold = 0;

        EXPtm = Exp.GetComponent<TextMeshProUGUI>();
        HPtm = HP.GetComponent<TextMeshProUGUI>();
        Leveltm = Level.GetComponent<TextMeshProUGUI>();
        Goldtm = Gold.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        earnGold();
        textUpdate();
    }

    private void earnGold()
    {

    }

    private void textUpdate()
    {
        EXPtm.text = currentExp.ToString() + "/" + maxExp.ToString();
        HPtm.text = currentHP.ToString();
        Goldtm.text = gold.ToString();
        Leveltm.text = "Level " + level.ToString();
    }
}
