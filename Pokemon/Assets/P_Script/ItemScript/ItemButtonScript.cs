using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PokemonSpace;

public class ItemButtonScript : MonoBehaviour {

    [SerializeField]
    UISprite sprite_Bg;
    [SerializeField]
    UISprite sprite_Item;
    [SerializeField]
    UILabel label_Name;
    [SerializeField]
    UILabel label_Count;

    [SerializeField]
    UILabel label_Dialog;

    public int itemType = (int)ItemType.TOOL;
    int itemNumber = 0;

    public void ItemButtonSet()
    {
        this.gameObject.SetActive(true);
        sprite_Bg.spriteName = "unselect_label";

        foreach (KeyValuePair<int, int> items in HeroItemManager.Instance.dicHeroItem)
        {
            if(HeroItemManager.Instance.dicItemInfo[items.Key].type == itemType)
            {
                itemNumber = items.Key;
                sprite_Item.spriteName = "Item_" + items.Key.ToString();
                label_Name.text = HeroItemManager.Instance.dicItemInfo[items.Key].name;
                label_Count.text = "x" + items.Value.ToString();
                return;
            }
        }

        this.gameObject.SetActive(false);
    }

	public void ItemClick()
    {
        label_Dialog.text = HeroItemManager.Instance.dicItemInfo[itemNumber].text;
        sprite_Bg.spriteName = "select_label";
    }
}
