using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public GameObject Exp;
    public GameObject HP;

    public int level;
    public int maxExp;
    public int currentExp;
    public int Gold;

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
        currentExp = maxHP;
        level = 1;
        maxExp = 2;
        Gold = 0;
    }

    private void Update()
    {
        
    }
}
