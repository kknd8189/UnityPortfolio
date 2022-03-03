using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nana : Persona
{

    public override void Skill()
    {
        AutoBattle autobattle = GetComponent<AutoBattle>();

        for (int i = 0; i < autobattle.enemys.Count; i++)
        {
            autobattle.enemys[i].GetComponent<Persona>().CurrentHp -= Power;
        }
    }
}
