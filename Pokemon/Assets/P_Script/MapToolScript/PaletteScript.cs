using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PokemonSpace;

public class PaletteScript : MonoBehaviour
{

    [SerializeField]
    UILabel paletteSelected;

    [SerializeField]
    UIGrid gridPalette;
    [SerializeField]
    GameObject gbPaletteOption;
 
    public void SetPalette()
    {
        gridPalette.transform.DestroyChildren();
        switch (paletteSelected.text)
        {
            case "TILE":
                {
                    ToolCursor.Instance.CursorType = (int)ObjectEnum.TILE;
                    SetTilePalette();
                    break;
                }
            case "BUILD":
                {
                    ToolCursor.Instance.CursorType = (int)ObjectEnum.BUILD;
                    SetObjectPalette();

                    break;
                }
            case "ITEM":
                {
                    break;
                }
            case "NPC":
                {
                    ToolCursor.Instance.CursorType = (int)ObjectEnum.NPC;
                    SetNpcPalette();
                    break;
                }
            case "BUSH":
                {
                    ToolCursor.Instance.CursorType = (int)ObjectEnum.BUSH;
                    SetBushPalette();
                    break;
                }
        }
        ToolCursor.Instance.SetObject();
        ToolCursor.Instance.SmartCursorActive(0, 0, 0); //오브젝트 선택시 초기값
        gridPalette.Reposition();
    }

    void SetTilePalette()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject paletteChildObject = NGUITools.AddChild(gridPalette.gameObject, gbPaletteOption);
            paletteChildObject.GetComponent<UISprite>().atlas = AtlasManager.Instance.atlas_Tile;
            PaletteChildScript paletteChildObjectScript = paletteChildObject.GetComponent<PaletteChildScript>();
            switch (i)
            {
                case 0:
                    {
                        paletteChildObjectScript.SetPaletteChild("Tile_Normal_01");
                        break;
                    }
                case 1:
                    {
                        paletteChildObjectScript.SetPaletteChild("Tile_Normal_02");
                        break;
                    }
                case 2:
                    {
                        paletteChildObjectScript.SetPaletteChild("Tile_Normal_03");
                        break;
                    }
                case 3:
                    {
                        paletteChildObjectScript.SetPaletteChild("Tile_Normal_04");
                        break;
                    }
                case 4:
                    {
                        paletteChildObjectScript.SetPaletteChild("Tile_Normal_05");
                        break;
                    }
                case 5:
                    {
                        paletteChildObjectScript.SetPaletteChild("Tile_Normal_06");
                        break;
                    }
                case 6:
                    {
                        paletteChildObjectScript.SetPaletteChild("Tile_Normal_07");
                        break;
                    }
                case 7:
                    {
                        paletteChildObjectScript.SetPaletteChild("Tile_Normal_08");
                        break;
                    }
                case 8:
                    {
                        paletteChildObjectScript.SetPaletteChild("Tile_Normal_09");
                        break;
                    }
                case 9:
                    {
                        paletteChildObjectScript.SetPaletteChild("Tile_Normal_10");
                        break;
                    }
                default:
                    {
                        paletteChildObjectScript.SetPaletteChild("Tile_Blank");
                        break;
                    }
            }
        }
    }

    void SetObjectPalette()
    {
        for (int i = 0; i < 7; i++)
        {
            GameObject paletteChildObject = NGUITools.AddChild(gridPalette.gameObject, gbPaletteOption);
            paletteChildObject.GetComponent<UISprite>().atlas = AtlasManager.Instance.atlas_Build;
            PaletteChildScript paletteChildObjectScript = paletteChildObject.GetComponent<PaletteChildScript>();

            switch (i)
            {
                case 0:
                    {
                        paletteChildObjectScript.SetPaletteChild("Build_01");
                        break;
                    }
                case 1:
                    {
                        paletteChildObjectScript.SetPaletteChild("Build_02");
                        break;
                    }
                case 2:
                    {
                        paletteChildObjectScript.SetPaletteChild("Build_03");
                        break;
                    }
                case 3:
                    {
                        paletteChildObjectScript.SetPaletteChild("Build_04");
                        break;
                    }
                case 4:
                    {
                        paletteChildObjectScript.SetPaletteChild("Build_05");
                        break;
                    }
                case 5:
                    {
                        paletteChildObjectScript.SetPaletteChild("Build_06");
                        break;
                    }
                case 6:
                    {
                        paletteChildObjectScript.SetPaletteChild("Build_07");
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

    }

    void SetNpcPalette()
    {
        for(int i=0; i<3; i++)
        {
            GameObject paletteChildObject = NGUITools.AddChild(gridPalette.gameObject, gbPaletteOption);
            paletteChildObject.GetComponent<UISprite>().atlas = AtlasManager.Instance.atlas_Npc;
            PaletteChildScript paletteChildObjectScript = paletteChildObject.GetComponent<PaletteChildScript>();

            switch (i)
            {
                case 0:
                    {
                        paletteChildObjectScript.SetPaletteChild("NPC_01_s_0");
                        break;
                    }
                case 1:
                    {
                        paletteChildObjectScript.SetPaletteChild("NPC_02_s_0");
                        break;
                    }
                case 2:
                    {
                        paletteChildObjectScript.SetPaletteChild("NPC_03_s_0");
                        break;
                    }
            }
        }
    }

    void SetBushPalette()
    {
        for(int i=0; i<9; i++)
        {
            GameObject paletteChildObject = NGUITools.AddChild(gridPalette.gameObject, gbPaletteOption);
            paletteChildObject.GetComponent<UISprite>().atlas = AtlasManager.Instance.atlas_Bush;
            PaletteChildScript paletteChildObjectScript = paletteChildObject.GetComponent<PaletteChildScript>();

            switch(i)
            {
                case 0:
                    {
                        paletteChildObjectScript.SetPaletteChild("Bush_01");
                        break;
                    }
                case 1:
                    {
                        paletteChildObjectScript.SetPaletteChild("Bush_02");
                        break;
                    }
                case 2:
                    {
                        paletteChildObjectScript.SetPaletteChild("Bush_03");
                        break;
                    }
                case 3:
                    {
                        paletteChildObjectScript.SetPaletteChild("Bush_04");
                        break;
                    }
                case 4:
                    {
                        paletteChildObjectScript.SetPaletteChild("Bush_05");
                        break;
                    }
                case 5:
                    {
                        paletteChildObjectScript.SetPaletteChild("Bush_06");
                        break;
                    }
                case 6:
                    {
                        paletteChildObjectScript.SetPaletteChild("Bush_07");
                        break;
                    }
                case 7:
                    {
                        paletteChildObjectScript.SetPaletteChild("Bush_08");
                        break;
                    }
                case 8:
                    {
                        paletteChildObjectScript.SetPaletteChild("Bush_09");
                        break;
                    }

            }
        }
    }
}
