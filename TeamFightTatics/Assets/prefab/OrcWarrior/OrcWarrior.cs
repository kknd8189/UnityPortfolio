using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcWarrior : Persona
{
    public override void Skill()
    {
        Persona enemyPersona = GetComponent<AutoBattle>().enemyPersona;
        enemyPersona.CurrentHp -= CurrentHp / 2;
    }
  
}
