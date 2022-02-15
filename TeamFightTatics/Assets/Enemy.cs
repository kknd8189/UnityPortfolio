using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Enemy : Entity
{
    public UnityEvent<int> CurrentHpChanged = new UnityEvent<int>();
    public new int CurrentHp
    {
        get { return _currentHp; }
        set
        {
            _currentHp = value;
            CurrentHpChanged?.Invoke(_currentHp);
        }
    }

    private void Start()
    {
        _maxHp = 100;
        _currentHp = _maxHp;
    }
}
