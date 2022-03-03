using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 screenSpace;
    private Vector3 offset;
    private Vector3 initialPosition;
    public GameObject Player;
    public Player player;
    public Synergy synergy;
    private AutoBattle _autoBattle;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        player = Player.GetComponent<Player>();
        synergy = Player.GetComponent<Synergy>();
        _autoBattle = GetComponent<AutoBattle>();
    }
    private void OnMouseDown()
    {
        if (GameManager.Instance.GameState == GAMESTATE.Battle && gameObject.GetComponent<Persona>().IsOnBattleField)
            return;

        _autoBattle.Agent.enabled = false;

        initialPosition = transform.position;
        screenSpace = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
    }
    private void OnMouseDrag()
    {
        if (GameManager.Instance.GameState == GAMESTATE.StandBy && GameManager.Instance.IsOver)
        {
            transform.position = initialPosition;
            _autoBattle.Agent.enabled = true;
            return;
        }
        else if (GameManager.Instance.GameState == GAMESTATE.Battle)
                        return;
        Vector3 curScreenSpace = new(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;

        curPosition.y = 5;

        transform.position = curPosition;
    }
    private void OnMouseUp()
    {

        if (GameManager.Instance.GameState == GAMESTATE.Battle && gameObject.GetComponent<Persona>().IsOnBattleField)
            return;

        RaycastHit dectectedTile;

        //비트 연산자 사용 Tile 레이어
        int mask = (1 << 6);

        if (Physics.Raycast(transform.position, Vector3.down, out dectectedTile, 20, mask))
        {
            Tile tile = dectectedTile.transform.GetComponent<Tile>();
            Persona persona = GetComponent<Persona>();

            //필드위에 다른 캐릭터가 있으면 원래 위치로
            if (tile.IsUsed)
            {
                transform.position = initialPosition;
                return;
            }

            //BattleField위에 올릴때
            else if (dectectedTile.collider.GetComponent<BattleFiled>() != null)
            {
                if (GameManager.Instance.GameState == GAMESTATE.Battle || tile.Index >= 48)
                {
                    transform.position = initialPosition;
                    return;
                }

                //SummonField에서 BattleField위로 올릴때
                if (!persona.IsOnBattleField)
                {
                    if (player.Capacity <= 0)
                    {
                        transform.position = initialPosition;
                        return;
                    }

                    synergy.IncreaseSynergyCount(persona.CharacterNum);

                    player.Capacity--;
                    player.LiveCharacterCount++;
                    player.OnBattleCharacterList.Add(gameObject);
                }


                persona.DiposedIndex = dectectedTile.collider.GetComponent<BattleFiled>().Index;
                transform.position = dectectedTile.transform.position;
                _autoBattle.Agent.enabled = true;
                return;
            }

            //SummonField위에 올릴때
            else if (dectectedTile.collider.GetComponent<SummonField>() != null)
            {
                transform.position = dectectedTile.transform.position;
                persona.DiposedIndex = dectectedTile.collider.GetComponent<SummonField>().Index;
                //BattleField에서 SummonField위로 올릴때
                if (persona.IsOnBattleField)
                {
                    synergy.DecreaseSynergyCount(persona.CharacterNum);
                    player.Capacity++;
                    player.LiveCharacterCount--;
                    player.OnBattleCharacterList.Remove(gameObject);
                    return;
                }
            }
        }
        //타일위에 올리지 않을때
        else transform.position = initialPosition;

    }
}
