using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState { Idle, Search, Attack, Chase, Skill, Die }


public class AutoBattle : MonoBehaviour, IAttack, ISkill
{
    [SerializeField]
    private List<GameObject> enemys = new List<GameObject>();
    [SerializeField]
    private GameObject enemy;

    public Animator anim;
    [SerializeField]
    private CharacterState _characterState;
    public CharacterState CharacterState
    {
        get { return _characterState; }

        set
        {
            _characterState = value;

            switch (CharacterState)
            {
                case CharacterState.Idle:
                    //anim.Play("Idle");
                    break;
                case CharacterState.Search:
                    checkEnemy();
                    break;
                case CharacterState.Attack:
                    //anim.Play("Attack");
                    break;
                case CharacterState.Chase:
                   // anim.Play("Chase");
                    break;
                case CharacterState.Skill:
                  //  anim.Play("Skill");
                    break;
                case CharacterState.Die:
                   // anim.Play("Die");
                    break;
            }
        }
    }

    [SerializeField]
    private int attackSequence = 0;

    public Persona enemyPersona;
    public Persona Persona;

    private void OnEnable()
    {
        CharacterState = CharacterState.Idle;
       // anim = GetComponent<Animator>();
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
                CharacterState = CharacterState.Search;
            }

            if (Persona.CurrentMp >= Persona.MaxMp) CharacterState = CharacterState.Skill;
            if (Persona.CurrentHp <= 0) Die();


            //상태 업데이트 이벤트
            switch (CharacterState)
            {
                case CharacterState.Idle:
                    break;
                case CharacterState.Attack:
                    break;
                case CharacterState.Chase:
                    chase();
                    break;
            }
        }
    }
    private void returnToInitialState()
    {
        CharacterState = CharacterState.Idle;
        Persona.CurrentHp = Persona.MaxHp;
        Persona.CurrentMp = Persona.DefaultMp;
        transform.position = TileManager.Instance.BattleTileList[Persona.DiposedIndex].transform.position;
        enemys.Clear();
    }
    private void chase()
    {
        float distance = Vector3.Distance(transform.position, enemy.transform.position);

        if (distance > Persona.AttackRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, Persona.Speed * Time.deltaTime);
            transform.LookAt(enemy.transform);
        }

        else if (distance <= Persona.AttackRange)
        {
            CharacterState = CharacterState.Attack;
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

            CharacterState = CharacterState.Chase;
        }
    }
    public void Skill()
    {
        Persona.CurrentMp = 0;
        Persona.Skill();
        CharacterState = CharacterState.Chase;
    }
    public void Attack(int power, float delayTime)
    {
        enemyPersona = enemy.GetComponent<Persona>();
        enemyPersona.Damaged(power);
        Persona.CurrentMp += 10;
    }
    public void Die()
    {
        if (gameObject.tag == "PlayerCharacter") Persona.player.LiveCharacterCount--;
        else if (gameObject.tag == "EnemyCharacter") Persona.enemy.LiveEnemyCount--;
        CharacterState = CharacterState.Die;
    }
}