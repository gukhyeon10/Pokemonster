using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class BushMapTool : MonoBehaviour {

    private static BushMapTool instance = null;

    [SerializeField]
    GameObject BushPanel;
    [SerializeField]
    GameObject gbBush;

    public static BushMapTool Instance
    {
        get
        {
            return instance;
        }
    }

    public Dictionary<int, GameObject> dicBush;

    void Awake()
    {
        instance = this;
        dicBush = new Dictionary<int, GameObject>();
    }

    void OnDestroy()
    {
        instance = null;
    }

    public void LoadBushData(string filePath)
    {
        if(dicBush.Count>0)
        {
            dicBush.Clear();
        }
        if(BushPanel.transform.childCount >0)
        {
            BushPanel.transform.DestroyChildren();
        }

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(filePath);

        XmlNodeList bushList = xmlDoc.SelectNodes("BushInfo/Bush");
        
        foreach(XmlNode bush in bushList)
        {
            int tileNumber = int.Parse(bush.SelectSingleNode("tileNumber").InnerText);

            GameObject bushObject = NGUITools.AddChild(BushPanel.gameObject, gbBush);
            bushObject.transform.localPosition = MapGrid.Instance.dicTile[tileNumber].transform.localPosition;

            BushScript bushScript = bushObject.GetComponent<BushScript>();
            bushScript.tileNumber = tileNumber;
            bushScript.m_Bush.spriteName = bush.SelectSingleNode("bushCode").InnerText;
            bushScript.m_Bush.depth = (tileNumber / MapGrid.Instance.GetMapWidth) + 1;
            bushScript.SetBushObject();
            bushScript.bushAngle = int.Parse(bush.SelectSingleNode("bushRotate").InnerText);
            bushScript.m_Bush.transform.localEulerAngles = new Vector3(0, 0, bushScript.bushAngle);
            dicBush.Add(tileNumber, bushObject);
        }
    }

    public void BushAttach(string bushName, int tileNumber)
    {
        if(!dicBush.ContainsKey(tileNumber))
        {
            GameObject bushObject = NGUITools.AddChild(BushPanel.gameObject, gbBush);
            bushObject.transform.localPosition = MapGrid.Instance.dicTile[tileNumber].transform.localPosition;

            BushScript bushScript = bushObject.GetComponent<BushScript>();
            bushScript.tileNumber = tileNumber;
            bushScript.m_Bush.spriteName = bushName;
            bushScript.m_Bush.depth = (tileNumber / MapGrid.Instance.GetMapWidth) + 1;
            bushScript.SetBushObject();
            dicBush.Add(tileNumber, bushObject);
        }
       
    }

    public void BushDelete(int tileNumber)
    {
        if(dicBush.ContainsKey(tileNumber))
        {
            dicBush.Remove(tileNumber);
        }
    }


}
