using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity , IAttack , ISkill
{

    public bool isOnBattleField;
    public int CharacterNum;
    public int maxMP;
    public int defaultMP;
    public int attackRange;
    public int power;
    public int attackDelay;
    public int cost;
    private int _cardIndex;
    public int CardIndex 
    { 
        get { return _cardIndex; }
        set { _cardIndex = value; }
    }
    [SerializeField]
    private int _diposedIndex;
    public int DiposedIndex
    {
        get { return _diposedIndex; }
        set { _diposedIndex = value; }
    }
    public virtual void Attack() { }
    public virtual void Skill() { }
    public void Summon()
    {
        PoolManager.Instance.SummonHelper(CharacterNum, _cardIndex);
    }
    private void Update()
    {
        //return to initial state
        if (GameManager.Instance.GameState == GAMESTATE.Battle && GameManager.Instance.IsOver && isOnBattleField)
        {
            CurrentHp = MaxHp;
            maxMP = defaultMP;
            transform.position = SetTile.Instance.BattleTileList[_diposedIndex].transform.position;
        }
    }
}
