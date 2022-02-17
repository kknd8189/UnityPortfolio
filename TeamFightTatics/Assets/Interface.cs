using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IAttack
{
    void Attack(int power, int attackRange, int attackDelay);
}
public interface ISkill
{
    void Skill();
}