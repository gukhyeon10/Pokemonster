using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using PokemonSpace;

public class HeroItemManager : MonoBehaviour {

    private static HeroItemManager instance = null;

    public Dictionary<int, Item> dicItemInfo = new Dictionary<int, Item>();
    public Dictionary<int, int> dicHeroItem = new Dictionary<int, int>();

    public static HeroItemManager Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        LoadItemInfo();
        LoadHeroItem();
	}
	
    void LoadItemInfo()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("Item/itemInfoList");

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNodeList items = xmlDoc.SelectNodes("InfoList/Item");

        foreach (XmlNode item in items)
        {
            Item item_Info = new Item(int.Parse(item.SelectSingleNode("No").InnerText),
                                      int.Parse(item.SelectSingleNode("Type").InnerText),
                                      item.SelectSingleNode("Name").InnerText,
                                      item.SelectSingleNode("Text").InnerText);

            dicItemInfo.Add(item_Info.no, item_Info);


        }

        Debug.Log("아이템 정보 Load Success");
    }
	
    void LoadHeroItem()
    {

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Application.streamingAssetsPath + "/Hero/BringItem.xml");

        XmlNodeList items = xmlDoc.SelectNodes("ItemList/Item");

        foreach (XmlNode item in items)
        {
            int itemNumber = int.Parse(item.SelectSingleNode("No").InnerText);
            int itemCount = int.Parse(item.SelectSingleNode("Count").InnerText);

            dicHeroItem.Add(itemNumber, itemCount);

        }

        Debug.Log("Hero 아이템 Load Success");
    }

}
