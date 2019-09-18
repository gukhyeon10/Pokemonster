using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PokemonSpace;

public class GameBuildScript : MonoBehaviour
{

    public UISprite m_Build;

    public int tileNumber;

    public void SetBuildObject()
    {
        int objectSizeX = 1, objectSizeY = 1;
        int movableStart = 0, movableWidth = 0, movableHeight = 0;
        int mapWidth = GameMapDataManager.Instance.width;
        switch (m_Build.spriteName)
        {
            case "Build_01":
                {
                    objectSizeX = 6;
                    objectSizeY = 5;
                    movableWidth = 4;
                    movableHeight = 3;
                    movableStart = tileNumber - 1;

                    break;
                }
            case "Build_02":
                {
                    objectSizeX = 6;
                    objectSizeY = 5;
                    m_Build.transform.localPosition += new Vector3(0, 70);

                    movableWidth = 4;
                    movableHeight = 3;
                    movableStart = tileNumber - 1 - mapWidth;
                    break;
                }
            case "Build_03":
                {
                    objectSizeX = 6;
                    objectSizeY = 5;
                    movableWidth = 4;
                    movableHeight = 3;
                    movableStart = tileNumber - 1;
                    break;
                }
            case "Build_04":
                {
                    objectSizeX = 7;
                    objectSizeY = 7;
                    m_Build.depth += 2;
                    movableWidth = 5;
                    movableHeight = 5;
                    movableStart = tileNumber - 2 - mapWidth;
                    break;
                }
            case "Build_05":
                {
                    objectSizeX = 7;
                    objectSizeY = 6;
                    movableWidth = 7;
                    movableHeight = 6;
                    movableStart = tileNumber - 3 - (mapWidth * 2);
                    break;
                }
            case "Build_06":
                {
                    objectSizeX = 4;
                    objectSizeY = 4;
                    movableWidth = 4;
                    movableHeight = 4;
                    movableStart = tileNumber - 1 - mapWidth;
                    break;
                }
            case "Build_07":
                {
                    objectSizeX = 5;
                    objectSizeY = 5;
                    movableWidth = 5;
                    movableHeight = 4;
                    movableStart = tileNumber - 2 - mapWidth;
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
            m_Build.transform.localPosition += new Vector3(80, 0);
        }
        if (objectSizeY % 2 == 0)
        {
            m_Build.transform.localPosition += new Vector3(0, -80);
        }

        m_Build.transform.localScale = new Vector3(objectSizeX, objectSizeY, 0);

        for (int i = 0; i < movableHeight; i++)
        {
            for (int j = 0; j < movableWidth; j++)
            {
                GameMap.Instance.dicMovable[movableStart + j + (i * mapWidth)] = tileNumber;
            }
        }

        return;
    }

}
