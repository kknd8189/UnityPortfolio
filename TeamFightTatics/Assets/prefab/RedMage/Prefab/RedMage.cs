using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedMage : Persona
{
    public GameObject HealZone;
    public override void Skill()
    {
        GameObject healZone = Instantiate(HealZone, transform.position, new Quaternion(0, 0, 0, 0));
        healZone.tag = tag;
    }
}
