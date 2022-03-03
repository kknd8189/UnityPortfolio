using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private List<StageScriptableObject> StageDataList;
    public Enemy Enemy;

    private void Start()
    {
        EnemyGenerate(0);
    }
    private void Update()
    {
        if (GameManager.Instance.GameState == GAMESTATE.Battle && GameManager.Instance.IsOver)
        {
            EnemyGenerate(GameManager.Instance.Turn);
        }
    }
    public void EnemyGenerate(int turn)
    {
        int k = 0;
        for (int j = 0; j < StageDataList[turn].Amount.Length; j++)
        {
            for (int i = 0; i < StageDataList[turn].Amount[j]; i++)
            {
                GameObject characterPrefab;
                characterPrefab = PoolManager.Instance.CharacterQueue[j].Dequeue();
                characterPrefab.tag = "EnemyCharacter";
                characterPrefab.transform.SetParent(null);
                characterPrefab.transform.position = TileManager.Instance.BattleTileList[StageDataList[turn].Index[k]].transform.position;
                characterPrefab.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                characterPrefab.GetComponent<Persona>().DiposedIndex = StageDataList[turn].Index[k];
                Enemy.LiveEnemyCount++;
                characterPrefab.SetActive(true);
                characterPrefab.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
                Enemy.EnemyPersonaList.Add(characterPrefab);
                k++;
            }
        }
    }
}
