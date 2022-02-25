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

    public GameObject ExplainUI;
    public TextMeshProUGUI CharacterName;
    public TextMeshProUGUI CharacterStat;
    public TextMeshProUGUI SkillExplain;

    private bool _isExplainActive = false;
    private string[] synergyName = { "Archor", "Warrior", "Magician", "Beast", "Orc", "Undead", "Human", "Elf" };
    #region Singleton
    public static UIManager Instance = null;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }
    #endregion
    private void Start()
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
    public void ExplainCharacter(string name, int[] synergyNum, int cost, string skillExplain, int hp, int power, float attackRange, int maxMP, int currentMp)
    {
        if (!_isExplainActive)
        {
            CharacterName.text = $"Name : {name}";
            CharacterStat.text = $"Cost {cost} Synergy {synergyName[synergyNum[0]]} / {synergyName[synergyNum[1]]}\nHP:{hp} ATK:{power}\nMP:{currentMp}/{maxMP} AttackRange:{attackRange}";
            SkillExplain.text = $"Skill : {skillExplain}";
            ExplainUI.SetActive(true);
            ExplainUI.transform.position = Input.mousePosition;
            _isExplainActive = true;
        }
        else
        {
            ExplainUI.SetActive(false);
            _isExplainActive = false;
        }

    }
}
