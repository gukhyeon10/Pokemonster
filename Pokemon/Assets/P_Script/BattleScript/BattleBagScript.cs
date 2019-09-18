using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PokemonSpace;

public class BattleBagScript : MonoBehaviour {
    
    [SerializeField]
    UISprite recovery_Button;
    [SerializeField]
    UISprite ball_Button;
    [SerializeField]
    UISprite fruit_Button;

    [SerializeField]
    GameObject Item_1;

    void Start()
    {
        recovery_Button.spriteName += "_select";
        Item_1.GetComponent<BattleItemButton>().ItemButtonSet();
    }

    void SelectButtonCancel()
    {
        recovery_Button.spriteName = "recovery";
        ball_Button.spriteName = "ball";
        fruit_Button.spriteName = "fruit";
    }
     
    public void SelectItemButton(UISprite selectButton)
    {
        SelectButtonCancel();
        switch(selectButton.spriteName)
        {
            case "recovery":
                {
                    recovery_Button.spriteName = recovery_Button.spriteName + "_select";
                    Item_1.GetComponent<BattleItemButton>().itemType = (int)ItemType.RECOVERY;
                    break;
                }
            case "ball":
                {
                    ball_Button.spriteName = ball_Button.spriteName + "_select";
                    Item_1.GetComponent<BattleItemButton>().itemType = (int)ItemType.BALL;
                    break;
                }
            case "fruit":
                {
                    fruit_Button.spriteName = fruit_Button.spriteName + "_select";
                    Item_1.GetComponent<BattleItemButton>().itemType = (int)ItemType.FRUIT;
                    break;
                }
            default:
                {
                    break;
                }
        }
        Item_1.GetComponent<BattleItemButton>().ItemButtonSet();
    }
}
