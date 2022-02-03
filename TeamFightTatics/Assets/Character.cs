using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity , IAttack , ISkill
{
    protected int MP;

    public virtual void Attack() { }
    public virtual void Skill() { }

}
