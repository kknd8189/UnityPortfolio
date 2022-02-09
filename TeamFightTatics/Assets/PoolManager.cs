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

        DontDestroyOnLoad(gameObject);


    }
    #endregion

    [SerializeField]
    private List<CharacterSciptableObject> CharacterDataList;

    [SerializeField]
    private List<GameObject> OnPanelList = new List<GameObject>();

    public Queue<GameObject>[] CardQueue = new Queue<GameObject>[2];
    public Queue<GameObject>[] CharacterQueue = new Queue<GameObject>[2];

    public Transform cardPanel;

    public GameObject playerObject;

    private Player player;

    private void Start()
    {

        for (int i = 0; i < CardQueue.Length; ++i)
        {
            CardQueue[i] = new Queue<GameObject>();
            CharacterQueue[i] = new Queue<GameObject>(); 
        }

        init(10);

        player = playerObject.GetComponent<Player>();

        freeReroll();
    }

    private void init(int Amount)
    {
        for (int i = 0; i < Amount; i++)
        {
            for (int j = 0; j < CharacterDataList.Count; j++)
            {
                setCardStatus(j);
            }
        }
    }

    private void RerollHelper()
    {
        eraseCard();

        for (int i = 0; i < 5; i++)
        {
            int randomIndex = Random.Range(0, CharacterDataList.Count);
            GameObject randomCard = CardQueue[randomIndex].Dequeue();
            setCard(randomCard);
        }
    }
    public void Reroll()
    {
        if (player.gold >= 2)
        {
            RerollHelper();
            player.DeductGold();
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

    private void eraseCard()
    {
        for (int i = 0; i < OnPanelList.Count; i++)
        {       
            OnPanelList[i].transform.SetParent(transform);
            CardQueue[OnPanelList[i].GetComponent<Character>().CharacterNum].Enqueue(OnPanelList[i]);
            OnPanelList[i].SetActive(false);   
        }

        OnPanelList.Clear();
    }

    private void setCardStatus(int CharacterListIndex)
    {
        GameObject cardPrefab = Instantiate(CharacterDataList[CharacterListIndex].CardPrefab, transform);
        Character character = cardPrefab.GetComponent<Character>();
        character.CharacterNum = CharacterDataList[CharacterListIndex].CharacterNum;
        cardPrefab.SetActive(false);
        CardQueue[CharacterListIndex].Enqueue(cardPrefab);
    }

    private void setCharacterStatus(int CharacterListIndex)
    {
        GameObject characterPrefab = Instantiate(CharacterDataList[CharacterListIndex].CardPrefab, transform);
        Character character = characterPrefab.GetComponent<Character>();
        character.CharacterNum = CharacterDataList[CharacterListIndex].CharacterNum;
        character.maxHP = CharacterDataList[CharacterListIndex].MaxHP;
        character.currentHP = character.maxHP;
        character.maxMP = CharacterDataList[CharacterListIndex].MaxMP;
        character.defaultMP = CharacterDataList[CharacterListIndex].DefaultMP;
        character.attackRange = CharacterDataList[CharacterListIndex].AttackRange;
        character.attackDelay = CharacterDataList[CharacterListIndex].AttackDelay;
        characterPrefab.SetActive(false);
        CharacterQueue[CharacterListIndex].Enqueue(characterPrefab);
    }
}

