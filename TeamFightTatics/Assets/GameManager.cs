using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GAMESTATE
{
    StandBy, Battle, GameSet
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

    public Material[] skybox;

    public GameObject VictoryImage;
    public GameObject DefeatImage;

    private void Start()
    {
        int rand = Random.Range(0, 5);
        RenderSettings.skybox = skybox[rand];

        GameState = GAMESTATE.StandBy;
        Turn = 1;
        IsOver = false;
        NextTurnTime = 0f;
        WaitingTime = 20.0f;
        TurnText.text = "Turn " + Turn.ToString();
    }
    private void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 7.0f);

        if (GameState != GAMESTATE.GameSet)
        {
            NextTurnTime += Time.deltaTime;
            OnTimeChanged?.Invoke((int)NextTurnTime);
        }

        else if(GameState == GAMESTATE.GameSet && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("LoadingScene");
        }
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
                    GameState = GAMESTATE.StandBy;
                    rerollManager.freeReroll();
                    break;
            }
            IsOver = false;
        }

        if (NextTurnTime >= WaitingTime)
        {
            IsOver = true;
            NextTurnTime = 0;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
    public void GameOver()
    {
        GameState = GAMESTATE.GameSet;
        DefeatImage.SetActive(true);
        StartCoroutine(FadeOut(DefeatImage));
        //Debug.Log("GameOver");
    }
    public void Win()
    {
        GameState = GAMESTATE.GameSet;
        VictoryImage.SetActive(true);
        StartCoroutine(FadeOut(VictoryImage));
        //Debug.Log("YouWin");
    }

    IEnumerator FadeOut(GameObject imageObejct)
    {
        float fadeCount = 0;
        Image image = imageObejct.GetComponent<Image>();

        while (fadeCount < 1.0f)
        {
            fadeCount += 0.1f;

            yield return new WaitForSeconds(0.1f);

            image.color = new Color(255, 255, 255, fadeCount);
        }
    }
}
