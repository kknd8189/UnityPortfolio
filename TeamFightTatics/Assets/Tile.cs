using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject TileObject;

    private static int SummonTileMax = 10; 
    public List<GameObject> SummonTileList = new List<GameObject>();
    public List<GameObject> BattleTileList = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < SummonTileMax; i++)
        {
            GameObject gameObject = TileObject;
            Instantiate(TileObject);
            SummonTileList.Add(gameObject);
            SummonTileList[i].transform.position = new Vector3(i * 10, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
