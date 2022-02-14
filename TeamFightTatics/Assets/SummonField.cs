using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonField : Tile
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Character>() != null)
        {
            collision.collider.GetComponent<Character>().isOnBattleField = false;
            PoolManager.Instance.SummonCount++;
            _isUsed = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.GetComponent<Character>() != null)
        {
            PoolManager.Instance.SummonCount--;
            _isUsed = false;
        }
    }
}
