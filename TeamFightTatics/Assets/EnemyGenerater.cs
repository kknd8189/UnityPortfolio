using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerater : MonoBehaviour
{
    [SerializeField]
    private List<CharacterSciptableObject> CharacterDataList;
    [SerializeField]
    private List<StageScriptableObject> StageDataList;

    //private void Update()
    //{
    //    if(GameManager.Instance.GameState == GAMESTATE.Battle && GameManager.Instance.IsOver)
    //    {
    //        for(int i = 0; i < )
    //        enemyGenerate();
    //    }
    //}

    //private void enemyGenerate(int turnNumber)
    //{
    //    for(int i =0; i < turnNumber; i++)
    //    {

    //    }
    //}
}
