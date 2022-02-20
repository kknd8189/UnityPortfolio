using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonField : Tile
{
    private Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Persona>() != null)
        {
            collision.collider.GetComponent<Persona>().IsOnBattleField = false;
            player.OnSummonFieldCount++;
            _isUsed = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.GetComponent<Persona>() != null)
        {
            player.OnSummonFieldCount--;
            _isUsed = false;
        }
    }
}
