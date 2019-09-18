using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcScript : MonoBehaviour {
    const int EAST = 1, WEST = 2, SOUTH = 3, NORTH = 4;

    public UISprite m_Npc;

    public int tileNumber;
    public void ObjectMouseRightButtonDown()
    {
        if (Input.GetMouseButton(1))
        {
            Debug.Log("this Object Press!");
            ToolCursor.Instance.MapObjectSelect(this.gameObject);
        }
    }

    public void SetNpcObject()
    {
        int objectSizeX = 1, objectSizeY = 1;
        switch (m_Npc.spriteName.Substring(0, 6))
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

        m_Npc.transform.localScale = new Vector3(objectSizeX, objectSizeY);
        m_Npc.transform.localPosition += new Vector3(0, 80);

        //맨 첨 xml파일 읽을때는 예외
        if(!MapFileClass.Instance.isMapLoad)
        {
            switch (ToolCursor.Instance.npcDirect)
            {
                case EAST:
                    {
                        m_Npc.spriteName = m_Npc.spriteName.Substring(0, 6) + "_e_0";
                        break;
                    }
                case WEST:
                    {
                        m_Npc.spriteName = m_Npc.spriteName.Substring(0, 6) + "_w_0";
                        break;
                    }
                case SOUTH:
                    {
                        m_Npc.spriteName = m_Npc.spriteName.Substring(0, 6) + "_s_0";
                        break;
                    }
                case NORTH:
                    {
                        m_Npc.spriteName = m_Npc.spriteName.Substring(0, 6) + "_n_0";
                        break;
                    }
            }
        }
      

    }
}
