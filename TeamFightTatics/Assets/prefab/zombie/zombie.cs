using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie : Persona
{
    AutoBattle autoBattle;
    private void Awake()
    {
        autoBattle = GetComponent<AutoBattle>();
    }
    public override void Skill()
    {
        autoBattle.enemyPersona.CurrentHp -= Power;
        CurrentHp += Power;
    }
}
