using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private List<StageScriptableObject> StageDataList;
    public void EnemyGenerate(int turn)
    {
        //j ĳ���ͳѹ�, i ĳ���� ��ȯ����, k ��ȯ�� ����
        int k = 0;
        for (int j = 0; j < StageDataList[turn - 1].Amount.Length; j++)
        {        
            for (int i = 0; i < StageDataList[turn - 1].Amount[j]; i++)
            {
                GameObject characterPrefab;
                characterPrefab = PoolManager.Instance.CharacterQueue[j].Dequeue();
                characterPrefab.tag = "EnemyCharacter";
                characterPrefab.SetActive(true);
                characterPrefab.transform.SetParent(null);
                characterPrefab.transform.position = TileManager.Instance.BattleTileList[StageDataList[turn - 1].Index[k]].transform.position;
                characterPrefab.transform.rotation = new Quaternion(0, 180, 0, 0);
                characterPrefab.GetComponent<Persona>().DiposedIndex = StageDataList[turn - 1].Index[k];
                k++;
            }
        }
    }
}
