using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public Transform field;
    public GameObject summonTileObject;
    public GameObject battleTileObject;

    public Material oddTileMaterial;
    public Material evenTileMaterial;
    public Material enemyTileMaterial;
    public Material playerTileMaterial;

    private int SummonTileMax = 10;
    private int verticalTileMax = 12;
    private int horizonalTileMax = 8;

    public List<GameObject> SummonTileList = new List<GameObject>();
    public List<GameObject> BattleTileList = new List<GameObject>();

    public static TileManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else if (Instance != null)
        {
            Destroy(this);
        }

        setSummonTile();
        setBattleTile();
    }
    private void setSummonTile()
    {
        for (int i = 0; i < SummonTileMax; i++)
        {
            GameObject gameObject;
            gameObject = Instantiate(summonTileObject, field);
            gameObject.transform.position = new Vector3(10 + i * 10, 0, 0);
            gameObject.GetComponent<SummonField>().Index = i;
            if (i % 2 == 0) gameObject.GetComponent<MeshRenderer>().material = evenTileMaterial;
            else if (i % 2 == 1) gameObject.GetComponent<MeshRenderer>().material = oddTileMaterial;
            SummonTileList.Add(gameObject);
        }
    }
    private void setBattleTile()
    {
        for (int i = 0; i < horizonalTileMax; i++)
        {
            for (int j = 0; j < verticalTileMax; j++)
            {
                GameObject gameObject;
                gameObject = Instantiate(battleTileObject, field);
                gameObject.GetComponent<BattleFiled>().Index = (j + i * verticalTileMax);
                gameObject.transform.position = new Vector3(j * 10, 0, 15 + i * 10);
                if (j + i * verticalTileMax >= 48) gameObject.GetComponent<MeshRenderer>().material = enemyTileMaterial;
                else gameObject.GetComponent<MeshRenderer>().material = playerTileMaterial;
                BattleTileList.Add(gameObject);
            }
        }
    }
}

