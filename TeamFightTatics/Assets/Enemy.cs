using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Enemy : Entity
{
    public UnityEvent<int> CurrentHpChanged = new UnityEvent<int>();

    public Player player;

    public new int CurrentHp
    {
        get { return _currentHp; }
        set
        {
            _currentHp = value;
            CurrentHpChanged?.Invoke(_currentHp);
            if (_currentHp <= 0) GameManager.Instance.Win();
        }
    }

    [SerializeField]
    private int _liveEnemyCount = 0;
    public int LiveEnemyCount
    {
        get { return _liveEnemyCount; }
        set { _liveEnemyCount = value; }
    }
    private void Start()
    {
        _maxHp = 100;
        _currentHp = _maxHp;
    }

    private void Update()
    {
        if(_liveEnemyCount <= 0)
        {
            Damaged(player.Level);
        }
    }
    public override void Damaged(int damage)
    {
        CurrentHp -= damage;
    }
}
