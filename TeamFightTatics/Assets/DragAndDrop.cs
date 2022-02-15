using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 screenSpace;
    private Vector3 offset;
    private Vector3 initialPosition;
    private Player playerScript;

    private void Awake()
    {
        playerScript = FindObjectOfType<Player>();
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

        if (Physics.Raycast(transform.position, Vector3.down, out dectectedTile, 20))
        {
            Tile tile = dectectedTile.transform.GetComponent<Tile>();

            if (dectectedTile.collider.GetComponent<BattleFiled>() != null)
            {
                GetComponent<Character>().DiposedIndex = dectectedTile.collider.GetComponent<BattleFiled>().Index;
                transform.position = dectectedTile.transform.position;

                if (playerScript.Capacity <= 0 || GameManager.Instance.GameState == GAMESTATE.Battle || tile.Index >= 48) transform.position = initialPosition;
            }

            else if(dectectedTile.collider.GetComponent<SummonField>() != null )
            {
                if (!tile.IsUsed) transform.position = dectectedTile.transform.position;
                else if (tile.IsUsed) transform.position = initialPosition;
            }

        }

        else transform.position = initialPosition;

    }
}
