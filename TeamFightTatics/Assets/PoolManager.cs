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


    private List<GameObject> OnPanelList = new List<GameObject>();

    private Queue<GameObject> orcArchorCardQueue = new Queue<GameObject>();
    private Queue<GameObject> skeletonArchorCardQueue = new Queue<GameObject>();

    public Transform cardPanel;

    public GameObject playerObject;
    public Player player;


    private void Start()
    {
        init(20);
        player = playerObject.GetComponent<Player>();
        freeReroll();
    }

    private void init(int Amount)
    {
        for (int i = 0; i < Amount; i++)
        {
            GameObject cardPrefab = Instantiate(CharacterDataList[0].CardPrefab, transform);
            cardPrefab.SetActive(false);
            orcArchorCardQueue.Enqueue(cardPrefab);
        }

        for (int i = 0; i < Amount; i++)
        {
            GameObject cardPrefab = Instantiate(CharacterDataList[1].CardPrefab, transform);
            cardPrefab.SetActive(false);
            skeletonArchorCardQueue.Enqueue(cardPrefab);
        }
    }

    public void Reroll()
    {
        if (player.gold >= 2)
        {
            eraseCard();

            player.MinusGold();

            for (int i = 0; i < 5; i++)
            {
                int rand = Random.Range(0, CharacterDataList.Count);

                switch (rand)
                {
                    case 0:
                        setCard(orcArchorCardQueue.Dequeue());
                        break;

                    case 1:
                        setCard(skeletonArchorCardQueue.Dequeue());
                        break;
                }

            }
        }
    }
    public void freeReroll()
    {
        eraseCard();

        for (int i = 0; i < 5; i++)
        {
            int rand = Random.Range(0, CharacterDataList.Count);

            switch (rand)
            {
                case 0:
                    setCard(orcArchorCardQueue.Dequeue());
                    break;

                case 1:
                    setCard(skeletonArchorCardQueue.Dequeue());
                    break;
            }
        }
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
            OnPanelList[i].SetActive(false);
            OnPanelList[i].transform.SetParent(transform);
        }
    }
}

