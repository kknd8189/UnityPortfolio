using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synergy : MonoBehaviour
{
    //0 Archor, 1 Warrior, 2 Magician,3 Beast, 4 orc, 5 Undead , 6 Human, 7 Elf
    public int[] _synergyCount;

    private int[] _summonCount;
    private bool[] _isCounted;
    private bool[] _isSynergyActivate;

    //시너지 받을 캐릭터를 담는 리스트
    public List<GameObject>[] SynergyCharacterList;

    public int MaxSynergyNum;
    public Player player;

    private void Start()
    {
        _isCounted = new bool[PoolManager.Instance.CharacterDataList.Count];
        _summonCount = new int[PoolManager.Instance.CharacterDataList.Count];
        _synergyCount = new int[MaxSynergyNum];
        _isSynergyActivate = new bool[MaxSynergyNum];
        SynergyCharacterList = new List<GameObject>[MaxSynergyNum];

        for (int i = 0; i < MaxSynergyNum; i++)
        {
            SynergyCharacterList[i] = new List<GameObject>();
            _isSynergyActivate[i] = false;
        }
    }
    private void Update()
    {
        if (_isSynergyActivate[5])
        {
            if (GameManager.Instance.GameState == GAMESTATE.Battle && GameManager.Instance.IsOver)
            {
                for (int i = 0; i < SynergyCharacterList[5].Count; i++)
                {
                    Persona persona = SynergyCharacterList[5][i].GetComponent<Persona>();
                    persona.MaxHp += 5;
                    persona.CurrentHp = persona.MaxHp;
                }
            }
        }

        if (_isSynergyActivate[6])
        {
            if (GameManager.Instance.GameState == GAMESTATE.Battle && GameManager.Instance.IsOver)
            {
                player.CurrentExp += 2;
                player.Gold += 2;
            }
        }
    }
    public void IncreaseSynergyCount(int characterNum)
    {
        _summonCount[characterNum]++;

        if (!_isCounted[characterNum])
        {
            for (int i = 0; i < PoolManager.Instance.CharacterDataList[characterNum].SynergyNum.Length; i++)
            {
                _synergyCount[PoolManager.Instance.CharacterDataList[characterNum].SynergyNum[i]]++;
                updateSynergy(PoolManager.Instance.CharacterDataList[characterNum].SynergyNum[i]);
                _isCounted[characterNum] = true;
            }
        }

        Debug.Log($"{_synergyCount[0]},{_synergyCount[1]},{_synergyCount[2]},{_synergyCount[3]}, {_synergyCount[4]},{_synergyCount[5]},{_synergyCount[6]},{_synergyCount[7]}");
    }
    public void DecreaseSynergyCount(int characterNum)
    {
        _summonCount[characterNum]--;

        if (_summonCount[characterNum] <= 0)
        {
            for (int i = 0; i < PoolManager.Instance.CharacterDataList[characterNum].SynergyNum.Length; i++)
            {
                _synergyCount[PoolManager.Instance.CharacterDataList[characterNum].SynergyNum[i]]--;
                updateSynergy(PoolManager.Instance.CharacterDataList[characterNum].SynergyNum[i]);
            }
            _isCounted[characterNum] = false;
        }

        Debug.Log($"{_synergyCount[0]},{_synergyCount[1]},{_synergyCount[2]},{_synergyCount[3]}, {_synergyCount[4]},{_synergyCount[5]},{_synergyCount[6]},{_synergyCount[7]}");
    }
    private void updateSynergy(int synergyNum)
    {
        switch (synergyNum)
        {
            case 0:
                Synergy0();
                break;
            case 1:
                Synergy1();
                break;
            case 2:
                Synergy2();
                break;
            case 3:
                Synergy3();
                break;
            case 4:
                Synergy4();
                break;
            case 5:
                Synergy5();
                break;
            case 6:
                Synergy6();
                break;
            case 7:
                Synergy7();
                break;
        }
    }
    //archor 시너지 사거리 10 증가
    void Synergy0()
    {
        if (_synergyCount[0] >= 3)
        {
            for (int i = 0; i < SynergyCharacterList[0].Count; i++)
            {
                Persona persona = SynergyCharacterList[0][i].GetComponent<Persona>();
                if (persona.IsSynergyOn[0]) break;
                persona.AttackRange += 10;
                persona.IsSynergyOn[0] = true;
            }

            if (!_isSynergyActivate[0]) UIManager.Instance.SynergyActivate(0);
            _isSynergyActivate[0] = true;
        }
        else if (_synergyCount[0] < 3)
        {
            for (int i = 0; i < SynergyCharacterList[0].Count; i++)
            {
                Persona persona = SynergyCharacterList[0][i].GetComponent<Persona>();
                if (!persona.IsSynergyOn[0]) break;
                persona.AttackRange -= 10;
                persona.IsSynergyOn[0] = false;
            }

            if (_isSynergyActivate[0]) UIManager.Instance.SynergyDeactivate(0);
            _isSynergyActivate[0] = false;
        }
    }
    //warrior 시너지 공격력이 10증가
    void Synergy1()
    {
        if (_synergyCount[1] >= 3)
        {
            for (int i = 0; i < SynergyCharacterList[1].Count; i++)
            {
                Persona persona = SynergyCharacterList[1][i].GetComponent<Persona>();
                if (persona.IsSynergyOn[0]) break;
                persona.Power += 10;
                persona.IsSynergyOn[0] = true;
            }

            if (!_isSynergyActivate[1]) UIManager.Instance.SynergyActivate(1);
            _isSynergyActivate[1] = true;
        }

        else if (_synergyCount[1] < 3)
        {
            for (int i = 0; i < SynergyCharacterList[1].Count; i++)
            {
                Persona persona = SynergyCharacterList[1][i].GetComponent<Persona>();
                if (!persona.IsSynergyOn[0]) break;
                persona.Power -= 10;
                persona.IsSynergyOn[0] = false;
            }

            if (_isSynergyActivate[1]) UIManager.Instance.SynergyDeactivate(1);
            _isSynergyActivate[1] = false;
        }
    }
    //magician 시너지 필요 마나량 10 감소
    void Synergy2()
    {
        if (_synergyCount[2] >= 3)
        {
            for (int i = 0; i < SynergyCharacterList[2].Count; i++)
            {
                Persona persona = SynergyCharacterList[2][i].GetComponent<Persona>();
                if (persona.IsSynergyOn[0]) break;
                persona.MaxMp -= 10;
                persona.IsSynergyOn[0] = true;
            }
            if (!_isSynergyActivate[2]) UIManager.Instance.SynergyActivate(2);
            _isSynergyActivate[2] = true;
        }
        else if (_synergyCount[1] < 3)
        {
            for (int i = 0; i < SynergyCharacterList[2].Count; i++)
            {
                Persona persona = SynergyCharacterList[2][i].GetComponent<Persona>();
                if (!persona.IsSynergyOn[0]) break;
                persona.MaxMp += 10;
                persona.IsSynergyOn[0] = false;
            }

            if (_isSynergyActivate[2]) UIManager.Instance.SynergyDeactivate(0);
            _isSynergyActivate[2] = false;
        }
    }
    //beast 시너지 최대 체력 50증가
    void Synergy3()
    {
        if (_synergyCount[3] >= 3)
        {
            for (int i = 0; i < SynergyCharacterList[3].Count; i++)
            {
                Persona persona = SynergyCharacterList[3][i].GetComponent<Persona>();
                if (persona.IsSynergyOn[0]) break;
                persona.MaxHp += 50;
                persona.IsSynergyOn[0] = true;
            }
            if (!_isSynergyActivate[3]) UIManager.Instance.SynergyActivate(3);
            _isSynergyActivate[3] = true;
        }
        else if (_synergyCount[1] < 3)
        {
            for (int i = 0; i < SynergyCharacterList[3].Count; i++)
            {
                Persona persona = SynergyCharacterList[3][i].GetComponent<Persona>();
                if (!persona.IsSynergyOn[0]) break;
                persona.MaxHp -=50;
                persona.IsSynergyOn[0] = false;
            }

            if (_isSynergyActivate[3]) UIManager.Instance.SynergyDeactivate(0);
            _isSynergyActivate[3] = false;
        }
    }
    //orc 시너지 공격속도 증가
    void Synergy4()
    {
        if (_synergyCount[4] >= 3)
        {
            for (int i = 0; i < SynergyCharacterList[4].Count; i++)
            {
                Persona persona = SynergyCharacterList[4][i].GetComponent<Persona>();
                if (persona.IsSynergyOn[1]) break;
                persona.AttackRange += 10;
                persona.IsSynergyOn[1] = true;
            }

            if (!_isSynergyActivate[4]) UIManager.Instance.SynergyActivate(4);
            _isSynergyActivate[4] = true;
        }
        else if (_synergyCount[4] < 3)
        {
            for (int i = 0; i < SynergyCharacterList[4].Count; i++)
            {
                Persona persona = SynergyCharacterList[4][i].GetComponent<Persona>();
                if (!persona.IsSynergyOn[1]) break;
                persona.AttackRange -= 10;
                persona.IsSynergyOn[1] = false;
            }

            if (_isSynergyActivate[4]) UIManager.Instance.SynergyDeactivate(4);
            _isSynergyActivate[4] = false;
        }
    }
    //undead 시너지 턴이 끝날때마다 영구적으로 체력 5획득
    void Synergy5()
    {
        if (_synergyCount[5] >= 3)
        {
            if (!_isSynergyActivate[5]) UIManager.Instance.SynergyActivate(5);
            _isSynergyActivate[5] = true;
        }
        else if (_synergyCount[5] < 3)
        {
            if (_isSynergyActivate[5]) UIManager.Instance.SynergyDeactivate(5);
            _isSynergyActivate[5] = false;
        }
    }
    //human 시너지 턴이 끝나면 골드 2와 경험치 2 추가 획득
    void Synergy6()
    {
        if (_synergyCount[6] >= 3)
        {
            if (!_isSynergyActivate[6]) UIManager.Instance.SynergyActivate(6);
            _isSynergyActivate[6] = true;
        }
        else if (_synergyCount[6] < 3)
        {
            if (_isSynergyActivate[6]) UIManager.Instance.SynergyDeactivate(6);
            _isSynergyActivate[6] = false;
        }
    }
    //elf 시너지 공격시 회복 마나 두배
    void Synergy7()
    {
        if (_synergyCount[7] >= 3)
        {
            for (int i = 0; i < SynergyCharacterList[7].Count; i++)
            {
                Persona persona = SynergyCharacterList[7][i].GetComponent<Persona>();
                if (persona.IsSynergyOn[1]) break;
                persona.RecoverMp += 10;
                persona.IsSynergyOn[1] = true;
            }

            if (!_isSynergyActivate[7]) UIManager.Instance.SynergyActivate(7);
            _isSynergyActivate[7] = true;
        }
        else if (_synergyCount[7] < 3)
        {
            for (int i = 0; i < SynergyCharacterList[7].Count; i++)
            {
                Persona persona = SynergyCharacterList[7][i].GetComponent<Persona>();
                if (!persona.IsSynergyOn[1]) break;
                persona.RecoverMp -= 10;
                persona.IsSynergyOn[1] = false;
            }

            if (_isSynergyActivate[7]) UIManager.Instance.SynergyDeactivate(7);
            _isSynergyActivate[7] = false;
        }
    }
}

