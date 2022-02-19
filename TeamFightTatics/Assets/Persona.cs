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
    private int _currentMp;
    public int CurrentMp
    {
        get { return _currentMp; }
        set { _currentMp = value; }
    }
    private float _attackRange;
    public float AttackRange
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
    public virtual void Skill() { }
    protected void Die()
    {
        if (_currentHp <= 0)
        {
            if (gameObject.tag == "PlayerCharacter") player.LiveCharacterCount--;
            else if (gameObject.tag == "EnemyCharacter") enemy.LiveEnemyCount--;
        }
    }
}
