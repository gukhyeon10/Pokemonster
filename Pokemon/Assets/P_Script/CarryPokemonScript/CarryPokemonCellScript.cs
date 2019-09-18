using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PokemonSpace;

public class CarryPokemonCellScript : MonoBehaviour {
    public int index;
    [SerializeField]
    UISprite sprite_Cell;
    [SerializeField]
    UISprite sprite_Icon;
    [SerializeField]
    UILabel label_Name;
    [SerializeField]
    UILabel label_Level;
    [SerializeField]
    UILabel label_RemainHp;
    [SerializeField]
    UILabel label_MaxHp;

    [SerializeField]
    UISprite sprite_HpBar;
    [SerializeField]
    UISprite sprite_Hp;
    [SerializeField]
    UISlider slider_HpBar;

    string pokemonNo, pokemonName;
    int level, attack, defence, specialAttack, specialDefence, speed, remainHp, maxHp;

    public bool isExist = false;

    bool isChoiceActive = false;

	// Use this for initialization
	void Start () {
        InitInfo();
        HpBarSet();
	}

    public void InitInfo()
    {
        if (HeroPokemonManager.Instance.carryPokemonList.Count > this.index)
        {
            PokemonData pokemonData = HeroPokemonManager.Instance.carryPokemonList[this.index];
            this.pokemonNo = pokemonData.no;
            this.pokemonName = pokemonData.name;
            this.level = pokemonData.level;
            this.attack = pokemonData.attack;
            this.defence = pokemonData.defence;
            this.specialAttack = pokemonData.specialAttack;
            this.specialDefence = pokemonData.specialDefence;
            this.speed = pokemonData.speed;
            this.remainHp = pokemonData.remainHp;
            this.maxHp = pokemonData.maxHp;

            sprite_Icon.spriteName = this.pokemonNo;
            label_Name.text = this.pokemonName;
            label_Level.text = this.level.ToString();
            label_RemainHp.text = this.remainHp.ToString();
            label_MaxHp.text = this.maxHp.ToString();

            this.isExist = true;
        }
        else // 현재 인덱스에 지닌 포켓몬이 없을때 빈 칸 출력
        {
            sprite_Cell.spriteName = "Blank";
            sprite_Icon.gameObject.SetActive(false);
            label_Name.gameObject.SetActive(false);
            label_Level.gameObject.SetActive(false);
            label_RemainHp.gameObject.SetActive(false);
            label_MaxHp.gameObject.SetActive(false);
            sprite_HpBar.gameObject.SetActive(false);

            sprite_Cell.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void HpBarSet()
    {
        slider_HpBar.value = (float)remainHp / (float)maxHp;
        if (slider_HpBar.value >= 0.39f)
        {
            sprite_Hp.spriteName = "Hp_Green";
        }
        else if (slider_HpBar.value >= 0.19f)
        {
            sprite_Hp.spriteName = "Hp_Yellow";
        }
        else
        {
            sprite_Hp.spriteName = "Hp_Red";
        }
    }

    public void SelectedPokemon()
    {
        sprite_Cell.spriteName = "Select";
    }

    public void UnSelectedPokemon()
    {
        sprite_Cell.spriteName = "UnSelect";
    }

    public void SelectedPokemonChioceActive()   // 선택한 포켓몬이 무얼 할지 선택지가 주어지면 위아래로 움직이는 모션 시작
    {
        isChoiceActive = true;
        StartCoroutine(ChoiceActiveMotion());
    }

    public void SelectedPokemonChioceEnable()   // 모션 코루틴 while 문 탈출
    {
        isChoiceActive = false;
    }

    IEnumerator ChoiceActiveMotion()
    {
        while(isChoiceActive)
        {
            yield return new WaitForSeconds(0.1f);
            sprite_Icon.transform.localPosition += (Vector3.up * 10f);
            yield return new WaitForSeconds(0.1f);
            sprite_Icon.transform.localPosition += (Vector3.down * 10f);
        }
    }
}
