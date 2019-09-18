using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PokemonSpace;

public class PaletteChildScript : MonoBehaviour {

    [SerializeField]
    UISprite m_PaletteChildSprite;

    public void SetPaletteChild(string spriteName)
    {
        m_PaletteChildSprite.spriteName = spriteName;
    }

    public void PaletteChildClick()
    {
         switch (ToolCursor.Instance.CursorType)
         {
             case (int)ObjectEnum.TILE:
                 {
                    ToolCursor.Instance.SetCursorTileName = m_PaletteChildSprite.spriteName;
                     break;
                 }
             case (int)ObjectEnum.BUILD:
                 {
                     ToolCursor.Instance.SetCursorBuildName = m_PaletteChildSprite.spriteName;
                    break;
                 }
             case (int)ObjectEnum.ITEM:
                 {
                     break;
                 }
             case (int)ObjectEnum.NPC:
                 {
                    ToolCursor.Instance.SetCursorNpcName = m_PaletteChildSprite.spriteName;
                    break;
                 }
            case (int)ObjectEnum.BUSH:
                {
                    ToolCursor.Instance.SetCursorBushName = m_PaletteChildSprite.spriteName;
                    break;
                }
         }
        ToolCursor.Instance.SetObject();
        ToolCursor.Instance.SmartCursorActive(0, 0, 0);   //오브젝트 선택시 초기값
    }
}
