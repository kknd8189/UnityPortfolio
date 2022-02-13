using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTile : MonoBehaviour
{
    public Transform field;
    public GameObject summonTileObject;
    public GameObject battleTileObject;

    public Material oddTileMaterial;
    public Material evenTileMaterial;

    private int SummonTileMax = 10;
    private int verticalTileMax = 12;
    private int horizonalTileMax = 8;

    public List<GameObject> SummonTileList = new List<GameObject>();
    public List<GameObject> BattleTileList = new List<GameObject>();

    // Start is called before the first frame update
    void Awake()
    {
        setSummonTile();
        setBattleTile();
    }
    private void setSummonTile()
    {
        for (int i = 0; i < SummonTileMax; i++)
        {
            GameObject gameObject = summonTileObject;
            Instantiate(gameObject, field);
            gameObject.transform.position = new Vector3(10 + i * 10, 0, 0);
            if (i % 2 == 0) gameObject.GetComponent<MeshRenderer>().material = evenTileMaterial;
            else if(i % 2 == 1) gameObject.GetComponent<MeshRenderer>().material = oddTileMaterial;
            gameObject.GetComponent<SummonField>().setTileIndex(i);
            SummonTileList.Add(gameObject);
        }
    }
    private void setBattleTile()
    {
        for(int i = 0; i < verticalTileMax; i++)
        {
            for(int j = 0; j< horizonalTileMax; j++)
            {
                GameObject gameObject = battleTileObject;
                Instantiate(gameObject, field);
                gameObject.GetComponent<BattleFiled>().setTileIndex(i + j * verticalTileMax);
                gameObject.transform.position = new Vector3( i * 10, 0, 15 + j * 10);
                BattleTileList.Add(gameObject);            
            }
        }
    }
}

