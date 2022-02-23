using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synergy : MonoBehaviour
{
    //0 Archor, 1 Warrior, 2 Magician, 3 Orc, 4 Undead, 5 Beast, 6 Human, 7 Elf
    private int[] SynergyCount;
    private int[] SummonCount;
    private bool[] isCounted;

    public int MaxSynergyNum;
    public Player player;

    private void Start()
    {
        isCounted = new bool[PoolManager.Instance.CharacterDataList.Count];
        SummonCount = new int[PoolManager.Instance.CharacterDataList.Count];
        SynergyCount = new int[MaxSynergyNum];
    }

    public void IncreaseSynergyCount(int characterNum)
    {
        SummonCount[characterNum]++;

        if (!isCounted[characterNum])
        {
            for (int i = 0; i < PoolManager.Instance.CharacterDataList[characterNum].SynergyNum.Length; i++)
            {
                SynergyCount[PoolManager.Instance.CharacterDataList[characterNum].SynergyNum[i]]++;
                isCounted[characterNum] = true;
            }
        }

        Debug.Log($"{SynergyCount[0]},{SynergyCount[1]},{SynergyCount[2]},{SynergyCount[3]}, {SynergyCount[4]},{SynergyCount[5]},{SynergyCount[6]},{SynergyCount[7]}");
    }
    public void DecreaseSynergyCount(int characterNum)
    {
        SummonCount[characterNum]--;

        if (SummonCount[characterNum] <= 0)
        {
            for (int i = 0; i < PoolManager.Instance.CharacterDataList[characterNum].SynergyNum.Length; i++)
            {
                SynergyCount[PoolManager.Instance.CharacterDataList[characterNum].SynergyNum[i]]--;
            }

            isCounted[characterNum] = false;
        }

        Debug.Log($"{SynergyCount[0]},{SynergyCount[1]},{SynergyCount[2]},{SynergyCount[3]}, {SynergyCount[4]},{SynergyCount[5]},{SynergyCount[6]},{SynergyCount[7]}");
    }

}
