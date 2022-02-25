using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Player Player;
    public Enemy Enemy;
    public GameObject ExplainUI;
    public GameObject SynergyExplainUI;

    public TextMeshProUGUI EXPtm;
    public TextMeshProUGUI HPtm;
    public TextMeshProUGUI Leveltm;
    public TextMeshProUGUI Goldtm;
    public TextMeshProUGUI Enemytm;
    public TextMeshProUGUI RestTimeText;

 
    public TextMeshProUGUI CharacterName;
    public TextMeshProUGUI CharacterStat;
    public TextMeshProUGUI SkillText;
    public TextMeshProUGUI SynergyText;

    public List<GameObject> SynergyUI = new List<GameObject>();
    private Vector3[] _synergyUIPosition = new Vector3[8];
    private bool[] _isLocated = new bool[8];

    private bool _isExplainActive = false;
    private bool _isSynergyUIActivate = false;

    private string[] synergyName = { "Archor", "Warrior", "Magician", "Beast", "Orc", "Undead", "Human", "Elf" };
    private string[] synergyExplain = new string[8];


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
        ExplainSynergyInit();

        for(int i = 0; i < _synergyUIPosition.Length; i++)
        {
            _synergyUIPosition[i] = new Vector3(-370f, 140f - 50 * i, 0);
        }
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
            SkillText.text = $"Skill : {skillExplain}";
            ExplainUI.transform.position = Input.mousePosition;
            ExplainUI.SetActive(true);
            _isExplainActive = true;
        }
        else
        {
            ExplainUI.SetActive(false);
            _isExplainActive = false;
        }

    }
    private void ExplainSynergyInit()
    {
        synergyExplain[0] = "사정거리가 10만큼 늘어난다";
        synergyExplain[1] = "공격력이 10증가";
        synergyExplain[2] = "필요 마나량 10 감소";
        synergyExplain[3] = "최대 체력 50 증가";
        synergyExplain[4] = "공격속도 증가";
        synergyExplain[5] = "10초 후에 죽지 않았다면 체력 회복";
        synergyExplain[6] = "턴이 끝나면 골드 2와 경험치 2 추가 획득";
        synergyExplain[7] = "초당 마나 회복 2";
    }
    public void ExplainSynergy(int synergyNum)
    {
        if (!_isSynergyUIActivate)
        {
            SynergyExplainUI.transform.position = new Vector3(Input.mousePosition.x + 350, Input.mousePosition.y, Input.mousePosition.z);

            SynergyExplainUI.SetActive(true);
            SynergyText.text = synergyExplain[synergyNum];
            _isSynergyUIActivate = true;
        }
        else
        {
            SynergyExplainUI.SetActive(false);
            _isSynergyUIActivate = false;
        }
    }
    public void SynergyActivate(int synergyNum)
    {
        SynergyUI[synergyNum].SetActive(true);
        SynergyUI synergyUI = SynergyUI[synergyNum].GetComponent<SynergyUI>();

        for (int i = 0; i < _synergyUIPosition.Length; i++)
        {
            if (!_isLocated[i])
            {
                SynergyUI[synergyNum].transform.localPosition = _synergyUIPosition[i];
                synergyUI.LocatedSequence = i;
                _isLocated[i] = true;
                return;
            }
        }
    }
    public void SynergyDeactivate(int synergyNum)
    {
        SynergyUI synergyUI = SynergyUI[synergyNum].GetComponent<SynergyUI>();
        _isLocated[synergyUI.LocatedSequence] = false;
        SynergyUI[synergyNum].SetActive(false);
    }
}

