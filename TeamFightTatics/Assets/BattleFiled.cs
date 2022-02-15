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
        Persona persona = collision.collider.GetComponent<Persona>();

        if (persona != null)
        {
            persona.IsOnBattleField = true;
            player.Capacity--;
            _isUsed = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.GetComponent<Persona>() != null)
        {
            player.Capacity++;
            _isUsed = false;
        }
    }
}
