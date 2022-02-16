using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    protected int _index;
    public int Index
    { 
        get { return _index; }
        set { _index = value; }
    }
    protected bool _isUsed;
    public bool IsUsed
    {
        get { return _isUsed; }
        set { _isUsed = value; }
    }
}
