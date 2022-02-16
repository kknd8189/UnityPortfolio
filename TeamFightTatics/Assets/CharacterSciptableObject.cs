using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObject/CharacterData", order = int.MaxValue)]

public class CharacterSciptableObject : ScriptableObject
{
    public int CharacterNum;

    public int[] SynergyNum;

    public GameObject CharacterPrefab;
    public GameObject CardPrefab;

    //character status
    public int MaxHP, Power, MaxMP, DefaultMP, Cost;
    public float AttackRange, AttackDelay;
}
