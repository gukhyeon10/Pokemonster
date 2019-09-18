using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PokemonSpace;
public class ObjectTable : MonoBehaviour {

    [SerializeField]
    UITable objectTable;

    [SerializeField]
    GameObject NpcMaker;

    [SerializeField]
    GameObject gbObject;
    [SerializeField]
    GameObject gbSmartObject;
    [SerializeField]
    GameObject gbNpc;
    [SerializeField]
    GameObject gbPortal;

    public Dictionary<int, GameObject> dicBuild;
    public Dictionary<int, GameObject> dicNpc;
    public Dictionary<int, string> dicPortal;

    void Awake()
    {
        dicBuild = new Dictionary<int, GameObject>();
        dicNpc = new Dictionary<int, GameObject>();
        dicPortal = new Dictionary<int, string>();
    }

    public void InitObjectDictionary()
    {
        dicBuild.Clear();
        dicNpc.Clear();
        dicPortal.Clear();

        if(objectTable.transform.childCount > 1)
        {
            for(int i= objectTable.transform.childCount -1; i> 0; i--)
            {
                Transform childObject = objectTable.transform.GetChild(i);
                Destroy(childObject.gameObject);
            }
        }
    }

    public void ObjectAttach(string objectName, int tileNumber)
    {

        switch(ToolCursor.Instance.CursorType)
        {
            case (int)ObjectEnum.TILE:
                {
                    break;
                }
            case (int)ObjectEnum.BUILD:
                {
                    BuildAttach(objectName, tileNumber);
                    break;
                }
            case (int)ObjectEnum.ITEM:
                {
                    break;
                }
            case (int)ObjectEnum.NPC:
                {
                    NpcAttach(objectName, tileNumber);
                    NpcMaker.GetComponent<NpcMaker>().NpcMakerActive(tileNumber);
                    break;
                }
            case (int)ObjectEnum.BUSH:
                {

                    BushMapTool.Instance.BushAttach(objectName, tileNumber);
                    break;
                }
        }
        
    }

    public GameObject CursorSmartObject()
    {
        GameObject smartObject = NGUITools.AddChild(objectTable.gameObject, gbSmartObject);
        smartObject.transform.localPosition = Vector3.zero;
        return smartObject;
    }
    
    public void BuildAttach(string buildName, int tileNumber)
    {
        GameObject buildObject = NGUITools.AddChild(objectTable.gameObject, gbObject);
        buildObject.transform.localPosition = MapGrid.Instance.dicTile[tileNumber].transform.localPosition;

        BuildScript buildScript = buildObject.GetComponent<BuildScript>();
        buildScript.tileNumber = tileNumber;
        buildScript.m_Build.spriteName = buildName;
        buildScript.m_Build.depth = (tileNumber / MapGrid.Instance.GetMapWidth) + 1;
        buildScript.SetBuildObject();
        dicBuild.Add(tileNumber, buildObject);

    } 

    public void NpcAttach(string npcName, int tileNumber)
    {
        GameObject npcObject = NGUITools.AddChild(objectTable.gameObject, gbNpc);
        npcObject.transform.localPosition = MapGrid.Instance.dicTile[tileNumber].transform.localPosition;

        NpcScript npcScript = npcObject.GetComponent<NpcScript>();
        npcScript.tileNumber = tileNumber;
        npcScript.m_Npc.spriteName = npcName;
        npcScript.m_Npc.depth = (tileNumber / MapGrid.Instance.GetMapWidth) + 1;
        npcScript.SetNpcObject();
        dicNpc.Add(tileNumber, npcObject);
    }
    

    

    public void ObjectDelete(GameObject selectedObject)
    {
        string atlasName = selectedObject.GetComponentInChildren<UISprite>().atlas.name ;
        switch(atlasName)
        {
            case "Npc":
                {
                    dicNpc.Remove(selectedObject.GetComponent<NpcScript>().tileNumber);
                    NpcMaker.GetComponent<NpcMaker>().NpcDelete(selectedObject.GetComponent<NpcScript>().tileNumber);
                    Destroy(selectedObject);
                    break;
                }
            case "Build":
                {
                    dicBuild.Remove(selectedObject.GetComponent<BuildScript>().tileNumber);
                    Destroy(selectedObject);
                    break;
                }
            case "Bush":
                {
                    BushMapTool.Instance.BushDelete(selectedObject.GetComponent<BushScript>().tileNumber);
                    Destroy(selectedObject);
                    break;
                }
        }
    }

    public void PortalAttach(int tileNumber, string mapName)
    {
        GameObject portalObject = NGUITools.AddChild(objectTable.gameObject, gbPortal);
        portalObject.transform.localPosition = MapGrid.Instance.dicTile[tileNumber].transform.localPosition;

        portalObject.GetComponentInChildren<UISprite>().depth = 125;
        portalObject.GetComponentInChildren<UILabel>().text = mapName;
        portalObject.GetComponentInChildren<UILabel>().depth = 125;
        dicPortal.Add(tileNumber, mapName);
    }

}
