using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState { Idle, Search, Attack, Chase, Die }

public class AutoBattle : MonoBehaviour, IAttack, ISkill
{
    [SerializeField]
    private List<GameObject> enemys = new List<GameObject>();
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private bool isDelay;
    [SerializeField]
    private CharacterState characterState;

    public Persona Persona;
    private void OnEnable()
    {
       isDelay = false;
        characterState = CharacterState.Idle;
    }
    private void Update()
    {
        if (Persona.IsOnBattleField)
        {
            ///배틀 페이즈가 끝날때
            if (GameManager.Instance.GameState == GAMESTATE.Battle && GameManager.Instance.IsOver)
            {
                returnToInitialState();

            }
            //준비 페이즈가 끝날때
            else if (GameManager.Instance.GameState == GAMESTATE.StandBy && GameManager.Instance.IsOver)
            {
                characterState = CharacterState.Search;
            }
            switch (characterState)
            {
                case CharacterState.Idle:
                    break;

                case CharacterState.Search:
                    checkEnemy();
                    break;


                case CharacterState.Attack:
                    Attack(Persona.Power, Persona.AttackRange, Persona.AttackDelayTime);
                    break;


                case CharacterState.Chase:
                    Chase();
                    break;


                case CharacterState.Die:
                    break;
            }


        }
       
    }

    IEnumerator AttackDelay(float DelayTime)
    { 
        isDelay = false;
        yield return new WaitForSeconds(DelayTime);
    }

    private void checkEnemy()
    {
        Debug.Log("감지!!!");

        enemys.Clear();
        if (gameObject.tag == "PlayerCharacter")
        {
            GameObject[] gameObject = GameObject.FindGameObjectsWithTag("EnemyCharacter");
            for (int i = 0; i < gameObject.Length; i++) enemys.Add(gameObject[i]);

        }
        else if (gameObject.tag == "EnemyCharacter")
        {
            GameObject[] gameObject = GameObject.FindGameObjectsWithTag("PlayerCharacter");

            for (int i = 0; i < gameObject.Length; i++)
            {
                if (!gameObject[i].GetComponent<Persona>().IsOnBattleField) break;
                enemys.Add(gameObject[i]);
            }

        }

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

        characterState = CharacterState.Chase;
    }
    public void Attack(int Power, float attackRange, float attackDelay)
    {
        Debug.Log("공격");
    }
    public void Skill()
    {
        Persona.Skill();
    }
    private void returnToInitialState()
    {
        characterState = CharacterState.Idle;
        Persona.CurrentHp = Persona.MaxHp;
        Persona.CurrentMp = Persona.DefaultMp;
        transform.position = TileManager.Instance.BattleTileList[Persona.DiposedIndex].transform.position;
        enemys.Clear();
        isDelay = false;
    }
    private void Chase()
    {
        float distance = Vector3.Distance(transform.position, enemy.transform.position);

        if (distance >= Persona.AttackRange)
        {
            Debug.Log("추적");
            transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, Persona.Speed * Time.deltaTime);
        }

        else if(distance <= Persona.AttackRange)
        {
            characterState = CharacterState.Attack;
        }
    }

}