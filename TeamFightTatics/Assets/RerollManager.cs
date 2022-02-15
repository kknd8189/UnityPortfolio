using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RerollManager : MonoBehaviour
{
    public Player player;

    public List<GameObject> OnPanelList = new List<GameObject>();
    public Transform cardPanel;

    public void Start()
    {
        freeReroll();
    }
    public void Reroll()
    {
        if (player.Gold >= 2)
        {
            freeReroll();
            player.Gold -= 2;
        }
    }
    public void freeReroll()
    {
        int maxCount = OnPanelList.Count - 1;

        for (int i = maxCount; i > -1; i--)
        {
            eraseCard(i);
        }

        for (int i = 0; i < 5; i++)
        {
            RandomCardPick(i);
        }
    }
    public void RandomCardPick(int i)
    {
        int randomIndex = Random.Range(0, PoolManager.Instance.CharacterDataList.Count);
        GameObject randomCard = PoolManager.Instance.CardQueue[randomIndex].Dequeue();
        setCard(randomCard);
        randomCard.GetComponent<Character>().CardIndex = i;
    }
    private void setCard(GameObject card)
    {
        card.transform.SetParent(cardPanel);
        card.SetActive(true);
        OnPanelList.Add(card);
    }
    public void eraseCard(int index)
    {
        OnPanelList[index].transform.SetParent(transform);
        PoolManager.Instance.CardQueue[OnPanelList[index].GetComponent<Character>().CharacterNum].Enqueue(OnPanelList[index]);
        OnPanelList[index].SetActive(false);
        OnPanelList.RemoveAt(index);

        for (int i = 0; i < OnPanelList.Count; i++)
        {
            OnPanelList[i].GetComponent<Character>().CardIndex = i;
        }
    }
}
