using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum CharacterState { Idle, Search, Attack, Chase, Skill, Die }


public class AutoBattle : MonoBehaviour, IAttack, ISkill
{
    public Persona enemyPersona;
    public Persona Persona;
    public Player Player;

    [SerializeField]
    public List<GameObject> enemys = new List<GameObject>();
    public GameObject Enemy;
    public Animator anim;

    private int _bonusPower;
    public int BonusPower
    {
        get { return _bonusPower; }
        set { _bonusPower = value; }
    }
    private Enemy enemy;


    [SerializeField]
    private CharacterState _characterState;
    public CharacterState CharacterState
    {
        get { return _characterState; }

        set
        {
            _characterState = value;

            switch (_characterState)
            {
                case CharacterState.Idle:
                    break;
                case CharacterState.Search:
                    checkEnemy();
                    break;
                case CharacterState.Attack:
                    anim.speed = Persona.AttackDelayTime;
                    break;
                case CharacterState.Chase:
                    updateNextSequence();
                    break;
                case CharacterState.Skill:
                    anim.Play("Skill");
                    break;
                case CharacterState.Die:
                    anim.Play("Die");
                    break;
            }
        }
    }

    private bool _isDie;

    public NavMeshAgent Agent;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Persona = GetComponent<Persona>();
        CharacterState = CharacterState.Idle;
        Agent = GetComponent<NavMeshAgent>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
    }
    private void Update()
    {
        if (Persona.IsOnBattleField)
        {
            if (GameManager.Instance.GameState == GAMESTATE.Battle)
            {
                if (GameManager.Instance.IsOver)
                {
                    returnToInitialState();
                }
            }
            else if (GameManager.Instance.GameState == GAMESTATE.StandBy && GameManager.Instance.IsOver)
            {
                CharacterState = CharacterState.Search;
            }

            if (Persona.CurrentHp <= 0 && !_isDie)
            {
                CharacterState = CharacterState.Die;
                
                if (gameObject.tag == "PlayerCharacter")
                {
                   Player.LiveCharacterCount--;
                }
                else if (gameObject.tag != "PlayerCharacter")
                {
                   Player.Enemy.LiveEnemyCount--;
                }

                _isDie = true;
            }

            //상태 업데이트 이벤트
            switch (CharacterState)
            {
                case CharacterState.Idle:
                    updateIdle();
                    break;
                case CharacterState.Attack:
                    updateAttack();
                    break;
                case CharacterState.Chase:
                    updateChase();
                    break;
                case CharacterState.Die:
                    updateDie();
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
        BonusPower = 0;

        float dir = 0;

        if (gameObject.tag == "PlayerCharacter" && _isDie)
        {
            dir = 0f;
            if (_isDie) Player.LiveCharacterCount++;
        }
        else if (gameObject.tag != "PlayerCharacter" )
        {
            dir = 180f;

            if(_isDie) Player.Enemy.LiveEnemyCount++;
        }

        transform.rotation = Quaternion.Euler(0f, dir, 0f);
        _isDie = false;
        enemys.Clear();
        Enemy = null;
        Agent.SetDestination(transform.position);
    }
    private void updateIdle()
    {
        anim.Play("Idle");
    }
    private void updateChase()
    {
        anim.Play("Chase");
        Agent.SetDestination(Enemy.transform.position);

        float distance = Vector3.Distance(transform.position, Enemy.transform.position);

        if (distance <= Persona.AttackRange)
        {
            Agent.speed = 0;
            CharacterState = CharacterState.Attack;
        }

        //적이 죽을 경우 다시 찾으러가자
        if (enemyPersona.CurrentHp <= 0)
        {
            CharacterState = CharacterState.Chase;
        }
    }
    private void updateAttack()
    {
        anim.Play("Attack");

        if (Persona.CurrentMp >= Persona.MaxMp)
        {
            CharacterState = CharacterState.Skill;
        }

        if (enemyPersona.CurrentHp <= 0)
        {
            CharacterState = CharacterState.Chase;
        }
    }

    private void updateDie()
    {
        //죽었을 경우 아무것도 하지 않는다.
    }
    public void Skill()
    {
        Persona.CurrentMp = 0;
        CharacterState = CharacterState.Chase;
    }
    private void checkEnemy()
    {
        enemys.Clear();

        if (gameObject.tag == "PlayerCharacter")
        {
            for (int i = 0; i < enemy.EnemyPersonaList.Count; i++)
            {
                enemys.Add(enemy.EnemyPersonaList[i]);
            }
        }

        else if (gameObject.tag != "PlayerCharacter")
        {
            for (int i = 0; i < Player.OnBattleCharacterList.Count; i++)
            {
                enemys.Add(Player.OnBattleCharacterList[i]);
            }
        }

        Agent.speed = Persona.Speed;
        Agent.stoppingDistance = Persona.AttackRange;
        CharacterState = CharacterState.Chase;
    }
    private void updateNextSequence()
    {
        for (int i = 0; i < enemys.Count; i++)
        {
            if (enemys[i].GetComponent<Persona>().CurrentHp <= 0)
            {
                enemys.RemoveAt(i);
            }
        }

        if (enemys.Count > 0)
        {
            Enemy = enemys[0];

            float shortDis = Vector3.Distance(gameObject.transform.position, enemys[0].transform.position);

            foreach (GameObject found in enemys)
            {
                float distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

                if (distance < shortDis)
                {
                    Enemy = found;
                    shortDis = distance;
                }
            }

            enemyPersona = Enemy.GetComponent<Persona>();
            Agent.speed = Persona.Speed;

        }

        else if (enemys.Count <= 0)
        {
            CharacterState = CharacterState.Idle;
            Agent.SetDestination(transform.position);
        }       
    }
    public void Attack()
    {
        enemyPersona.Damaged(Persona.Power + BonusPower);
        Persona.CurrentMp += Persona.RecoverMp;
    }
}