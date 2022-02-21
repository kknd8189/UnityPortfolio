using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private List<StageScriptableObject> StageDataList;

    public Enemy enemy;

    public void EnemyGenerate(int turn)
    {
        //j 캐릭터넘버, i 캐릭터 소환숫자, k 소환된 순서
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
                characterPrefab.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                characterPrefab.GetComponent<Persona>().DiposedIndex = StageDataList[turn - 1].Index[k];
                enemy.LiveEnemyCount++;
                k++;
            }
        }
    }
}
