using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public List<CharacterSciptableObject> CharacterDataList;
    public Queue<GameObject>[] CardQueue = new Queue<GameObject>[2];
    public Queue<GameObject>[] CharacterQueue = new Queue<GameObject>[2];
    private int _onSummonFieldCount = 0;
    public int OnSummonFieldCount
    {
        get { return _onSummonFieldCount; }
        set { _onSummonFieldCount = value; }
    }
    [SerializeField]
    private int amount = 20;
    #region Singleton
    public static PoolManager Instance = null;
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
        for (int i = 0; i < CardQueue.Length; ++i)
        {
            CardQueue[i] = new Queue<GameObject>();
            CharacterQueue[i] = new Queue<GameObject>();
        }
        init(amount);
    }
    #endregion
    private void init(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            for (int j = 0; j < CharacterDataList.Count; j++)
            {
                setCardStatus(j);
                setPersonaStatus(j);
            }
        }
    }
    private void setCardStatus(int CharacterListIndex)
    {
        GameObject cardPrefab = Instantiate(CharacterDataList[CharacterListIndex].CardPrefab, transform);
        Character character = cardPrefab.GetComponent<Character>();
        character.CharacterNum = CharacterDataList[CharacterListIndex].CharacterNum;
        cardPrefab.SetActive(false);
        CardQueue[CharacterListIndex].Enqueue(cardPrefab);
    }
    private void setPersonaStatus(int CharacterListIndex)
    {
        GameObject personaObject = Instantiate(CharacterDataList[CharacterListIndex].CharacterPrefab,transform);
        Persona persona = personaObject.GetComponent<Persona>();
        persona.CharacterNum = CharacterDataList[CharacterListIndex].CharacterNum;
        persona.MaxHp = CharacterDataList[CharacterListIndex].MaxHP;
        persona.CurrentHp = persona.MaxHp;
        persona.MaxMp = CharacterDataList[CharacterListIndex].MaxMP;
        persona.DefaultMp = CharacterDataList[CharacterListIndex].DefaultMP;
        persona.AttackRange = CharacterDataList[CharacterListIndex].AttackRange;
        persona.AttackDelayTime = CharacterDataList[CharacterListIndex].AttackDelay;
        persona.Cost = CharacterDataList[CharacterListIndex].Cost;
        personaObject.SetActive(false);
        CharacterQueue[CharacterListIndex].Enqueue(personaObject);
    }
}

