using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
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
    }
    #endregion

    [SerializeField]
    private List<CharacterSciptableObject> CharacterDataList;

    [SerializeField]
    private List<GameObject> OnPanelList = new List<GameObject>();

    public Queue<GameObject>[] CardQueue = new Queue<GameObject>[2];
    public Queue<GameObject>[] CharacterQueue = new Queue<GameObject>[2];

    public Transform cardPanel;
    public Player Player;

    private void Start()
    {
        for (int i = 0; i < CardQueue.Length; ++i)
        {
            CardQueue[i] = new Queue<GameObject>();
            CharacterQueue[i] = new Queue<GameObject>(); 
        }

        init(10);

        freeReroll();
    }
    private void init(int Amount)
    {
        for (int i = 0; i < Amount; i++)
        {
            for (int j = 0; j < CharacterDataList.Count; j++)
            {
                setCardStatus(j);
                setCharacterStatus(j);
            }
        }
    }
    private void RerollHelper()
    {
        int maxCount = OnPanelList.Count - 1;

        for (int i = maxCount; i > -1; i--)
        {
            eraseCard(i);
        }

        for (int i = 0; i < 5; i++)
        {
            int randomIndex = Random.Range(0, CharacterDataList.Count);
            GameObject randomCard = CardQueue[randomIndex].Dequeue();
            setCard(randomCard);
            randomCard.GetComponent<Character>().CardIndex = i;
        }
    }
    public void Reroll()
    {
        if (Player.Gold >= 2)
        {
            RerollHelper();
            Player.Gold -= 2;
        }
    }
    public void freeReroll()
    {
        RerollHelper();
    }
    private void setCard(GameObject card)
    {
        card.transform.SetParent(cardPanel);
        card.SetActive(true);
        OnPanelList.Add(card);
    }
    private void eraseCard(int index)
    {
        OnPanelList[index].transform.SetParent(transform);
        CardQueue[OnPanelList[index].GetComponent<Character>().CharacterNum].Enqueue(OnPanelList[index]);
        OnPanelList[index].SetActive(false);
        OnPanelList.RemoveAt(index);
    }
    private void setCardStatus(int CharacterListIndex)
    {
        GameObject cardPrefab = Instantiate(CharacterDataList[CharacterListIndex].CardPrefab, transform);
        Character character = cardPrefab.GetComponent<Character>();
        character.CharacterNum = CharacterDataList[CharacterListIndex].CharacterNum;
        cardPrefab.SetActive(false);
        CardQueue[CharacterListIndex].Enqueue(cardPrefab);
    }
    public void setCharacterStatus(int CharacterListIndex)
    {
        GameObject characterPrefab = Instantiate(CharacterDataList[CharacterListIndex].CharacterPrefab,transform);
        Character character = characterPrefab.GetComponent<Character>();
        character.CharacterNum = CharacterDataList[CharacterListIndex].CharacterNum;
        character.MaxHp = CharacterDataList[CharacterListIndex].MaxHP;
        character.CurrentHp = character.MaxHp;
        character.maxMP = CharacterDataList[CharacterListIndex].MaxMP;
        character.defaultMP = CharacterDataList[CharacterListIndex].DefaultMP;
        character.attackRange = CharacterDataList[CharacterListIndex].AttackRange;
        character.attackDelay = CharacterDataList[CharacterListIndex].AttackDelay;
        character.cost = CharacterDataList[CharacterListIndex].Cost;
        characterPrefab.SetActive(false);
        CharacterQueue[CharacterListIndex].Enqueue(characterPrefab);
    }
    public void SummonHelper(int characterNum)
    {
        if (Player.Gold < CharacterDataList[characterNum].Cost) return;
        GameObject characterPrefab;
        characterPrefab = CharacterQueue[characterNum].Dequeue();
        Character character = characterPrefab.GetComponent<Character>();    
        characterPrefab.SetActive(true);
        characterPrefab.transform.SetParent(null);
        characterPrefab.transform.position = new Vector3(0, 0, 0);
        Player.Gold -= character.cost;

        eraseCard(character.CardIndex);
    }
}

