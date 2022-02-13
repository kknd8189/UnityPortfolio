using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFiled : Tile
{
    private GameObject Player;
    private Player playerScript;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerScript = Player.GetComponent<Player>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Character>() != null)
        {
            collision.collider.GetComponent<Character>().isOnBattleField = true;
            playerScript.Capacity--;
            _isUsed = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.GetComponent<Character>() != null)
        {
            collision.collider.GetComponent<Character>().isOnBattleField = false;
            playerScript.Capacity++;
            _isUsed = false;
        }
    }
}
