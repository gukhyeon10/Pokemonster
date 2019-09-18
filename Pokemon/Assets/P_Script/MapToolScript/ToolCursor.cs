using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PokemonSpace;

public class ToolCursor : MonoBehaviour {

    [SerializeField]
    GameObject objectPanel;
    [SerializeField]
    UISprite m_CurrentCursorSprite;

    GameObject cursorSmartObject;

    private static ToolCursor instance = null;

    int width, height;
    int cursorType;
    string objectName;
    string tileName, buildName, npcName, bushName;
    int tileRotation;

    const int EAST = 1, WEST = 2, SOUTH = 3, NORTH = 4;
    public int npcDirect = SOUTH;

    GameObject selectObject;
    public static ToolCursor Instance
    {
        get
        {
            return instance;
        }   
    }

    void Awake()
    {
        instance = this;
        this.selectObject = null;
        tileRotation = 0;

        this.cursorSmartObject = objectPanel.GetComponent<ObjectTable>().CursorSmartObject();
        this.cursorSmartObject.gameObject.SetActive(true);
    }

    void OnDestroy()
    {
        instance = null;    
    }

    void Start()
    {   //스마트 커서 초기값
        tileName = "Tile_Normal_02";
        buildName = "Build_01";
        npcName = "NPC_01_s_0";
        bushName = "Bush_01";
    }

    void Update()
    {
        SelectedObjectDelete();

        if(Input.GetKeyDown(KeyCode.Q))
        {
            bool isActive = this.cursorSmartObject.gameObject.activeSelf;
            this.cursorSmartObject.gameObject.SetActive(!isActive);
        }

        TileRotate();

        NpcRotate();
    }

    void SelectedObjectDelete()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            if (selectObject != null)
            {
                objectPanel.GetComponent<ObjectTable>().ObjectDelete(selectObject);
                selectObject = null;
            }
        }
    }

    void TileRotate()
    {

        if (CursorType == (int)ObjectEnum.TILE || CursorType == (int)ObjectEnum.BUSH)
        {
            if (Input.GetKeyDown(KeyCode.PageDown))
            {
                tileRotation -= 90;
                if (tileRotation == -360)
                {
                    tileRotation = 0;
                }
            }
            else if (Input.GetKeyDown(KeyCode.PageUp))
            {
                tileRotation += 90;
                if (tileRotation == 360)
                {
                    tileRotation = 0;
                }
            }
            m_CurrentCursorSprite.transform.localEulerAngles = new Vector3(0, 0, tileRotation);
            cursorSmartObject.transform.localEulerAngles = new Vector3(0, 0, tileRotation);
            //cursorSmartObject.GetComponentInChildren<Transform>().localEulerAngles = new Vector3(0, 0, tileRotation);
        }
    }

    void NpcRotate()
    {
        if(CursorType == (int)ObjectEnum.NPC)
        {
            if(Input.GetKeyDown(KeyCode.PageDown))
            {
                npcDirect -= 1;
                if(npcDirect <= 0)
                {
                    npcDirect = NORTH;
                }
            }
            else if(Input.GetKeyDown(KeyCode.PageUp))
            {
                npcDirect += 1;
                if(npcDirect >=5)
                {
                    npcDirect = EAST;
                }
            }

            UISprite smartCursorSprite = cursorSmartObject.GetComponentInChildren<UISprite>();
            switch (ToolCursor.Instance.npcDirect)
            {
                case EAST:
                    {
                        m_CurrentCursorSprite.spriteName = m_CurrentCursorSprite.spriteName.Substring(0, 6) + "_e_0";
                        smartCursorSprite.spriteName = smartCursorSprite.spriteName.Substring(0, 6) + "_e_0";
                        break;
                    }
                case WEST:
                    {
                        m_CurrentCursorSprite.spriteName = m_CurrentCursorSprite.spriteName.Substring(0, 6) + "_w_0";
                        smartCursorSprite.spriteName = smartCursorSprite.spriteName.Substring(0, 6) + "_w_0";
                        break;
                    }
                case SOUTH:
                    {
                        m_CurrentCursorSprite.spriteName = m_CurrentCursorSprite.spriteName.Substring(0, 6) + "_s_0";
                        smartCursorSprite.spriteName = smartCursorSprite.spriteName.Substring(0, 6) + "_s_0";
                        break;
                    }
                case NORTH:
                    {
                        m_CurrentCursorSprite.spriteName = m_CurrentCursorSprite.spriteName.Substring(0, 6) + "_n_0";
                        smartCursorSprite.spriteName = smartCursorSprite.spriteName.Substring(0, 6) + "_n_0";
                        break;
                    }
            }

        }
    }

    public void SetObject()
    {
        switch (CursorType)
        {
            case (int)ObjectEnum.TILE:
                {
                    m_CurrentCursorSprite.atlas = AtlasManager.Instance.atlas_Tile;
                    m_CurrentCursorSprite.transform.localEulerAngles = new Vector3(0, 0, tileRotation);
                    this.objectName = this.tileName;
                    Debug.Log(this.objectName + " tile Click!");
                    break;
                }
            case (int)ObjectEnum.BUILD:
                {

                    m_CurrentCursorSprite.atlas = AtlasManager.Instance.atlas_Build;
                    this.objectName = this.buildName;
                    m_CurrentCursorSprite.transform.localEulerAngles = Vector3.zero;
                    Debug.Log(this.objectName + " build Click!");
                    break;
                }
            case (int)ObjectEnum.NPC:
                {
                    m_CurrentCursorSprite.atlas = AtlasManager.Instance.atlas_Npc;
                    this.objectName = this.npcName;
                    m_CurrentCursorSprite.transform.localEulerAngles = Vector3.zero;

                    Debug.Log(this.objectName + " npc Click!");
                    break;
                }
            case (int)ObjectEnum.BUSH:
                {
                    m_CurrentCursorSprite.atlas = AtlasManager.Instance.atlas_Bush;
                    this.objectName = this.bushName;
                    m_CurrentCursorSprite.transform.localEulerAngles = new Vector3(0, 0, tileRotation);
                    Debug.Log(this.objectName + " bush Click!");
                    break;
                }
        }
        cursorSmartObject.GetComponentInChildren<UISprite>().atlas = m_CurrentCursorSprite.atlas;
        cursorSmartObject.GetComponentInChildren<UISprite>().spriteName = objectName;
        m_CurrentCursorSprite.spriteName = objectName;
    }
    

    public void SetMapSize(int x, int y)
    {
        this.width = x;
        this.height = y;
        
    }

    public void SmartCursorActive(float x, float y, int tileNumber)
    {
        cursorSmartObject.transform.localPosition = new Vector3(x, y);
        SmartCursorScript smartCursorScript = cursorSmartObject.GetComponent<SmartCursorScript>();
        smartCursorScript.SetSmartCursor(objectName);

        if (width > 0)
        {
            smartCursorScript.m_Object.depth = (tileNumber / width) + 1;
        }

    }

    public void ObjectAttach(int tileNumber)
    {
        if(objectName != null)
        {
            objectPanel.GetComponent<ObjectTable>().ObjectAttach(objectName, tileNumber);
        }
        else
        {
            Debug.Log("Object Not Selected!");
        }
    }
    

    public void MapObjectSelect(GameObject selectObject)
    {
        Color color;
        if (this.selectObject != null)
        {
            color = this.selectObject.GetComponentInChildren<UISprite>().color;
            color.a = 1f;
            this.selectObject.GetComponentInChildren<UISprite>().color = color;

        }
            this.selectObject = selectObject;
            color = this.selectObject.GetComponentInChildren<UISprite>().color;
            color.a = 0.6f;
            this.selectObject.GetComponentInChildren<UISprite>().color = color;
        
    }

    public string SetCursorTileName
    {
        set
        {
            this.tileName = value;
        }
    }

    public string SetCursorBuildName
    {
        set
        {
            this.buildName = value;
        }
    }

    public string SetCursorNpcName
    {
        set
        {
            this.npcName = value;
        }
    }

    public string SetCursorBushName
    {
        set
        {
            this.bushName = value;
        }
    }

    public string GetCursorTile
    {
        get
        {
            return objectName;
        }
    }

    public Vector3 GetCursorObjectPosition
    {
        get
        {
            return cursorSmartObject.transform.localPosition;
        }
    }

    public Vector3 GetCursorObjectScale
    {
        get
        {
            return cursorSmartObject.transform.localScale;
        }
    }


    public int GetCursorRotation
    {
        get
        {
            return this.tileRotation;
        }
    }

    public int CursorType
    {
        set
        {
            this.cursorType = value;
        }
        get
        {
            return this.cursorType;
        }
    }


   
}
