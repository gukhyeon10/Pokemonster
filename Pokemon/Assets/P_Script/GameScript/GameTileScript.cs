using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PokemonSpace;

public class GameTileScript : MonoBehaviour
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


    public int GetTileNumber
    {
        get
        {
            return this.tileNumber;
        }
    }

}
