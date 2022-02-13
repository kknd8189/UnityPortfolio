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
    private int _cardIndex;

    public bool isOnBattleField;

    public int CardIndex 
    { 
        get { return _cardIndex; }
        set { _cardIndex = value; }
    }

    public virtual void Attack() { }
    public virtual void Skill() { }
    public void Summon()
    {
        PoolManager.Instance.SummonHelper(CharacterNum, _cardIndex);
    }
}
