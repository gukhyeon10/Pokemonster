using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleKeyManager : MonoBehaviour {
    

    private static BattleKeyManager instance = null;

    public int changePokemonIndex = 0;
    [SerializeField]
    UILabel label_changeDialog;

    public static BattleKeyManager Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void OnDestroy()
    {
        instance = null;    
    }

    public void PanelTransform(GameObject ActivePanel, GameObject HidePanel)
    {
        HidePanel.gameObject.SetActive(false);
        ActivePanel.gameObject.SetActive(true);
    }
    
    public void ClickChangePokemon(int pokemonIndex, GameObject ChoicePanel)  // 교체할 포켓몬을 포커스 할때 해당 포켓몬의 인덱스
    {
        changePokemonIndex = pokemonIndex;
        label_changeDialog.text = HeroPokemonManager.Instance.carryPokemonList[pokemonIndex].name + "을 어떻게 할까?";
        ChoicePanel.gameObject.SetActive(true);
    }

    public void ReturnFocusPokemon()
    {
        label_changeDialog.text = "포켓몬을 선택해 주십시오";
    }

    public void ItemUseActive(GameObject UseButton, GameObject UseCancelButton)
    {
        UseButton.gameObject.SetActive(true);
        UseCancelButton.gameObject.SetActive(true);
    }

    public void ItemUse(int itemNumber, GameObject UseButton, GameObject UseCancelButton, GameObject BattleBagPanel)
    {
        UseButton.gameObject.SetActive(false);
        UseCancelButton.gameObject.SetActive(false);
        BattleBagPanel.gameObject.SetActive(false);

        BattleManager.Instance.UseItem(itemNumber);
    }

    public void ItemUseCancel(UISprite ItemBgSprite, GameObject UseButton, GameObject UseCancelButton)
    {
        ItemBgSprite.spriteName = "unselect_label";
        UseButton.gameObject.SetActive(false);
        UseCancelButton.gameObject.SetActive(false);
    }


    public void BattleRun()
    {
        BattleUIManager.Instance.SceneChange();
    }
}
