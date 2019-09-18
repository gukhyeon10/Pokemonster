using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokedexCellScript : MonoBehaviour {

    [SerializeField]
    UISprite pokemonSprite;
    [SerializeField]
    UILabel pokemonNo;
    [SerializeField]
    UILabel pokemonName;


	public void CellSetting(string No, string Name)
    {
        pokemonNo.text = No;    // 포켓몬 넘버
        pokemonName.text = Name;  // 포켓몬 이름
        pokemonSprite.spriteName = No; // 포켓몬 아이콘 스프라이트
    }

    public void CellClick()
    {
        Debug.Log(this.pokemonName.text);                                               // 아이콘은 001 형식, 앞모습sprite는 001_0형식
        PokedexManager.Instance.SelectPokemon(this.pokemonSprite.spriteName + "_0");   // 셀 클릭시 해당 포켓몬 정보 포커스
    }
}
