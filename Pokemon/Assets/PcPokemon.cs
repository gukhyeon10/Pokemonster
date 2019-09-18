using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcPokemon : MonoBehaviour {

    public int pcPokemonIndex;
    [SerializeField]
    UISprite sprite_Pokemon;
    [SerializeField]
    UISprite sprite_Pointer;

    public void PcPokemonSet()
    {

        sprite_Pokemon.spriteName = HeroPokemonManager.Instance.pcPokemonList[pcPokemonIndex].no;
    }

    public void PokemonPointerEnable()
    {
        sprite_Pointer.gameObject.SetActive(false);
    }

    public void PokemonPointerAble()
    {
        sprite_Pointer.gameObject.SetActive(true);
    }

	
}
