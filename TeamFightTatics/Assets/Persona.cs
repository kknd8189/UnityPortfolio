using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Persona : Character
{
    private bool _isOnBattleField;
    public bool IsOnBattleField
    {
        get { return _isOnBattleField; }
        set { _isOnBattleField = value; }
    }
    private int _maxMp;
    public int MaxMp
    {
        get { return _maxMp; }
        set { _maxMp = value; }
    }
    private int _defaultMp;
    public int DefaultMp
    {
        get { return _defaultMp; }
        set { _defaultMp = value; }
    }
    private int _attackRange;
    public int AttackRange
    {
        get { return _attackRange; }
        set { _attackRange = value; }
    }
    private int _power;
    public int Power
    {
        get { return _power; }
        set { _power = value; }
    }
    private float _attackDelayTime;
    public float AttackDelayTime
    {
        get { return _attackDelayTime; }
        set { _attackDelayTime = value; }
    }

    [SerializeField]
    private int _diposedIndex;
    public int DiposedIndex
    {
        get { return _diposedIndex; }
        set { _diposedIndex = value; }
    }

    private void Update()
    {
        //return to initial state
        if (GameManager.Instance.GameState == GAMESTATE.Battle && GameManager.Instance.IsOver && IsOnBattleField)
        {
            CurrentHp = MaxHp;
            MaxMp = DefaultMp;
            transform.position = TileManager.Instance.BattleTileList[DiposedIndex].transform.position;
        }
    }
    public virtual void Skill() { }
}
