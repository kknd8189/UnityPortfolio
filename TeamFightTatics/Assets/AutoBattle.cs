using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState { Search, Attack }

public class AutoBattle : MonoBehaviour
{
    public GameObject enemy;
    public List<GameObject> enemys;

    public bool isFind;
    public bool isDelay;

    public float attackRange = 10;

    public CharacterState characterState;

    private void Start()
    {
        characterState = CharacterState.Search;
        isFind = false;
        StartCoroutine("CheckEnemy");
    }

     private void Update()
    {
        if (enemy != null) isFind = true;
        else if (enemy == null) isFind = false;

        if (characterState == CharacterState.Search && isFind)
        {
            if (attackRange >= Vector3.Distance(enemy.transform.position, gameObject.transform.position) && !isDelay)
            {                
                characterState = CharacterState.Attack;
                isDelay = true;
                StartCoroutine("Attack");
            }
        }
        
        else if(characterState == CharacterState.Attack && isFind)
        {

            if (attackRange >= Vector3.Distance(enemy.transform.position, gameObject.transform.position) && !isDelay)
            {

                characterState = CharacterState.Attack;
                isDelay = true;
                StartCoroutine("Attack");

            }
        }
      

        else if (characterState == CharacterState.Attack && !isFind && enemys.Count > 0)
        {
            StartCoroutine("CheckEnemy");
            characterState = CharacterState.Search;
        }

    }

    IEnumerator Attack()
    { 
        Debug.Log("공격!!!");

        yield return new WaitForSeconds(1f);

        isDelay = false;
    }


    IEnumerator CheckEnemy()
    {
        yield return new WaitForSeconds(0.3f);
            Debug.Log("감지!!!");

            enemys = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
            enemy = enemys[0];

            float shortDis = Vector3.Distance(gameObject.transform.position, enemys[0].transform.position);

            foreach (GameObject found in enemys)
            {
                float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

                if (Distance < shortDis)
                {
                    enemy = found;
                    shortDis = Distance;
                }
            }

            if (!isFind) StartCoroutine("CheckEnemy");
            else StopCoroutine("CheckEnemy");
    }
}
