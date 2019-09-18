using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PokemonSpace;
using System.Xml;
public class NpcMaker : MonoBehaviour {

    [SerializeField]
    UIPanel npcMakerPanel;

    [SerializeField]
    UILabel npcTileNumber;
    [SerializeField]
    UIInput itemNumber;
    [SerializeField]
    UIInput itemDialog;
    [SerializeField]
    UIInput dialog;
    [SerializeField]
    UIToggle isMoveOn;
    [SerializeField]
    UIToggle isFightOn;

    public Dictionary<int, NpcData> dicNpcData = new Dictionary<int, NpcData>();
	
    public void NpcMakerActive(int tileNumber)
    {
        npcMakerPanel.gameObject.SetActive(true);
        npcTileNumber.text = tileNumber.ToString();
    }

    public void NpcMake()
    {
        NpcData npc = new NpcData();
        npc.npcTileNumber = int.Parse(npcTileNumber.text);
        npc.itemNumber = itemNumber.value;
        npc.itemDialog = itemDialog.value;
        npc.dialog = dialog.value;
        npc.isMoveOn =  !isMoveOn.value;
        npc.isFightOn = !isFightOn.value;

        dicNpcData.Add(npc.npcTileNumber, npc);

        npcMakerPanel.gameObject.SetActive(false);
    }

    public void NpcDelete(int npcTileNumber)
    {
        dicNpcData.Remove(npcTileNumber);
    }

    public void NpcLoad(string npcFilePath)
    {
        if(dicNpcData.Count > 0)
        {
            dicNpcData.Clear();
        }

         XmlDocument xmlDoc = new XmlDocument();
         xmlDoc.Load(npcFilePath);

        XmlNodeList npcList = xmlDoc.SelectNodes("NpcInfo/Npc"); 

        foreach(XmlNode npc in npcList)
        {
            NpcData npcData = new NpcData();
            npcData.npcTileNumber = int.Parse(npc.SelectSingleNode("npcTileNumber").InnerText);
            npcData.itemNumber = npc.SelectSingleNode("itemNumber").InnerText;
            npcData.itemDialog = npc.SelectSingleNode("itemDialog").InnerText;
            npcData.dialog = npc.SelectSingleNode("dialog").InnerText;

            if(npc.SelectSingleNode("isMoveOn").InnerText.Equals("False"))
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
