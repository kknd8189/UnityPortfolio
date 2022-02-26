using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState { Idle, Search, Attack, Chase, Skill, Die }


public class AutoBattle : MonoBehaviour, IAttack, ISkill
{
    public Persona enemyPersona;
    public Persona Persona;
    public Player Player;

    public ParticleSystem BloodParticle;

    [SerializeField]
    private List<GameObject> enemys = new List<GameObject>();
    public GameObject Enemy;
    public Animator anim;

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
                    break;
                case CharacterState.Chase:
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
    [SerializeField]
    private int attackSequence = 0;
    private bool _isDie;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        CharacterState = CharacterState.Idle;
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
            }
        }
    }
    private void returnToInitialState()
    {
        CharacterState = CharacterState.Idle;
        Persona.CurrentHp = Persona.MaxHp;
        Persona.CurrentMp = Persona.DefaultMp;
        transform.position = TileManager.Instance.BattleTileList[Persona.DiposedIndex].transform.position;
        attackSequence = 0;
        if (gameObject.tag == "PlayerCharacter" && _isDie)
        {
            Player.LiveCharacterCount++;
        }
        else if (gameObject.tag != "PlayerCharacter" && _isDie)
        {
            Player.Enemy.LiveEnemyCount++;
        }
        _isDie = false;
        enemys.Clear();
    }
    private void updateIdle()
    {
        anim.Play("Idle");
    }
    private void updateChase()
    {
        anim.Play("Chase");

        float distance = Vector3.Distance(transform.position, Enemy.transform.position);

        if (distance > Persona.AttackRange)
        {
            transform.position = Vector3.MoveTowards(transform.position,Enemy.transform.position, Persona.Speed * Time.deltaTime);
            transform.LookAt(Enemy.transform);
        }

        else if (distance <= Persona.AttackRange)
        {
            CharacterState = CharacterState.Attack;
        }
    }
    private void updateAttack()
    {
        UpdateNextSequence();

        anim.speed = Persona.AttackDelayTime;

        anim.Play("Attack");

        if (Persona.CurrentMp >= Persona.MaxMp)
        {
            CharacterState = CharacterState.Skill;                    
        }
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
            Enemy = enemys[attackSequence];

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

            CharacterState = CharacterState.Chase;
        }
    }
    private void UpdateNextSequence()
    {
        enemyPersona = Enemy.GetComponent<Persona>();

        if (enemyPersona.CurrentHp <= 0)
        { 
            attackSequence++;

            if (attackSequence >= enemys.Count)
            {
                _characterState = CharacterState.Idle;
            }
            else if (attackSequence < enemys.Count)
            {
                Enemy = enemys[attackSequence];
                _characterState = CharacterState.Chase;
            }
            return;
        }
    }
    public void Attack()
    {
        enemyPersona.Damaged(Persona.Power);
        Persona.CurrentMp += Persona.RecoverMp;
    }
}