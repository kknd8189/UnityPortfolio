using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public enum GAMESTATE
{
    StandBy, Battle
}

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion
    public UnityEvent<int> OnTimeChanged = new UnityEvent<int>();
    public RerollManager rerollManager;

    public int Turn;
    public bool IsOver;
    public float NextTurnTime;
    public float WaitingTime;
    public GAMESTATE GameState;
    public TextMeshProUGUI TurnText;

    public EnemyGenerator enemyGenerator;

    private void Start()
    {
        GameState = GAMESTATE.StandBy;
        Turn = 1;
        IsOver = false;
        NextTurnTime = 0f;
        WaitingTime = 20.0f;
        TurnText.text = "Turn " + Turn.ToString();
        enemyGenerator.EnemyGenerate(Turn);
    }
    private void Update()
    {
        NextTurnTime += Time.deltaTime;
        OnTimeChanged?.Invoke((int)NextTurnTime);

        //시간이 되면 게임의 상태를 변경하고 턴을 넘겨준다.
        if (IsOver)
        {
            switch (GameState)  
            {
                case GAMESTATE.StandBy:
                    GameState = GAMESTATE.Battle;
                    break;
                case GAMESTATE.Battle:
                    Turn += 1;
                    TurnText.text = $"Turn {Turn}";
                    if (Turn > 20) GameOver();
                    GameState = GAMESTATE.StandBy;
                    rerollManager.freeReroll();
                    enemyGenerator.EnemyGenerate(Turn);
                    break;
            }
            IsOver = false;
        }

        if (NextTurnTime >= WaitingTime)
        {
            IsOver = true;
            NextTurnTime = 0;
        }
    }
    public void GameOver()
    {
        Debug.Log("GameOver");
    }
    public void Win()
    {
        Debug.Log("YouWin");
    }
}
