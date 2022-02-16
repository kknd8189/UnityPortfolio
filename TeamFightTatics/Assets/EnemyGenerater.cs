using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerater : MonoBehaviour
{
    [SerializeField]
    private List<StageScriptableObject> StageDataList;
    private void Update()
    {
        if (GameManager.Instance.GameState == GAMESTATE.Battle && GameManager.Instance.IsOver)
        {
        }
    }
    private void enemyGenerate(int turnNumber)
    {
        for (int i = 0; i < turnNumber; i++)
        {
        }
    }
}
