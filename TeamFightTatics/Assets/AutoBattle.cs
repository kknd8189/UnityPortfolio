using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState { Search, Attack }

public class AutoBattle : MonoBehaviour, IAttack, ISkill
{
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private List<GameObject> enemys;
    [SerializeField]
    private bool isFind;
    [SerializeField]
    private bool isDelay;
    [SerializeField]
    private CharacterState characterState;

    public Persona Persona;

    private void OnEnable()
    {
        isFind = false;
    }
    private void Update()
    {
        if (enemy != null) isFind = true;
        else if (enemy == null) isFind = false;

        if (characterState == CharacterState.Search && isFind)
        {
            if (Persona.AttackRange >= Vector3.Distance(enemy.transform.position, gameObject.transform.position) && !isDelay)
            {                
                characterState = CharacterState.Attack;
                isDelay = true;
                StartCoroutine("Attack");
            }
        }
        
        else if(characterState == CharacterState.Attack && isFind)
        {

            if (Persona.AttackRange >= Vector3.Distance(enemy.transform.position, gameObject.transform.position) && !isDelay)
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
        Debug.Log("감지!!!");

        GameObject gameObject = this.gameObject;

        if (gameObject.tag == "PlayerCharacter") 
        {
            enemys = new List<GameObject>(GameObject.FindGameObjectsWithTag("EnemyCharacter"));
            enemy = enemys[0];
        }
        else if(gameObject.tag == "EnemyCharacter")
        {
            enemys = new List<GameObject>(GameObject.FindGameObjectsWithTag("PlayerCharacter"));
            enemy = enemys[0];
        }

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

        yield return new WaitForSeconds(1f);
    }
    public void Attack(int Power, int attackRange,int attackDelay)
    {

    }
    public void Skill() 
    {
        Persona.Skill();
    }
}