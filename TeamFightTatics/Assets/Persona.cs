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
    [SerializeField]
    private int _maxMp;
    public int MaxMp
    {
        get { return _maxMp; }
        set { _maxMp = value; }
    }
    [SerializeField]
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
    protected int _recoverMp = 10;
    public int RecoverMp
    {
        get { return _recoverMp; }
        set { _recoverMp = value; }
    }
    private bool[] _isSynergyOn = {false , false};
    public bool[] IsSynergyOn
    {
        get { return _isSynergyOn; }
        set { _isSynergyOn = value; }
    }
    [SerializeField]
    private int[] _synergyNumber = { 0, 0 };
    public int[] SynergyNumber
    {
        get { return _synergyNumber; }
        set { _synergyNumber = value; }
    }

    protected string _characterName;

    protected string _skillExplain;
    public string CharacterName
    {
        get { return _characterName; }
        set { _characterName = value; }
    }
    public string SkillExplain
    {
        get { return _skillExplain; }
        set { _skillExplain = value; }
    }

    protected void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            UIManager.Instance.ExplainCharacter(CharacterName, _synergyNumber, Cost, SkillExplain, CurrentHp, Power, AttackRange,MaxMp,CurrentMp);
        }
    }
    public virtual void Skill() { }
    public override void Damaged(int damage)
    {
        CurrentHp -= damage;
    }
}
