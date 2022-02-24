using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synergy : MonoBehaviour
{
    //0 Archor, 1 Warrior, 2 Magician,3 Beast, /////////// 4 orc, 5 Undead , 6 Human, 7 Elf
    public int[] _synergyCount;

    private int[] SummonCount;
    private bool[] isCounted;

    //시너지 받을 캐릭터를 담는 리스트
    public List<GameObject>[] SynergyCharacterList;

    public int MaxSynergyNum;
    public Player player;

    private void Start()
    {
        isCounted = new bool[PoolManager.Instance.CharacterDataList.Count];
        SummonCount = new int[PoolManager.Instance.CharacterDataList.Count];
        _synergyCount = new int[MaxSynergyNum];

        SynergyCharacterList = new List<GameObject>[MaxSynergyNum];

        for (int i = 0; i < MaxSynergyNum; i++)
        {
            SynergyCharacterList[i] = new List<GameObject>();
        }
    }
    public void IncreaseSynergyCount(int characterNum)
    {
        SummonCount[characterNum]++;

        if (!isCounted[characterNum])
        {
            for (int i = 0; i < PoolManager.Instance.CharacterDataList[characterNum].SynergyNum.Length; i++)
            {
                _synergyCount[PoolManager.Instance.CharacterDataList[characterNum].SynergyNum[i]]++;
                updateSynergy(PoolManager.Instance.CharacterDataList[characterNum].SynergyNum[i]);
                isCounted[characterNum] = true;
            }
        }
        Debug.Log($"{_synergyCount[0]},{_synergyCount[1]},{_synergyCount[2]},{_synergyCount[3]}, {_synergyCount[4]},{_synergyCount[5]},{_synergyCount[6]},{_synergyCount[7]}");
    }
    public void DecreaseSynergyCount(int characterNum)
    {
        SummonCount[characterNum]--;

        if (SummonCount[characterNum] <= 0)
        {
            for (int i = 0; i < PoolManager.Instance.CharacterDataList[characterNum].SynergyNum.Length; i++)
            {
                _synergyCount[PoolManager.Instance.CharacterDataList[characterNum].SynergyNum[i]]--;
                updateSynergy(PoolManager.Instance.CharacterDataList[characterNum].SynergyNum[i]);
            }
            isCounted[characterNum] = false;
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
    //archor 시너지 사거리 5 증가
    void Synergy0()
    {
        if (_synergyCount[0] >= 1)
        {
            for (int i = 0; i < SynergyCharacterList[0].Count; i++)
            {
                Persona persona = SynergyCharacterList[0][i].GetComponent<Persona>();
                if (persona.IsSynergyOn[0]) break;
                persona.AttackRange += 10;
                persona.IsSynergyOn[0] = true;
            }
        }

        else if (_synergyCount[0] < 1)
        {
            for (int i = 0; i < SynergyCharacterList[0].Count; i++)
            {
                Persona persona = SynergyCharacterList[0][i].GetComponent<Persona>();
                if (!persona.IsSynergyOn[0]) break;
                persona.AttackRange -= 10;
                persona.IsSynergyOn[0] = false;
            }
        }
    }
    //warrior 시너지 공격력이 10증가
    void Synergy1()
    {
        if (_synergyCount[1] >= 1)
        {
            for (int i = 0; i < SynergyCharacterList[1].Count; i++)
            {
                Persona persona = SynergyCharacterList[1][i].GetComponent<Persona>();
                if (persona.IsSynergyOn[0]) break;
                persona.Power += 10;
                persona.IsSynergyOn[0] = true;
            }
        }

        else if (_synergyCount[1] < 1)
        {
            for (int i = 0; i < SynergyCharacterList[1].Count; i++)
            {
                Persona persona = SynergyCharacterList[1][i].GetComponent<Persona>();
                if (!persona.IsSynergyOn[0]) break;
                persona.Power -= 10;
                persona.IsSynergyOn[0] = false;
            }
        }
    }
    //magician 시너지 필요 마나량 10 감소
    void Synergy2()
    {
        if (_synergyCount[2] >= 1)
        {
            for (int i = 0; i < SynergyCharacterList[2].Count; i++)
            {
                Persona persona = SynergyCharacterList[2][i].GetComponent<Persona>();
                if (persona.IsSynergyOn[0]) break;
                persona.MaxMp -= 10;
                persona.IsSynergyOn[0] = true;
            }
        }
        else if (_synergyCount[1] < 1)
        {
            for (int i = 0; i < SynergyCharacterList[2].Count; i++)
            {
                Persona persona = SynergyCharacterList[2][i].GetComponent<Persona>();
                if (!persona.IsSynergyOn[0]) break;
                persona.MaxMp += 10;
                persona.IsSynergyOn[0] = false;
            }
        }
    }
    //beast 시너지 최대 체력 50증가
    void Synergy3()
    {
        if (_synergyCount[3] >= 1)
        {
            for (int i = 0; i < SynergyCharacterList[3].Count; i++)
            {
                Persona persona = SynergyCharacterList[3][i].GetComponent<Persona>();
                if (persona.IsSynergyOn[0]) break;
                persona.MaxHp += 50;
                persona.IsSynergyOn[0] = true;
            }
        }
        else if (_synergyCount[1] < 1)
        {
            for (int i = 0; i < SynergyCharacterList[3].Count; i++)
            {
                Persona persona = SynergyCharacterList[3][i].GetComponent<Persona>();
                if (!persona.IsSynergyOn[0]) break;
                persona.MaxHp -=50;
                persona.IsSynergyOn[0] = false;
            }
        }
    }
    //orc 시너지
    void Synergy4()
    {

    }
    //undead 시너지
    void Synergy5()
    {
    }
    //human 시너지
    void Synergy6()
    {
    }
    //elf 시너지
    void Synergy7()
    {
    }
}

