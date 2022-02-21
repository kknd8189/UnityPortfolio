using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public List<CharacterSciptableObject> CharacterDataList;
    public Queue<GameObject>[] CardQueue;
    public Queue<GameObject>[] CharacterQueue;

    public Queue<GameObject> ArrowPool = new Queue<GameObject>();
    public GameObject ArrowPrefab;

    public int Amount = 20;
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

        CardQueue = new Queue<GameObject>[CharacterDataList.Count];
        CharacterQueue = new Queue<GameObject>[CharacterDataList.Count];

        for (int i = 0; i < CardQueue.Length; ++i)
        {
            CardQueue[i] = new Queue<GameObject>();
            CharacterQueue[i] = new Queue<GameObject>();
        }
        init(Amount);
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

           createArrow();
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
        GameObject personaObject = Instantiate(CharacterDataList[CharacterListIndex].CharacterPrefab, transform);
        Persona persona = personaObject.GetComponent<Persona>();
        persona.CharacterNum = CharacterDataList[CharacterListIndex].CharacterNum;
        persona.MaxHp = CharacterDataList[CharacterListIndex].MaxHP;
        persona.Power = CharacterDataList[CharacterListIndex].Power;
        persona.CurrentHp = persona.MaxHp;
        persona.MaxMp = CharacterDataList[CharacterListIndex].MaxMP;
        persona.DefaultMp = CharacterDataList[CharacterListIndex].DefaultMP;
        persona.AttackRange = CharacterDataList[CharacterListIndex].AttackRange;
        persona.AttackDelayTime = CharacterDataList[CharacterListIndex].AttackDelay;
        persona.Cost = CharacterDataList[CharacterListIndex].Cost;
        persona.Speed = CharacterDataList[CharacterListIndex].Speed;
        personaObject.SetActive(false);
        CharacterQueue[CharacterListIndex].Enqueue(personaObject);
    }
    public void PushCharacterQueue(int CharacterNum, GameObject personaPrefab)
    {
        personaPrefab.SetActive(false);
        personaPrefab.transform.SetParent(transform);
        CharacterQueue[CharacterNum].Enqueue(personaPrefab);
    }
    private void createArrow()
    {
        GameObject arrowPrefab = Instantiate(ArrowPrefab, transform);
        arrowPrefab.SetActive(false);
        ArrowPool.Enqueue(arrowPrefab);
    }

    public void PushArrowQueue(GameObject arrowPrefab)
    {
        arrowPrefab.SetActive(false);
        arrowPrefab.transform.SetParent(transform);
        ArrowPool.Enqueue(arrowPrefab);
    }
    public GameObject PullArrowQueue(int power, Vector3 StartPosition, GameObject target)
    {
        GameObject arrowPrefab;
        arrowPrefab = ArrowPool.Dequeue();
        arrowPrefab.SetActive(true);
        arrowPrefab.transform.SetParent(null);
        arrowPrefab.transform.position = StartPosition;
        Arrow arrow = arrowPrefab.GetComponent<Arrow>();
        arrow.SetPower(power);
        arrow.SetDest(target);

        return arrowPrefab;
    }
}

