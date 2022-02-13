using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 screenSpace;
    private Vector3 offset;
    private Vector3 initialPosition;
    private GameObject Player;
    private Player playerScript;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerScript = Player.GetComponent<Player>();
    }
    private void OnMouseDown()
    {
       
            initialPosition = transform.position;
            screenSpace = Camera.main.WorldToScreenPoint(transform.position);
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
        
    }
    private void OnMouseDrag()
    {
        if (GameManager.Instance.GameState == GAMESTATE.Battle && gameObject.GetComponent<Character>().isOnBattleField)
        {
            return;
        }

        Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;

        curPosition.y = 5;

        transform.position = curPosition;
    }

    private void OnMouseUp()
    {
        RaycastHit dectectedTile;
        Vector3 finalPosition;

        finalPosition = initialPosition;

        if (Physics.Raycast(transform.position, Vector3.down, out dectectedTile, 20))
        {
            Tile tile = dectectedTile.transform.GetComponent<Tile>();
            if (playerScript.Capacity <= 0 && dectectedTile.collider.GetComponent<BattleFiled>() != null) finalPosition = initialPosition;
            else if(GameManager.Instance.GameState == GAMESTATE.Battle && dectectedTile.collider.GetComponent<BattleFiled>() != null) finalPosition = initialPosition;
            else if (tile.Index < 48 && !tile.IsUsed) finalPosition = dectectedTile.transform.position;
        }
       
        transform.position = finalPosition;
    }
}
