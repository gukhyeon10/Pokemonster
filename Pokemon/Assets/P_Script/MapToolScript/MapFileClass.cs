using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using PokemonSpace;
using System.Xml;

public class MapFileClass : MonoBehaviour {

    private static MapFileClass instance = null;

    [SerializeField]
    GameObject MapGrid;
    [SerializeField]
    GameObject ObjectTable;
    [SerializeField]
    GameObject NpcMaker;

    [SerializeField]
    UIInput inputWidth;
    [SerializeField]
    UIInput inputHeight;

    [SerializeField]
    UILabel label_MapName;


    public string filePath = "";
    string saveFilePath = "";

    public bool isMapLoad = false;

    public static MapFileClass Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
    }

    void OnDestroy()
    {
        instance = null;    
    }

    // 이곳에 타일 그리드랑 오브젝트 테이블 자식들을 순차적으로 받아와서 tileNumber랑 해당 spritName에 따라 오브젝트넘버를 저장하는 부분
    void SaveMapData(string filePath)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        XmlNode root = xmlDoc.CreateNode(XmlNodeType.Element, "MapInfo", string.Empty);
        xmlDoc.AppendChild(root);

        XmlNode mapSizeNode = xmlDoc.CreateNode(XmlNodeType.Element, "MapSize", string.Empty);
        root.AppendChild(mapSizeNode);

        XmlElement width = xmlDoc.CreateElement("Width");
        width.InnerText = inputWidth.value;
        int gridMapWidth = int.Parse(inputWidth.value);
        mapSizeNode.AppendChild(width);
        XmlElement height = xmlDoc.CreateElement("Height");
        height.InnerText = inputHeight.value;
        int gridMapHeight = int.Parse(inputHeight.value);
        mapSizeNode.AppendChild(height);

        Dictionary<int, GameObject> dicTile = MapGrid.GetComponent<MapGrid>().dicTile;
        Dictionary<int, GameObject> dicBuild = ObjectTable.GetComponent<ObjectTable>().dicBuild;
        Dictionary<int, GameObject> dicNpc = ObjectTable.GetComponent<ObjectTable>().dicNpc;
        Dictionary<int, string> dicPortal = ObjectTable.GetComponent<ObjectTable>().dicPortal;
        
         for(int i=0; i<gridMapWidth * gridMapHeight; i++)
         {
            XmlNode mapNode = xmlDoc.CreateNode(XmlNodeType.Element, "MapTile", string.Empty);
            root.AppendChild(mapNode);

            XmlElement tileNumber = xmlDoc.CreateElement("tileNumber");
            tileNumber.InnerText = i.ToString();
            mapNode.AppendChild(tileNumber);

            XmlElement tileCode = xmlDoc.CreateElement("tileCode");
            tileCode.InnerText = dicTile[i].GetComponent<UISprite>().spriteName;
            mapNode.AppendChild(tileCode);

            XmlElement tileRotate = xmlDoc.CreateElement("tileRotate");
            tileRotate.InnerText = dicTile[i].GetComponent<TileScript>().tileAngle.ToString();
            mapNode.AppendChild(tileRotate);

            XmlElement buildCode = xmlDoc.CreateElement("buildCode");
            if(dicBuild.ContainsKey(i))
            {
                buildCode.InnerText = dicBuild[i].GetComponentInChildren<UISprite>().spriteName;
            }
            else
            {
                buildCode.InnerText = null;
            }
            mapNode.AppendChild(buildCode);

            XmlElement npcCode = xmlDoc.CreateElement("npcCode");
            if(dicNpc.ContainsKey(i))
            {
                npcCode.InnerText = dicNpc[i].GetComponentInChildren<UISprite>().spriteName;
            }
            else
            {
                npcCode.InnerText = null;
            }
            mapNode.AppendChild(npcCode);
         }

         foreach(KeyValuePair<int, string> pair in dicPortal)
        {
            XmlNode mapNode = xmlDoc.CreateNode(XmlNodeType.Element, "MapPortal", string.Empty);
            root.AppendChild(mapNode);
            XmlElement tileNumber = xmlDoc.CreateElement("tileNumber");
            tileNumber.InnerText = pair.Key.ToString();
            mapNode.AppendChild(tileNumber);

            XmlElement portalName = xmlDoc.CreateElement("portalName");
            portalName.InnerText = pair.Value;
            mapNode.AppendChild(portalName);
        }
         
        xmlDoc.Save(filePath);

        Debug.Log("MAP Save End!!");
    }

    void SaveMapBushData(string filePath)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        XmlNode root = xmlDoc.CreateNode(XmlNodeType.Element, "BushInfo", string.Empty);
        xmlDoc.AppendChild(root);

        Dictionary<int, GameObject> dicBush = BushMapTool.Instance.dicBush;

        foreach (KeyValuePair<int, GameObject> pair in dicBush)
        {
            XmlNode bushNode = xmlDoc.CreateNode(XmlNodeType.Element, "Bush", string.Empty);
            root.AppendChild(bushNode);
            XmlElement tileNumber = xmlDoc.CreateElement("tileNumber");
            tileNumber.InnerText = pair.Key.ToString();
            bushNode.AppendChild(tileNumber);

            XmlElement bushName = xmlDoc.CreateElement("bushCode");
            bushName.InnerText = pair.Value.GetComponent<BushScript>().m_Bush.spriteName;
            bushNode.AppendChild(bushName);

            XmlElement bushRotate = xmlDoc.CreateElement("bushRotate");
            bushRotate.InnerText = pair.Value.GetComponent<BushScript>().bushAngle.ToString();
            bushNode.AppendChild(bushRotate);
        }

        xmlDoc.Save(filePath);

        Debug.Log("MAP Bush Save End!!");
    }

    void SaveNpcData(string filePath)
    {
        filePath = filePath.Insert(filePath.Length - 4, "Npc");

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        XmlNode root = xmlDoc.CreateNode(XmlNodeType.Element, "NpcInfo", string.Empty);
        xmlDoc.AppendChild(root);

        Dictionary<int, NpcData> dicNpcData = NpcMaker.GetComponent<NpcMaker>().dicNpcData;

        foreach (KeyValuePair<int, NpcData> pair in dicNpcData)
        {
            XmlNode npcNode = xmlDoc.CreateNode(XmlNodeType.Element, "Npc", string.Empty);
            root.AppendChild(npcNode);

            XmlElement tileNumber = xmlDoc.CreateElement("npcTileNumber");
            tileNumber.InnerText = pair.Key.ToString();
            npcNode.AppendChild(tileNumber);

            XmlElement itemNumber = xmlDoc.CreateElement("itemNumber");
            itemNumber.InnerText = pair.Value.itemNumber;
            npcNode.AppendChild(itemNumber);

            XmlElement itemDialog = xmlDoc.CreateElement("itemDialog");
            itemDialog.InnerText = pair.Value.itemDialog;
            npcNode.AppendChild(itemDialog);

            XmlElement dialog = xmlDoc.CreateElement("dialog");
            dialog.InnerText = pair.Value.dialog;
            npcNode.AppendChild(dialog);

            XmlElement isMoveOn = xmlDoc.CreateElement("isMoveOn");
            isMoveOn.InnerText = pair.Value.isMoveOn.ToString();
            npcNode.AppendChild(isMoveOn);

            XmlElement isFightOn = xmlDoc.CreateElement("isFightOn");
            isFightOn.InnerText = pair.Value.isFightOn.ToString();
            npcNode.AppendChild(isFightOn);

        }
        xmlDoc.Save(filePath);

        Debug.Log("NPC Save End!");
    }

    void SaveNpcCheckData(string filePath)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        XmlNode root = xmlDoc.CreateNode(XmlNodeType.Element, "NpcCheck", string.Empty);
        xmlDoc.AppendChild(root);

        Dictionary<int, NpcData> dicNpcData = NpcMaker.GetComponent<NpcMaker>().dicNpcData;

        foreach (KeyValuePair<int, NpcData> pair in dicNpcData)
        {
            XmlNode npcNode = xmlDoc.CreateNode(XmlNodeType.Element, "Npc", string.Empty);
            root.AppendChild(npcNode);

            XmlElement tileNumber = xmlDoc.CreateElement("npcTileNumber");
            tileNumber.InnerText = pair.Key.ToString();
            npcNode.AppendChild(tileNumber);

            XmlElement npcCheck = xmlDoc.CreateElement("npcCheck");
            npcCheck.InnerText = "0";
            npcNode.AppendChild(npcCheck);

        }
        xmlDoc.Save(filePath);

        Debug.Log("NpcCheck Save End!");
    }

    void LoadMapData(string filePath)
    {
        isMapLoad = true;

        ObjectTable.GetComponent<ObjectTable>().InitObjectDictionary();
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(filePath);

        XmlNode mapSizeNode = xmlDoc.SelectSingleNode("MapInfo/MapSize");

        inputWidth.value = mapSizeNode.SelectSingleNode("Width").InnerText;
        inputHeight.value = mapSizeNode.SelectSingleNode("Height").InnerText;

        MapGrid.GetComponent<MapGrid>().SetGridSize();

        XmlNodeList tiles = xmlDoc.SelectNodes("MapInfo/MapTile");

        foreach(XmlNode tile in tiles)
        {
            int tileNumber = int.Parse(tile.SelectSingleNode("tileNumber").InnerText);
            int tileAngle = int.Parse(tile.SelectSingleNode("tileRotate").InnerText);

            MapGrid.GetComponent<MapGrid>().LoadMapGrid(tileNumber, tile.SelectSingleNode("tileCode").InnerText, tileAngle);

            if(tile.SelectSingleNode("buildCode").InnerText.Length > 0)
            {
                ObjectTable.GetComponent<ObjectTable>().BuildAttach(tile.SelectSingleNode("buildCode").InnerText, tileNumber);

            }
            if(tile.SelectSingleNode("npcCode").InnerText.Length > 0)
            {
                ObjectTable.GetComponent<ObjectTable>().NpcAttach(tile.SelectSingleNode("npcCode").InnerText, tileNumber);
            }
        }

        XmlNodeList portalList = xmlDoc.SelectNodes("MapInfo/MapPortal");

        foreach(XmlNode portal in portalList)
        {
            int tileNumber = int.Parse(portal.SelectSingleNode("tileNumber").InnerText);
            string mapName = portal.SelectSingleNode("portalName").InnerText;

            ObjectTable.GetComponent<ObjectTable>().PortalAttach(tileNumber, mapName);
        }

        isMapLoad = false;
    }

    void LoadMapBushData(string filePath)
    {
        BushMapTool.Instance.LoadBushData(filePath);
    }

    void LoadNpcData(string filePath)
    {
        filePath = filePath.Insert(filePath.Length - 4, "Npc");
        NpcMaker.GetComponent<NpcMaker>().NpcLoad(filePath);
        
    }



    public void OpenFileDialogShow()
    {

   
    #if UNITY_EDITOR
            filePath = EditorUtility.OpenFilePanel("Open Map File Dialog"
                                                , Application.dataPath
                                                , "xml");
    #endif
            if (filePath.Length != 0)  // 파일 선택
            {
            LoadMapData(filePath);
            LoadNpcData(FilePath.MapNpcFolderPath + filePath.Substring(FilePath.MapFolderPath.Length));
            LoadMapBushData(PokemonSpace.FilePath.MapBushFolderPath + filePath.Substring(PokemonSpace.FilePath.MapFolderPath.Length));

            label_MapName.text = filePath.Substring(FilePath.MapFolderPath.Length);
            }
        
    }

    public void SaveFileDialogShow()
    {
#if UNITY_EDITOR
         saveFilePath = EditorUtility.SaveFilePanel("Save Map File Dialog"
                                    , Application.dataPath
                                    , "map"
                                    , "xml");
#endif
        if(saveFilePath.Length != 0)
        {
            SaveMapData(saveFilePath);
            SaveMapBushData(PokemonSpace.FilePath.MapBushFolderPath + saveFilePath.Substring(PokemonSpace.FilePath.MapFolderPath.Length));
            SaveNpcData(PokemonSpace.FilePath.MapNpcFolderPath + saveFilePath.Substring(PokemonSpace.FilePath.MapFolderPath.Length));
            SaveNpcCheckData(Application.streamingAssetsPath + "/NpcCheck/" + saveFilePath.Substring(PokemonSpace.FilePath.MapFolderPath.Length));
        }
    }

}
