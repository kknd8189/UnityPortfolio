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

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        CharacterState = CharacterState.Idle;
    }

    private void Update()
    {
        if (Persona.IsOnBattleField)
        {
            ///��Ʋ ����� ������
            if (GameManager.Instance.GameState == GAMESTATE.Battle && GameManager.Instance.IsOver)
            {
                returnToInitialState();
            }
            //�غ� ����� ������
            else if (GameManager.Instance.GameState == GAMESTATE.StandBy && GameManager.Instance.IsOver)
            {
                CharacterState = CharacterState.Search;
            }

            if (Persona.CurrentMp >= Persona.MaxMp)
            {
                CharacterState = CharacterState.Skill;
            }

            if (Persona.CurrentHp <= 0)
            {
                Die();
            }


            //���� ������Ʈ �̺�Ʈ
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
        enemys.Clear();

        if (gameObject.tag == "PlayerCharacter")
        {
            GameObject[] enemyObject = GameObject.FindGameObjectsWithTag("EnemyCharacter");
            for (int i = 0; i < enemyObject.Length; i++)
            {
                enemys.Add(enemyObject[i]);
            }
        }

        else if (gameObject.tag != "PlayerCharacter")
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
                float distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

                if (distance < shortDis)
                {
                    enemy = found;
                    shortDis = distance;
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

        if(enemyPersona.CurrentHp <= 0)
        {
            attackSequence++;
            enemy = enemys[attackSequence];
        }
    }
    public void Die()
    {
        if (gameObject.tag == "PlayerCharacter") Persona.Player.LiveCharacterCount--;
        else if (gameObject.tag != "PlayerCharacter") Persona.Player.Enemy.LiveEnemyCount--;
    }
}