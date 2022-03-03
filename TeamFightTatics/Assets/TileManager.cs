using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public Transform Field;
    public GameObject summonTileObject;
    public GameObject battleTileObject;

    public Material oddTileMaterial;
    public Material evenTileMaterial;
    public Material enemyTileMaterial;
    public Material playerTileMaterial;

    private int summonTileMax = 10;
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
        for (int i = 0; i < summonTileMax; i++)
        {
            SummonTileList[i].GetComponent<SummonField>().Index = i;
            if (i % 2 == 0) SummonTileList[i].GetComponent<MeshRenderer>().material = evenTileMaterial;
            else if (i % 2 == 1) SummonTileList[i].GetComponent<MeshRenderer>().material = oddTileMaterial;
        }
    }
    private void setBattleTile()
    {

        for (int i = 0; i < horizonalTileMax; i++)
        {
            for (int j = 0; j < verticalTileMax; j++)
            {
                if (j + i * verticalTileMax >= 48)
                {
                    BattleTileList[j + i * verticalTileMax].GetComponent<MeshRenderer>().material = enemyTileMaterial;
                }

                else BattleTileList[j + i * verticalTileMax].GetComponent<MeshRenderer>().material = playerTileMaterial;

                BattleTileList[j + i * verticalTileMax].GetComponent<BattleFiled>().Index = (j + i * verticalTileMax);
            }
        }

    }
}

