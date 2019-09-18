using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PokemonSpace;

public class GameNpcScript : MonoBehaviour {

    public UISprite m_Npc;

    public int tileNumber;

    public string itemNumber;
    public string itemDialog;
    public string dialog;
    public bool isMoveOn;
    public bool isFightOn;

    const int EAST = 1, WEST = 2, SOUTH = 3, NORTH = 4;

    public void SetNpcObject()
    {
        float objectSizeX = 1, objectSizeY = 1;
        switch (m_Npc.spriteName.Substring(0, 6))
        {
            case "NPC_01":
                {
                    objectSizeX = 1.5f;
                    objectSizeY = 1.6f;
                    break;
                }
            case "NPC_02":
                {
                    objectSizeX = 1.2f;
                    objectSizeY = 1.6f;
                    break;
                }
            case "NPC_03":
                {
                    objectSizeX = 1f;
                    objectSizeY = 1.8f;
                    break;
                }
            default:
                {
                    break;
                }
        }

        m_Npc.transform.localScale = new Vector3(objectSizeX, objectSizeY);
        m_Npc.transform.localPosition += new Vector3(0, 60);
        GameMap.Instance.dicMovable[tileNumber] = tileNumber;
    }

    public void SetNpcDetail(NpcData npcData)
    {
        this.itemNumber = npcData.itemNumber;
        this.itemDialog = npcData.itemDialog;
        this.dialog = npcData.dialog;
        this.isMoveOn = npcData.isMoveOn;
        this.isFightOn = npcData.isFightOn;
    }

    public void LookAtHero(int heroDirect)
    {
        switch(heroDirect)
        {
            case EAST:
                {
                    m_Npc.spriteName = m_Npc.spriteName.Substring(0, 7) + "w_0";
                    break;
                }
            case WEST:
                {
                    m_Npc.spriteName = m_Npc.spriteName.Substring(0, 7) + "e_0";
                    break;
                }
            case SOUTH:
                {
                    m_Npc.spriteName = m_Npc.spriteName.Substring(0, 7) + "n_0";
                    break;
                }
            case NORTH:
                {
                    m_Npc.spriteName = m_Npc.spriteName.Substring(0, 7) + "s_0";
                    break;
                }
        }

    }

}
