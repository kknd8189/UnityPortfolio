using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Player Player;
    public Enemy Enemy;

    public TextMeshProUGUI EXPtm;
    public TextMeshProUGUI HPtm;
    public TextMeshProUGUI Leveltm;
    public TextMeshProUGUI Goldtm;
    public TextMeshProUGUI Enemytm;
    public TextMeshProUGUI RestTimeText;

    void Start()
    {
        Player.OnGoldChanged.AddListener(UpdateGoldText);
        Player.CurrentExpChanged.AddListener(UpdateExpText);
        Player.CurrentHpChanged.AddListener(UpdateHpText);
        Enemy.CurrentHpChanged.AddListener(UpdateEnemyHpText);
        GameManager.Instance.OnTimeChanged.AddListener(UpdateRestTimeText);
    }
    private void OnDestroy()
    {
        Player.OnGoldChanged.RemoveListener(UpdateGoldText);
        Player.CurrentExpChanged.RemoveListener(UpdateExpText);
        Player.CurrentHpChanged.RemoveListener(UpdateHpText);
        Enemy.CurrentHpChanged.RemoveListener(UpdateEnemyHpText);
    }

    public void UpdateGoldText(int gold)
    {
        Goldtm.text = $"{gold}";
    }

    public void UpdateExpText(int exp)
    {
        if (Player.Level >= 9) EXPtm.text = "MAX";
        else EXPtm.text = $"{exp}/{Player.MaxExp}";
        Leveltm.text = $"Level {Player.Level}";
    }

    public void UpdateHpText(int hp)
    {
        HPtm.text = Player.CurrentHp.ToString();
    }
    public void UpdateEnemyHpText(int hp)
    {
        Enemytm.text = Enemy.CurrentHp.ToString();
    }

    public void UpdateRestTimeText(int time)
    {
        RestTimeText.text = ((int)(GameManager.Instance.WaitingTime - GameManager.Instance.NextTurnTime)).ToString();
    }
}

