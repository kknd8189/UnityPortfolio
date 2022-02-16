using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFiled : Tile
{
    private void OnCollisionEnter(Collision collision)
    {
        Persona persona = collision.collider.GetComponent<Persona>();

        if (persona != null)
        {
            persona.IsOnBattleField = true;
            _isUsed = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.GetComponent<Persona>() != null)
        {
            _isUsed = false;
        }
    }
}
