using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected int _maxHp;
    public int MaxHp
    {
        get { return _maxHp; }
        set { _maxHp = value; }
    }
    [SerializeField]
    protected int _currentHp;
    public int CurrentHp
    {
        get { return _currentHp; }
        set
        {
            _currentHp = value;
        }
    }
    public virtual void Damaged(int damage)
    {
        CurrentHp -= damage;
    }
}
