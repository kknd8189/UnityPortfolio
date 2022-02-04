using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GAMESTATE
{
    StanBy, Battle
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

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public int Turn;
    public bool IsOver;
    public float NextTurnTime;
    public float WaitingTime;
    public GAMESTATE GameState;


    public TextMeshProUGUI RestTimeText;
    public TextMeshProUGUI TurnText;

    private void Start()
    {
        GameState = GAMESTATE.StanBy;

        Turn = 1;
        IsOver = false;
        NextTurnTime = 0f;
        WaitingTime = 30f;
    }

    private void Update()
    {
        NextTurnTime += Time.deltaTime;

        if (IsOver)
        {
            switch (GameState)
            {
                case GAMESTATE.StanBy:
                    GameState = GAMESTATE.Battle;
                    break;
                case GAMESTATE.Battle:
                    Turn += 1;
                    GameState = GAMESTATE.StanBy;
                    break;
            }

            IsOver = false;
        }

     if(NextTurnTime >= WaitingTime)
        {
            IsOver = true;
            NextTurnTime = 0;
        }

        TurnText.text = "Turn " + Turn.ToString();
        RestTimeText.text = "RestTime " + ((int)(WaitingTime - NextTurnTime)).ToString();
    }
}
