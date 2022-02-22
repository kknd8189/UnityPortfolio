using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Enemy : Entity
{
    public UnityEvent<int> CurrentHpChanged = new UnityEvent<int>();
    public Player Player;
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
        if (_currentHp <= 0)
        {
            GameManager.Instance.Win();
        }

        //상대에 데미지 조건 
        if (GameManager.Instance.GameState == GAMESTATE.Battle)
        {
            if (Player.LiveCharacterCount <= 0) Shoot();
            else if (GameManager.Instance.IsOver) Shoot();
        }

    }
    public override void Damaged(int damage)
    {
        CurrentHp -= damage;
    }
    public void Shoot()
    {
        int damage = _liveEnemyCount + GameManager.Instance.Turn;
        PoolManager.Instance.PullArrowQueue(damage, transform.position + transform.up * 20f, Player.gameObject);
    }
}
