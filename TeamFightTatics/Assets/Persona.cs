using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Persona : Character
{
    [SerializeField]
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
    [SerializeField]
    private int _currentMp;
    public int CurrentMp
    {
        get { return _currentMp; }
        set { _currentMp = value; }
    }
    [SerializeField]
    private float _attackRange;
    public float AttackRange
    {
        get { return _attackRange; }
        set { _attackRange = value; }
    }
    [SerializeField]
    private int _power;
    public int Power
    {
        get { return _power; }
        set { _power = value; }
    }
    [SerializeField]
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
    [SerializeField]
    protected float _speed;
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }
    public virtual void Skill() { }
    public override void Damaged(int damage)
    {
        Debug.Log("¾ÆÆÌ");
        CurrentHp -= damage;
    }
}
