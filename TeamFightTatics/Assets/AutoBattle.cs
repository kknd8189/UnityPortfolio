using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AutoBattle : MonoBehaviour, IAttack, ISkill
{
    [SerializeField]
    private List<GameObject> enemys = new List<GameObject>();
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private bool isDelay;
    [SerializeField]
    private int attackSequence = 0;

    public Persona Persona;
    private void OnEnable()
    {
       isDelay = false;
       Persona.CharacterState = CharacterState.Idle;
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
                Persona.CharacterState = CharacterState.Search;
            }

            characterStatePattern();

            if (Persona.CurrentHp <= 0) Persona.CharacterState = CharacterState.Die;
            if (Persona.CurrentMp >= Persona.MaxMp) Persona.CharacterState = CharacterState.Skill;
        }
       
    }

    public void Attack(int power, float attackRange, float delayTime)
    {
        Persona enemyPersona = enemy.GetComponent<Persona>();
        if (enemyPersona.CharacterState == CharacterState.Die) attackSequence++;
        if(!isDelay) StartCoroutine(AttackDelay(delayTime, power, enemyPersona));
    }
    IEnumerator AttackDelay(float delayTime ,int power, Persona enemyPersona)
    {
        Debug.Log("공격");
        enemyPersona.Damaged(power);
        Persona.CurrentMp += 10;
        isDelay = true;
        yield return new WaitForSeconds(delayTime);
        isDelay = false;
    }
    public void Skill()
    {
        Persona.Skill();
    }
    private void returnToInitialState()
    {
        Persona.CharacterState = CharacterState.Idle;
        Persona.CurrentHp = Persona.MaxHp;
        Persona.CurrentMp = Persona.DefaultMp;
        transform.position = TileManager.Instance.BattleTileList[Persona.DiposedIndex].transform.position;
        enemys.Clear();
        isDelay = false;
    }
    private void chase()
    {
        float distance = Vector3.Distance(transform.position, enemy.transform.position);

        if (distance >= Persona.AttackRange)
        {
            Debug.Log("추적");
            transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, Persona.Speed * Time.deltaTime);
        }

        else if(distance <= Persona.AttackRange)
        {
            Persona.CharacterState = CharacterState.Attack;
        }
    }
    private void characterStatePattern()
    {
        switch (Persona.CharacterState)
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
                chase();
                break;
            case CharacterState.Skill:
                break;
            case CharacterState.Die:
                break;
        }
    }
    private void checkEnemy()
    {
        Debug.Log("감지!!!");

        enemys.Clear();

        if (gameObject.tag == "PlayerCharacter")
        {
            GameObject[] enemyObject = GameObject.FindGameObjectsWithTag("EnemyCharacter");
            for (int i = 0; i < enemyObject.Length; i++) enemys.Add(enemyObject[i]);
        }

        else if (gameObject.tag == "EnemyCharacter")
        {
            GameObject[] enemyObject = GameObject.FindGameObjectsWithTag("PlayerCharacter");
            for (int i = 0; i < enemyObject.Length; i++)
            {
                if (!enemyObject[i].GetComponent<Persona>().IsOnBattleField) break;
                enemys.Add(enemyObject[i]);
            }
        }

        if (enemys.Count > 0)
        {
            enemy = enemys[attackSequence];

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

            Persona.CharacterState = CharacterState.Chase;
        }
    }
}