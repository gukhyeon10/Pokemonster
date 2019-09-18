using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PokemonSpace;

public class BagScript : MonoBehaviour {

    [SerializeField]
    UISprite BagSprite;

    [SerializeField]
    UISprite background;
    [SerializeField]
    UISprite normal_Button;
    [SerializeField]
    UISprite recovery_Button;
    [SerializeField]
    UISprite ball_Button;
    [SerializeField]
    UISprite tech_Button;
    [SerializeField]
    UISprite fruit_Button;
    [SerializeField]
    UISprite important_Button;

    [SerializeField]
    GameObject Item_1;
    [SerializeField]
    UILabel label_item_dialog;

    AudioSource tab;

    // Use this for initialization
    void Start() {
        tab = this.GetComponent<AudioSource>();
        normal_Button.spriteName += "_select";
        background.color = new Color(222f/255f, 132f/255f, 173f/255f);
        BagSprite.spriteName = "bag_1";
        Item_1.GetComponent<ItemButtonScript>().ItemButtonSet();
    }

    void SelectButtonCancel()
    {
        normal_Button.spriteName = "normal";
        recovery_Button.spriteName = "recovery";
        ball_Button.spriteName = "ball";
        tech_Button.spriteName = "tech";
        fruit_Button.spriteName = "fruit";
        important_Button.spriteName = "important";
    }
    
    public void SelectItemButton(UISprite selectButton)
    {
        tab.Play();
        SelectButtonCancel();
        switch(selectButton.spriteName)
        {
            case "normal":
                {
                    normal_Button.spriteName += "_select";
                    background.color = new Color(222f / 255f, 132f / 255f, 173f / 255f);
                    BagSprite.spriteName = "bag_1";
                    Item_1.GetComponent<ItemButtonScript>().itemType = (int)ItemType.TOOL;
                    break;
                }
            case "recovery":
                {
                    recovery_Button.spriteName = recovery_Button.spriteName+"_select";
                    background.color = new Color(239f / 255f, 132f / 255f, 82f / 255f);
                    BagSprite.spriteName = "bag_2";
                    Item_1.GetComponent<ItemButtonScript>().itemType = (int)ItemType.RECOVERY;
                    break;
                }
            case "tech":
                {
                    tech_Button.spriteName = tech_Button.spriteName+ "_select";
                    background.color = new Color(148f / 255f, 198f / 255f, 49f / 255f);
                    BagSprite.spriteName = "bag_3";
                    Item_1.GetComponent<ItemButtonScript>().itemType = (int)ItemType.TECH;
                    break;
                }
            case "ball":
                {
                    ball_Button.spriteName += "_select";
                    background.color = new Color(231f / 255f, 165f / 255f, 57f / 255f);
                    BagSprite.spriteName = "bag_4";
                    Item_1.GetComponent<ItemButtonScript>().itemType = (int)ItemType.BALL;
                    break;
                }
            case "fruit":
                {
                    fruit_Button.spriteName += "_select";
                    background.color = new Color(49f / 255f, 189f / 255f, 90f / 255f);
                    BagSprite.spriteName = "bag_5";
                    Item_1.GetComponent<ItemButtonScript>().itemType = (int)ItemType.FRUIT;
                    break;
                }
            case "important":
                {
                    important_Button.spriteName += "_select";
                    background.color = new Color(165f / 255f, 90f / 255f, 239f / 255f);
                    BagSprite.spriteName = "bag_6";
                    Item_1.GetComponent<ItemButtonScript>().itemType = 6;
                    break;
                }
            default:
                {
                    break;
                }
        }
        Item_1.GetComponent<ItemButtonScript>().ItemButtonSet();

        label_item_dialog.text = "";
        return;
    }

    public void CloseBag()
    {
        Debug.Log("가방을 닫는다.");
        SceneManager.LoadScene(1);

    }
}
