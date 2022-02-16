using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 screenSpace;
    private Vector3 offset;
    private Vector3 initialPosition;
    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    private void OnMouseDown()
    {
            initialPosition = transform.position;
            screenSpace = Camera.main.WorldToScreenPoint(transform.position);
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
    }
    private void OnMouseDrag()
    {
        if (GameManager.Instance.GameState == GAMESTATE.Battle && gameObject.GetComponent<Persona>().IsOnBattleField)
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

        //비트 연산자 사용 레이어 6번째
        int mask = (1 << 6);

        if (Physics.Raycast(transform.position, Vector3.down, out dectectedTile, 20, mask))
        {
            Tile tile = dectectedTile.transform.GetComponent<Tile>();

            if (dectectedTile.collider.GetComponent<BattleFiled>() != null)
            {
                GetComponent<Persona>().DiposedIndex = dectectedTile.collider.GetComponent<BattleFiled>().Index;
                transform.position = dectectedTile.transform.position;

                if (player.Capacity <= 0 || GameManager.Instance.GameState == GAMESTATE.Battle || tile.Index >= 48) transform.position = initialPosition;
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
