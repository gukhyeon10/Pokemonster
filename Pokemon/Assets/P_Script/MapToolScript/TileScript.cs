using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PokemonSpace;

public class TileScript : MonoBehaviour
{
    [SerializeField]
    UISprite m_Tile;
    
    public int tileNumber;
    public int tileAngle;

    public void SetBasicTile(string spriteName)
    {
        m_Tile.spriteName = spriteName;
    }

    public void SetNumber(int n)
    {
        this.tileNumber = n;
    }


    public void MapTileAttach()
    {
        if (ToolCursor.Instance.CursorType == (int)ObjectEnum.TILE)
        {
            m_Tile.spriteName = ToolCursor.Instance.GetCursorTile;
            m_Tile.transform.localEulerAngles = new Vector3(0, 0, ToolCursor.Instance.GetCursorRotation);
            tileAngle = ToolCursor.Instance.GetCursorRotation;

            CursorSmartObject();
        }
        else
        {
            return;
        }
    }

    public void MapObjectAttach()
    {
        ToolCursor.Instance.ObjectAttach(tileNumber);
    }

    public void CursorSmartObject()
    {
        Vector2 pos = this.transform.localPosition;
        ToolCursor.Instance.SmartCursorActive(pos.x, pos.y, tileNumber);
    }

    public int GetTileNumber
    {
        get
        {
            return this.tileNumber;
        }
    }

}
