using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    public UnityEvent<int> CurrentHpChanged = new UnityEvent<int>();

    protected int _maxHp;
    public int MaxHp
    {
        get { return _maxHp; }
        set { _maxHp = value; }
    }
    protected int _currentHp;
    public int CurrentHp
    {
        get {return _currentHp; }
        set 
        { 
            _currentHp = value;
            CurrentHpChanged?.Invoke(_currentHp);
        }
    }
}
