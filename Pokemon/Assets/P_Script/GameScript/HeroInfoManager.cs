using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using PokemonSpace;
using UnityEngine.SceneManagement;

public class HeroInfoManager : MonoBehaviour {

    private static HeroInfoManager instance = null;

    public static HeroInfoManager Instance
    {
        get
        {
            return instance;
        }
    }

    public string heroName;
    public int money;
    public int totalPokemon;
    public int playTime;
    public string startPlayTime;
    public string mapName;
    public int mapTileLocation;

    Dictionary<string, Dictionary<int, int>> dicNpcCheck = new Dictionary<string, Dictionary<int, int>>();

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            LoadHero();
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void LoadHero()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Application.streamingAssetsPath + "/Hero/SaveFile.xml");

        XmlNode heroInfoNode = xmlDoc.SelectSingleNode("SaveFile/Hero");

        heroName = heroInfoNode.SelectSingleNode("Name").InnerText;
        money = int.Parse(heroInfoNode.SelectSingleNode("Money").InnerText);
        totalPokemon = int.Parse(heroInfoNode.SelectSingleNode("TotalPokemon").InnerText);
        playTime = int.Parse(heroInfoNode.SelectSingleNode("PlayTime").InnerText);
        startPlayTime = heroInfoNode.SelectSingleNode("StartPlayTime").InnerText;
        mapName = heroInfoNode.SelectSingleNode("Map").InnerText;
        mapTileLocation = int.Parse(heroInfoNode.SelectSingleNode("Location").InnerText);

        Debug.Log("주인공 정보 로드 success");

    }

    public void NpcCheck()
    {
        if(!dicNpcCheck.ContainsKey(mapName))
        {
            Dictionary<int, int> valueNpcCheck = new Dictionary<int, int>();

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(Application.streamingAssetsPath + "/NpcCheck/" + mapName + ".xml");

            XmlNodeList npcCheckList = xmlDoc.SelectNodes("NpcCheck/Npc");

            foreach(XmlNode npcCheck in npcCheckList)
            {
                int npcTileNumber = int.Parse(npcCheck.SelectSingleNode("npcTileNumber").InnerText);
                int npcCheckNumber = int.Parse(npcCheck.SelectSingleNode("npcCheck").InnerText);
                valueNpcCheck.Add(npcTileNumber, npcCheckNumber);
            }

            dicNpcCheck.Add(mapName, valueNpcCheck);
            Debug.Log(mapName + " 맵을 처음 방문!");
        }

    }

    public void SaveHero()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Application.streamingAssetsPath + "/Hero/SaveFile.xml");

        
        XmlNode hero = xmlDoc.SelectSingleNode("SaveFile/Hero");

        hero.SelectSingleNode("Money").InnerText = this.money.ToString();

        hero.SelectSingleNode("TotalPokemon").InnerText = this.totalPokemon.ToString();

        hero.SelectSingleNode("PlayTime").InnerText = this.playTime.ToString();
        
        hero.SelectSingleNode("StartPlayTime").InnerText = this.startPlayTime;

        hero.SelectSingleNode("Map").InnerText = this.mapName;

        hero.SelectSingleNode("Location").InnerText = this.mapTileLocation.ToString();

        xmlDoc.Save(Application.streamingAssetsPath + "/Hero/SaveFile.xml");

        SaveNpcCheck(); // npc으로부터 한번 아이템을 받거나 배틀 승리할경우 체크관련 데이터 저장

        Debug.Log("Game Save Succes!!");
        GameAudioManager.Instance.SaveAudio();
    }

    void SaveNpcCheck()
    {
        //첫번째 키는 맵이름, 두번째 키는 npc가 처음 위치한 tileNumber
        foreach(KeyValuePair<string, Dictionary<int, int>> mapPair in dicNpcCheck) // 키 2개 딕셔너리
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Application.streamingAssetsPath + "/NpcCheck/" + mapPair.Key + ".xml"); 
            //방문한 맵 마다 아이템을 받거나 배틀에서 이겼을 경우 해당 맵이름의 키값인 딕셔너리에 해당 npc의 checkNumber값 저장

            XmlNodeList npcNodeList = xmlDoc.SelectNodes("NpcCheck/Npc");
            foreach (XmlNode npcNode in npcNodeList)
            {
                int tileNumber = int.Parse(npcNode.SelectSingleNode("npcTileNumber").InnerText);
                if (mapPair.Value.ContainsKey(tileNumber))
                {
                    npcNode.SelectSingleNode("npcCheck").InnerText = mapPair.Value[tileNumber].ToString();
                }
            }
            xmlDoc.Save(Application.streamingAssetsPath + "/NpcCheck/" + mapPair.Key + ".xml");

        }

    }

    public bool IsNpcPastDialog(int tileNumber)
    {
        if(dicNpcCheck[mapName].ContainsKey(tileNumber))
        {
            if(dicNpcCheck[mapName][tileNumber] != 0)
            {
                return true;
            }
            else
            {
                dicNpcCheck[mapName][tileNumber] = 1;
                return false;
            }
        }
        else
        {
            return false;

        }
    }
}
