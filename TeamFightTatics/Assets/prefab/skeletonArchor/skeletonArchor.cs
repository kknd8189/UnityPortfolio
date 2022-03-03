using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonArchor : Persona
{
 public override void Skill()
    {
        Persona enemyPersona = GetComponent<AutoBattle>().enemyPersona;
        enemyPersona.CurrentHp -= Power * 2;
    }
}
