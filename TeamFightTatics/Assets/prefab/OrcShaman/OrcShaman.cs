using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcShaman : Persona
{
    public override void Skill()
    {
        Persona enemyPersona = GetComponent<AutoBattle>().enemyPersona;
        enemyPersona.CurrentHp -= Power * 10;
    }
}
