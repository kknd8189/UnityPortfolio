using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IAttack
{
    void Attack(int power, float attackRange, float attackDelay);
}
public interface ISkill
{
    void Skill();
}