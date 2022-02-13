using UnityEngine;
using UnityEngine.Events;

public class Player : Entity
{
    public UnityEvent<int> OnGoldChanged = new UnityEvent<int>();
    public UnityEvent<int> CurrentExpChanged = new UnityEvent<int>();

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
    private int _gold = 10;

    //    레벨 필요 경험치
    //Lv.1  Lv.2	-
    //Lv.2  Lv.3	2 XP
    //Lv.3  Lv.4	6 XP
    //Lv.4  Lv.5	10 XP
    //Lv.5  Lv.6	20 XP
    //Lv.6  Lv.7	36 XP
    //Lv.7  Lv.8	56 XP
    //Lv.8  Lv.9	80 XP

    private void Start()
    {
        _maxHp = 100;
        _currentHp = _maxHp;
    }
    private void Update()
    {
        earnGold();
    }
    private void earnGold()
    {
        int interest;
        interest = Mathf.FloorToInt(_gold / 10);

        //최대 이자는 5이다.
        if (interest >= 5) interest = 5;

        if (GameManager.Instance.IsOver && GameManager.Instance.GameState == GAMESTATE.Battle)
        {
            Gold += 4 + interest;
            if(Level != 9) CurrentExp += 2;
        }
    }
    public void BuyExp()
    {
        if(Gold >= 4 && Level <= 8) 
        {
            CurrentExp += 4;
            Gold -= 4;   
        }

        if (CurrentExp >= MaxExp)
        {
            CurrentExp = CurrentExp - MaxExp;

            Level += 1;

            switch (Level)
            {
                case 1:
                    MaxExp = 2;
                    break;
                case 2:
                    MaxExp = 6;
                    break;
                case 3:
                    MaxExp = 10;
                    break;
                case 4:
                    MaxExp = 20;
                    break;
                case 5:
                    MaxExp = 36;
                    break;
                case 6:
                    MaxExp = 56;
                    break;
                case 7:
                    MaxExp = 80;
                    break;
            }

            CurrentExp = CurrentExp;
        }
    }
}
