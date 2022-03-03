using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie : Persona
{
    public override void Skill()
    {
        Persona enemyPersona = GetComponent<AutoBattle>().enemyPersona;

        enemyPersona.CurrentHp -= Power;
        CurrentHp += Power;
    }
}
