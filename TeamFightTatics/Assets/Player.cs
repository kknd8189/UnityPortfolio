using TMPro;
using UnityEngine;

public class Player : Entity
{
    public TextMeshProUGUI EXPtm;
    public TextMeshProUGUI HPtm;
    public TextMeshProUGUI Leveltm;
    public TextMeshProUGUI Goldtm;

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
        Goldtm.text = gold.ToString();
        Leveltm.text = "Level " + level.ToString();
        EXPtm.text = currentExp.ToString() + "/" + maxExp.ToString();
    }
    private void Update()
    {
        earnGold();
        levelToExp();
    }
    private void earnGold()
    {
        int interest;
        interest = Mathf.FloorToInt(gold / 10);
        if (interest >= 5) interest = 5;

        if (GameManager.Instance.IsOver && GameManager.Instance.GameState == GAMESTATE.Battle)
        {
            gold += 4 + interest;
            if(level != 9) currentExp += 2;
            Goldtm.text = gold.ToString();

        }
    }

    private void levelToExp()
    {
        if (currentExp >= maxExp)
        {
         
            currentExp =  currentExp - maxExp;
            Leveltm.text = "Level " + level.ToString();

            switch (level)
            {
                case 1:
                    maxExp = 2;
                    break;
                case 2:
                    maxExp = 6;
                    break;
                case 3:
                    maxExp = 10;
                    break;
                case 4:
                    maxExp = 20;
                    break;
                case 5:
                    maxExp = 36;
                    break;
                case 6:
                    maxExp = 56;
                    break;
                case 7:
                    maxExp = 80;
                    break;
            }

            level += 1;

        }
    }
    public void BuyExp()
    {
        if(gold >= 4 && level <= 8) 
        {
            currentExp += 4;
            gold -= 4;
            Goldtm.text = gold.ToString();

            if (level == 9) EXPtm.text = "MAX";
            else EXPtm.text = currentExp.ToString() + "/" + maxExp.ToString();
            Leveltm.text = "Level " + level.ToString();
        }
    }
}
