using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PokemonSpace;


public class SmartCursorScript : MonoBehaviour {
    const int EAST = 1, WEST = 2, SOUTH = 3, NORTH = 4;

    public UISprite m_Object;
    
    int objectNumber;

    public void SetSmartCursor(string objectName)
    {
        switch(ToolCursor.Instance.CursorType)
        {
            case (int)ObjectEnum.TILE:
                {
                    SetTileCursor(objectName);
                    break;
                }
            case (int)ObjectEnum.BUILD:
                {
                    SetBuildCursor(objectName);
                    break;
                }
            case (int)ObjectEnum.NPC:
                {
                    SetNpcCursor(objectName);
                    break;
                }
            case (int)ObjectEnum.BUSH:
                {
                    SetBushCursor(objectName);
                    break;
                }
            default:
                {
                    break;
                }
        }
        
    }

    void SetTileCursor(string objectName)
    {
        m_Object.spriteName = objectName;
        m_Object.transform.localScale = new Vector3(1, 1, 1);
        this.transform.localEulerAngles = new Vector3(0, 0, ToolCursor.Instance.GetCursorRotation);
    }

    public void SetBuildCursor(string objectName)
    {
        m_Object.spriteName = objectName;
        this.transform.localEulerAngles = Vector3.zero;
        m_Object.transform.localPosition = Vector3.zero;
        //오브젝트 원본사이즈 맞추기
        int objectSizeX = 1, objectSizeY = 1;
        switch (objectName)
        {
            case "Build_01":
                {
                    objectSizeX = 6;
                    objectSizeY = 5;
                    break;
                }
            case "Build_02":
                {
                    objectSizeX = 6;
                    objectSizeY = 5;
                    m_Object.transform.localPosition += new Vector3(0, 70);
                    break;
                }
            case "Build_03":
                {
                    objectSizeX = 6;
                    objectSizeY = 5;
                    break;
                }
            case "Build_04":
                {
                    objectSizeX = 7;
                    objectSizeY = 7;
                    break;
                }
            case "Build_05":
                {
                    objectSizeX = 7;
                    objectSizeY = 6;
                    break;
                }
            case "Build_06":
                {
                    objectSizeX = 4;
                    objectSizeY = 4;
                    break;
                }
            case "Build_07":
                {
                    objectSizeX = 5;
                    objectSizeY = 5;
                    break;
                }
            default:
                {
                    break;
                }
        }
     
        //타일위치와 맞추기
        if (objectSizeX % 2 == 0)
        {
            m_Object.transform.localPosition +=  new Vector3(80, 0);
        }
        if (objectSizeY % 2 == 0)
        {
            m_Object.transform.localPosition +=  new Vector3(0, -80);
        }
        
        m_Object.transform.localScale = new Vector3(objectSizeX, objectSizeY, 0);
        return;
    }

    public void SetNpcCursor(string objectName)
    {
        m_Object.spriteName = objectName;
        this.transform.localEulerAngles = Vector3.zero;
        int objectSizeX = 1, objectSizeY = 1;
        switch (objectName.Substring(0,6))
        {
            case "NPC_01":
                {
                    objectSizeX = 2;
                    objectSizeY = 2;
                    break;
                }
            case "NPC_02":
                {
                    objectSizeX = 2;
                    objectSizeY = 2;
                    break;
                }
            case "NPC_03":
                {
                    objectSizeX = 1;
                    objectSizeY = 2;
                    break;
                }
            default:
                {
                    break;
                }
        }
        
        m_Object.transform.localScale = new Vector3(objectSizeX, objectSizeY);
        m_Object.transform.localPosition = new Vector3(0, 80);
     
        switch(ToolCursor.Instance.npcDirect)
        {
            case EAST:
                {
                    m_Object.spriteName = objectName.Substring(0, 6) + "_e_0";
                    break;
                }
            case WEST:
                {
                    m_Object.spriteName = objectName.Substring(0, 6) + "_w_0";
                    break;
                }
            case SOUTH:
                {
                    m_Object.spriteName = objectName.Substring(0, 6) + "_s_0";
                    break;
                }
            case NORTH:
                {
                    m_Object.spriteName = objectName.Substring(0, 6) + "_n_0";
                    break;
                }
        }

    }

    void SetBushCursor(string objectName)
    {
        m_Object.spriteName = objectName;

        float objectSizeX = 1, objectSizeY = 1;
        float objectPositionX = 0, objectPositionY = 0;
        m_Object.transform.localPosition = Vector3.zero;

        switch (objectName)
        {
            case "Bush_03":
                {
                    objectSizeX = 1;
                    objectSizeY = 1.6f;
                    objectPositionX = 0;
                    objectPositionY = 40;
                    break;
                }
            default:
                {
                    break;
                }
        }

        m_Object.transform.localScale = new Vector3(objectSizeX, objectSizeY, 1);
        m_Object.transform.localPosition += new Vector3(objectPositionX, objectPositionY);
        this.transform.localEulerAngles = new Vector3(0, 0, ToolCursor.Instance.GetCursorRotation);
    }

}
