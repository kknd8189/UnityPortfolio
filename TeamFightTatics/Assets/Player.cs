using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Player : Entity
{
    public UnityEvent<int> OnGoldChanged = new UnityEvent<int>();
    public UnityEvent<int> CurrentExpChanged = new UnityEvent<int>();
    public UnityEvent<int> CurrentHpChanged = new UnityEvent<int>();

    public Enemy enemy;
    public List<GameObject>[] PlayerCharacterList;

    [SerializeField]
    private int _liveCharacterCount = 0;
    public int LiveCharacterCount 
    {
        get { return _liveCharacterCount; }
        set { _liveCharacterCount = value; }
    }

    public int Level
    {
        get
        {
            return _level;
        }

        set
        {
            _level = value;
        }
    }
    private int _level = 1;
    public int MaxExp
    {
        get { return _maxExp; }
        set { _maxExp = value; }
    }
    private int _maxExp = 2;
    public int CurrentExp
    {
        get { return _currentExp; }
        set
        {
            _currentExp = value;
            CurrentExpChanged?.Invoke(_currentExp);
        }
    }
    private int _currentExp = 0;
    public int Gold
    {
        get { return _gold; }
        set
        {
            _gold = value;
            // Null이 될 일이 없다면 Null 체크 안해도 됨
            OnGoldChanged?.Invoke(_gold);
        }
    }
    [SerializeField]
    private int _gold = 10;
    private int _capacity = 1;
    public int Capacity
    {
        get { return _capacity; }
        set { _capacity = value; }
    }
    public new int CurrentHp
    {
        get { return _currentHp; }
        set
        {
            _currentHp = value;
            CurrentHpChanged?.Invoke(_currentHp);
            if (_currentHp <= 0) GameManager.Instance.GameOver();
        }
    }
    //    레벨 필요 경험치
    //Lv.1  Lv.2	-
    //Lv.2  Lv.3	2 XP
    //Lv.3  Lv.4	6 XP
    //Lv.4  Lv.5	10 XP
    //Lv.5  Lv.6	20 XP
    //Lv.6  Lv.7	36 XP
    //Lv.7  Lv.8	56 XP
    //Lv.8  Lv.9	80 XP
    private int[] maxExpContainer = { 0, 2, 6, 10, 20, 36, 56, 80, 999  };
    private void Start()
    {
        PlayerCharacterList = new List<GameObject>[PoolManager.Instance.CharacterDataList.Count];
        for (int i = 0; i < PlayerCharacterList.Length; ++i) { PlayerCharacterList[i] = new List<GameObject>(); }
        _maxHp = 100;
        _currentHp = _maxHp;
    }
    private void Update()
    {
        earnGold();
        updateLevel();
    }
    private void earnGold()
    {
        if (GameManager.Instance.IsOver && GameManager.Instance.GameState == GAMESTATE.Battle)
        {
            int interest = Mathf.FloorToInt(_gold / 10);
            //최대 이자는 5이다.
            if (interest >= 5) interest = 5;
            Gold += 4 + interest;
            if (Level <= 8) { CurrentExp += 2; }
        }
    }
    public void BuyExp()
    {
        if (Gold >= 4 && Level <= 8)
        {
            CurrentExp += 4;
            Gold -= 4;
        }
    }
    private void updateLevel()
        {
            if (CurrentExp >= MaxExp)
            {
                CurrentExp = CurrentExp - MaxExp;
                Capacity++;
                Level += 1;

                _maxExp = maxExpContainer[Level-1];
          
                CurrentExp = CurrentExp;
            }
        }
    public override void Damaged(int damage)
    {
        CurrentHp -= damage;
    }

} 
