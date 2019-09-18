using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEditor;
using PokemonSpace;

public class GameMapDataManager : MonoBehaviour {

    private static GameMapDataManager instance = null;

    public Dictionary<int, MapData> dicMapData;
    public Dictionary<int, BushData> dicBushData;
    public Dictionary<int, string> dicPortal;
    public Dictionary<int, NpcData> dicNpcData;

    public int width, height;

    public bool isMapLoad =false;

    public static GameMapDataManager Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            dicMapData = new Dictionary<int, MapData>();
            dicBushData = new Dictionary<int, BushData>();
            dicPortal = new Dictionary<int, string>();
            dicNpcData = new Dictionary<int, NpcData>();
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        LoadMapData(HeroInfoManager.Instance.mapName);
        isMapLoad = true;
        GameMap.Instance.LoadMap(width, height);
    }

    public void LoadMapData(string mapFileName)
    {
        if (mapFileName.Length != 0)  // 파일 선택
        {
            if(dicMapData.Count > 0)
            {
                dicMapData.Clear();
            }
            if(dicPortal.Count>0)
            {
                dicPortal.Clear();
            }

            TextAsset textAsset = (TextAsset)Resources.Load("Map/" + mapFileName);

            //해당 맵 파일 데이터 로드
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(textAsset.text);

                     XmlNode mapSizeNode = xmlDoc.SelectSingleNode("MapInfo/MapSize");

                     width = int.Parse(mapSizeNode.SelectSingleNode("Width").InnerText);
                     height = int.Parse(mapSizeNode.SelectSingleNode("Height").InnerText);

                     XmlNodeList tiles = xmlDoc.SelectNodes("MapInfo/MapTile");

            foreach (XmlNode tile in tiles)
            {
                MapData mapData = new MapData();

                mapData.tileNumber = int.Parse(tile.SelectSingleNode("tileNumber").InnerText);
                mapData.tileRotate = int.Parse(tile.SelectSingleNode("tileRotate").InnerText);
                mapData.tileCode = tile.SelectSingleNode("tileCode").InnerText;

                if (tile.SelectSingleNode("buildCode").InnerText.Length > 0)
                {
                    mapData.buildCode = tile.SelectSingleNode("buildCode").InnerText;
                }
                if (tile.SelectSingleNode("npcCode").InnerText.Length > 0)
                {
                    mapData.npcCode = tile.SelectSingleNode("npcCode").InnerText;
                }

                dicMapData.Add(mapData.tileNumber, mapData);
            }

            XmlNodeList portals = xmlDoc.SelectNodes("MapInfo/MapPortal");

            foreach(XmlNode portal in portals)
            {
                int tileNumber = int.Parse(portal.SelectSingleNode("tileNumber").InnerText);
                string mapName = portal.SelectSingleNode("portalName").InnerText;
               // mapName = mapName.Substring(FilePath.FileBasicPath.Length, mapName.Length - FilePath.FileBasicPath.Length);  // 파일 공통 경로는 빼고 파일 이름만 저장
                dicPortal.Add(tileNumber, mapName);

            }

            MapBushData(mapFileName);
            MapNpcData(mapFileName);

            MapCameraMove.Instance.character = null;
        }
    

    }

    void MapBushData(string mapFileName)
    {
        if(dicBushData.Count>0)
        {
            dicBushData.Clear();
        }

        TextAsset textAsset = (TextAsset)Resources.Load("MapBush/" + mapFileName);
        //해당 맵 풀숲 파일 데이터 로드
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNodeList bushList = xmlDoc.SelectNodes("BushInfo/Bush");

        foreach (XmlNode bush in bushList)
        {
            BushData bushData = new BushData(
                                            int.Parse(bush.SelectSingleNode("tileNumber").InnerText),
                                            bush.SelectSingleNode("bushCode").InnerText,
                                            int.Parse(bush.SelectSingleNode("bushRotate").InnerText)
                                            );
            
            dicBushData.Add(bushData.bushTileNumber, bushData);
        }
    }

    void MapNpcData(string mapFileName)
    {
        if(dicNpcData.Count>0)
        {
            dicNpcData.Clear();
        }

        TextAsset textAsset = (TextAsset)Resources.Load("MapNpc/" + mapFileName + "Npc");
        //해당 맵 파일 데이터 로드
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNodeList npcList = xmlDoc.SelectNodes("NpcInfo/Npc");

        foreach(XmlNode npc in npcList)
        {
            NpcData npcData = new NpcData();
            npcData.npcTileNumber = int.Parse(npc.SelectSingleNode("npcTileNumber").InnerText);
            npcData.itemNumber = npc.SelectSingleNode("itemNumber").InnerText;
            npcData.itemDialog = npc.SelectSingleNode("itemDialog").InnerText;
            npcData.dialog = npc.SelectSingleNode("dialog").InnerText;

            if (npc.SelectSingleNode("isMoveOn").InnerText.Equals("False"))
            {
                npcData.isMoveOn = false;
            }
            else
            {
                npcData.isMoveOn = true;
            }

            if (npc.SelectSingleNode("isFightOn").InnerText.Equals("False"))
            {
                npcData.isFightOn = false;
            }
            else
            {
                npcData.isFightOn = true;
            }

            dicNpcData.Add(npcData.npcTileNumber, npcData);
        }


    }

}
