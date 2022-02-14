using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFiled : Tile
{
    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Character character = collision.collider.GetComponent<Character>();

        if (character != null)
        {
            character.isOnBattleField = true;
            player.Capacity--;
            _isUsed = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.GetComponent<Character>() != null)
        {
            player.Capacity++;
            _isUsed = false;
        }
    }
}
