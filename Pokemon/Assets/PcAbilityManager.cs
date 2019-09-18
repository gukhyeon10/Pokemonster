using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PokemonSpace;

public class PcAbilityManager : MonoBehaviour {
    [SerializeField]
    GameObject PcKeyManager;

    [SerializeField]
    UILabel label_Hp;
    [SerializeField]
    UILabel label_Attack;
    [SerializeField]
    UILabel label_Defence;
    [SerializeField]
    UILabel label_SpecialAttack;
    [SerializeField]
    UILabel label_SpecialDefence;
    [SerializeField]
    UILabel label_Speed;
    [SerializeField]
    UILabel label_Name;
    [SerializeField]
    UILabel label_Level;
    [SerializeField]
    UISprite sprite_Pokemon;


    public void PcPokemonAbilityCheck()
    {
        int pcPokemonIndex = PcKeyManager.GetComponent<PcPokemonKeyManager>().pointPokemonIndex;
        PokemonData pokemon = HeroPokemonManager.Instance.pcPokemonList[pcPokemonIndex];
        label_Hp.text = pokemon.remainHp.ToString() + " / " + pokemon.maxHp.ToString();
        label_Attack.text = pokemon.attack.ToString();
        label_Defence.text = pokemon.defence.ToString();
        label_SpecialAttack.text = pokemon.specialAttack.ToString();
        label_SpecialDefence.text = pokemon.specialDefence.ToString();
        label_Speed.text = pokemon.speed.ToString();
        label_Name.text = pokemon.name;
        label_Level.text = "Lv." + pokemon.level.ToString();
        sprite_Pokemon.spriteName = pokemon.no + "_1";
    }

    public void CarryPokemonAbilityCheck()
    {
        int carryPokemonIndex = PcKeyManager.GetComponent<PcPokemonKeyManager>().selectPokemonIndex;
        PokemonData pokemon = HeroPokemonManager.Instance.carryPokemonList[carryPokemonIndex];
        label_Hp.text = pokemon.remainHp.ToString() + " / " + pokemon.maxHp.ToString();
        label_Attack.text = pokemon.attack.ToString();
        label_Defence.text = pokemon.defence.ToString();
        label_SpecialAttack.text = pokemon.specialAttack.ToString();
        label_SpecialDefence.text = pokemon.specialDefence.ToString();
        label_Speed.text = pokemon.speed.ToString();
        label_Name.text = pokemon.name;
        label_Level.text = "Lv." + pokemon.level.ToString();
        sprite_Pokemon.spriteName = pokemon.no + "_1";
    }
    


}
