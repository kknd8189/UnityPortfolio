using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : Entity
{
    private void Start()
    {
        _maxHp = 100;
        _currentHp = _maxHp;
    }
}
