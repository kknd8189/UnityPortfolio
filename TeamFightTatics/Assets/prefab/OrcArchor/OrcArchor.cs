using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcArchor : Persona
{
    public override void Skill()
    {
        Persona enemyPersona = GetComponent<AutoBattle>().enemyPersona;
        enemyPersona.CurrentHp -= Power;
        enemyPersona.gameObject.transform.position -= enemyPersona.gameObject.transform.forward * 20;
        enemyPersona.GetComponent<AutoBattle>().CharacterState = CharacterState.Chase;
    }
}
