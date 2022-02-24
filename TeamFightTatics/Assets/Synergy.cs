using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synergy : MonoBehaviour
{
    //0 Archor, 1 Warrior, 2 Magician, 3 Orc, 4 Undead, 5 Beast, 6 Human, 7 Elf
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
        for (int i = 0; i < SynergyCharacterList[synergyNum].Count; i++)
        {
            Persona persona = SynergyCharacterList[synergyNum][i].GetComponent<Persona>();

            switch (synergyNum)
            {                    
                case 0:
                    if (_synergyCount[0] >= 1 && !persona.IsSynergyOn)
                    {
                        SynergyCharacterList[0][i].GetComponent<Persona>().AttackRange += 5;
                        SynergyCharacterList[0][i].GetComponent<Persona>().IsSynergyOn = true;
                    }

                    else if(_synergyCount[0] < 1 && persona.IsSynergyOn)
                    {
                        SynergyCharacterList[0][i].GetComponent<Persona>().AttackRange -= 5;
                        SynergyCharacterList[0][i].GetComponent<Persona>().IsSynergyOn = false;
                    }
                    break;

                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
            }
        }
    }
}
