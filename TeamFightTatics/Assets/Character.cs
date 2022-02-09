using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity , IAttack , ISkill
{
    public int CharacterNum;
    public int maxMP;
    public int defaultMP;
    public int attackRange;
    public int power;
    public int attackDelay;
    public int cost;

    public virtual void Attack() { }
    public virtual void Skill() { }
}
