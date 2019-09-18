using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGrid : MonoBehaviour {

    private static MapGrid instance = null;

    [SerializeField]
    UIGrid gridMap;

    [SerializeField]
    GameObject gbTile;

    [SerializeField]
    UIInput inputWidth;
    [SerializeField]
    UIInput inputHeight;
    
    public Dictionary<int, GameObject> dicTile;
    public Dictionary<int, int> dicMovable;


    int gridWidth, gridHeight;

    public static MapGrid Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
        dicTile = new Dictionary<int, GameObject>();
        dicMovable = new Dictionary<int, int>();
    }

    void OnDestroy()
    {
        instance = null;     
    }



    public void SetGridSize()
    {
        gridWidth = int.Parse(inputWidth.value);
        gridHeight = int.Parse(inputHeight.value);

        if(gridWidth * gridHeight == 0)
        {
            return;
        }

        ToolCursor.Instance.SetMapSize(gridWidth, gridHeight);

        dicMovable.Clear();
        dicTile.Clear();
        if(gridMap.transform.childCount!=0)
        {
           gridMap.transform.DestroyChildren();
        }
        gridMap.Reposition();

        gridMap.maxPerLine = gridWidth;

        for (int i=0; i < gridWidth * gridHeight; i++)
        {
            GameObject blankTile = NGUITools.AddChild(gridMap.gameObject, gbTile);
            TileScript blankTileScript = blankTile.GetComponent<TileScript>();
            blankTileScript.SetBasicTile("Grid_Tile");
        }
        gridMap.Reposition();

        SetDictionary();
    }

    void SetDictionary()
    {
        for(int i=0; i< gridMap.transform.childCount; i++)
        {
            gridMap.transform.GetChild(i).GetComponent<TileScript>().SetNumber(i);
            dicTile.Add(i, gridMap.transform.GetChild(i).gameObject);
            dicMovable.Add(i, 0);
        }
    }

    //타일 로드
    public void LoadMapGrid(int tileNumber, string tileCode, int tileRotate)
    {
        dicTile[tileNumber].GetComponent<UISprite>().spriteName = tileCode;
        dicTile[tileNumber].GetComponent<TileScript>().tileAngle = tileRotate;
        dicTile[tileNumber].transform.localEulerAngles = new Vector3(0, 0, tileRotate);
    }

    public int GetMapWidth
    {
        get
        {
            return this.gridWidth;
        }
    }

    public int GetMapHeight
    {
        get
        {
            return this.gridHeight;
        }
    }

}
