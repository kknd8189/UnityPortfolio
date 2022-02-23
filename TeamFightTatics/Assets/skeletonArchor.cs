using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonArchor : Persona
{
    AutoBattle AutoBattle;
    private void Awake()
    {
        AutoBattle = GetComponent<AutoBattle>();
    }
    public override void Skill()
    {
        AutoBattle.Enemy.GetComponent<Persona>().CurrentHp -= Power * 2;
    }
}
