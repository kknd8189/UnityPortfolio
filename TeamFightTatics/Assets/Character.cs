using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{
    [SerializeField]
    private int _characterNum;
    public int CharacterNum
    {
        get { return _characterNum; }
        set { _characterNum = value; }
    }
    [SerializeField]
    private int _cost;
    public int Cost
    {
        get { return _cost; }
        set { _cost = value; }
    }
    [SerializeField]
    private int _cardIndex;
    public int CardIndex
    {
        get { return _cardIndex; }
        set { _cardIndex = value; }
    }

    public Player Player;

    private RerollManager RerollManager;

    private void Awake()
    {
        Player = FindObjectOfType<Player>();
        RerollManager = FindObjectOfType<RerollManager>();
    }
    public void Summon()
    {
        if (Player.Gold < PoolManager.Instance.CharacterDataList[CharacterNum].Cost) return;
        if (Player.OnSummonFieldCount > 9) return;
        GameObject characterPrefab;
        characterPrefab = PoolManager.Instance.CharacterQueue[CharacterNum].Dequeue();
        Persona character = characterPrefab.GetComponent<Persona>();
        characterPrefab.tag = "PlayerCharacter";
        characterPrefab.AddComponent<DragAndDrop>();

        for (int i = 0; i < TileManager.Instance.SummonTileList.Count; i++)
        {
            if (!TileManager.Instance.SummonTileList[i].GetComponent<SummonField>().IsUsed)
            {
                characterPrefab.transform.position = TileManager.Instance.SummonTileList[i].transform.position;
                character.DiposedIndex = i;
                break;
            }
        }

        characterPrefab.SetActive(true);
        characterPrefab.transform.SetParent(null);
        Player.PlayerCharacterList[character.CharacterNum].Add(characterPrefab);
        Player.Gold -= character.Cost;
        RerollManager.eraseCard(_cardIndex);
        Player.PromoteHelper(CharacterNum);
    }
}
