using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int CurrentHp
    {
        get { return _currentHp; }
        set { _currentHp = value; }
    }
    protected int _maxHp;
    public int MaxHp
    {
        get { return _maxHp; }
        set { _maxHp = value; }
    }
    [SerializeField]
    protected int _currentHp;
    [SerializeField]
    protected float _speed;
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }
    public virtual void Damaged(int damage)
    {
        _currentHp -= damage;
    }
}
