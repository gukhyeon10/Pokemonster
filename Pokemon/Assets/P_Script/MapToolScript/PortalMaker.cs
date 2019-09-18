using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PokemonSpace;
using System.Xml;
using UnityEditor;

public class PortalMaker : MonoBehaviour {

    [SerializeField]
    UIPanel portalMakerPanel;
    [SerializeField]
    UIPanel portalMakerActiveButton;
    [SerializeField]
    GameObject portalLabelPrefab;

    [SerializeField]
    GameObject objectManager;

    public void ActivePortalMaker()
    {
        portalMakerPanel.gameObject.SetActive(true);
        portalMakerActiveButton.gameObject.SetActive(false);

        Dictionary<int, GameObject> dicTile_Portal = MapGrid.Instance.dicTile;

        for(int i=0; i< dicTile_Portal.Count; i++)
        {
            GameObject label = NGUITools.AddChild(this.gameObject, portalLabelPrefab);
            label.GetComponent<UILabel>().text = i.ToString();
            label.transform.localPosition = dicTile_Portal[i].transform.localPosition;
        }

    }

    public void ClosePortalMaker()
    {
        portalMakerPanel.gameObject.SetActive(false);
        portalMakerActiveButton.gameObject.SetActive(true);

        if(this.transform.childCount!=0)
        {
            this.transform.DestroyChildren();
        }
    }

    public void MakePortal(UIInput tileNumberInput)
    {
        int tileNumber = int.Parse(tileNumberInput.value);
        if(!MapGrid.Instance.dicTile.ContainsKey(tileNumber))
        {
            Debug.Log(tileNumber);
            return;
        }
        
#if UNITY_EDITOR
        string filePath = EditorUtility.OpenFilePanel("Open Map File Dialog"
                                            , Application.streamingAssetsPath
                                            , "xml");
#endif
        if (filePath.Length != 0)  // 파일 선택
        {
            Debug.Log(filePath);
            filePath = filePath.Substring(PokemonSpace.FilePath.MapFolderPath.Length);
            filePath = filePath.Substring(0, filePath.Length - 4);
            objectManager.GetComponent<ObjectTable>().PortalAttach(tileNumber, filePath);
            ClosePortalMaker();
        }
        
    }
}

