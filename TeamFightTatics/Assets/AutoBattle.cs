using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState { Idle, Search, Attack, Chase, Skill, Die }


public class AutoBattle : MonoBehaviour, IAttack, ISkill
{
    public Persona enemyPersona;
    public Persona Persona;
    public Player Player;

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

    private void Awake()
    {
        anim = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Persona = GetComponent<Persona>();
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
        if (enemyPersona.CurrentHp <= 0)
        {
            CharacterState = CharacterState.Chase;
        }

        anim.Play("Attack");

        if (Persona.CurrentMp >= Persona.MaxMp)
        {
            CharacterState = CharacterState.Skill;                    
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
        }

        else if (enemys.Count <= 0)
        {
            CharacterState = CharacterState.Idle;
        }
    }
    public void Attack()
    {
        enemyPersona.Damaged(Persona.Power);
        Persona.CurrentMp += Persona.RecoverMp;
    }
}