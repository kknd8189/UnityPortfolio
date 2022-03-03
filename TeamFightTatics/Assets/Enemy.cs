using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;


public class Enemy : Entity
{
    public UnityEvent<int> CurrentHpChanged = new UnityEvent<int>();
    public Player Player;
    public List<GameObject> EnemyPersonaList = new List<GameObject>();

    public new int CurrentHp
    {
        get { return _currentHp; }
        set
        {
            _currentHp = value;
            CurrentHpChanged?.Invoke(_currentHp);
            if (_currentHp <= 0)
            {
                GameManager.Instance.Win();
                Destroy(Player.gameObject);
            }
        }
    }
    [SerializeField]
    private int _liveEnemyCount = 0;
    public int LiveEnemyCount
    {
        get { return _liveEnemyCount; }
        set { _liveEnemyCount = value; }
    }

    private bool _isShoot;
    private void Start()
    {

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        _maxHp = 100;
        _currentHp = _maxHp;
    }
    private void Update()
    { 
        if (GameManager.Instance.GameState == GAMESTATE.Battle)
        {
            if (GameManager.Instance.IsOver)
            {
                _isShoot = false;
            }
            if (LiveEnemyCount > 0 && Player.LiveCharacterCount > 0 && GameManager.Instance.NextTurnTime >= 19.5f) Shoot();

            if (Player.LiveCharacterCount <= 0)
            {
                Shoot();
            }
        }
    }
    public override void Damaged(int damage)
    {
        CurrentHp -= damage;
    }
    public void Shoot()
    {
        if (!_isShoot)
        {
            int damage = _liveEnemyCount + GameManager.Instance.Turn;
            PoolManager.Instance.PullArrowQueue(damage, transform.position + transform.up * 20f, Player.gameObject);
            _isShoot = true;
        }
    }
}
