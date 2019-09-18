using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PokemonSpace;

public class PcSkillManager : MonoBehaviour {

    [SerializeField]
    GameObject PcKeyManager;

    [SerializeField]
    UISprite Sprite_Pokemon;
    [SerializeField]
    UILabel label_Name;
    [SerializeField]
    UILabel label_Level;

    [SerializeField]
    UISprite Sprite_Skill_1;
    [SerializeField]
    UISprite Sprite_Skill_2;
    [SerializeField]
    UISprite Sprite_Skill_3;
    [SerializeField]
    UISprite Sprite_Skill_4;

    public void PcPokemonSkillCheck()
    {
        int pcPokemonIndex = PcKeyManager.GetComponent<PcPokemonKeyManager>().pointPokemonIndex;
        PokemonData pokemon = HeroPokemonManager.Instance.pcPokemonList[pcPokemonIndex];
      
        label_Name.text = pokemon.name;
        label_Level.text = "Lv." + pokemon.level.ToString();
        Sprite_Pokemon.spriteName = pokemon.no + "_1";

        if(pokemon.skill_one != 0)
        {
            Sprite_Skill_1.GetComponent<SkillInit>().SkillInfoInit(pokemon.skill_one);
        }
        else
        {
            Sprite_Skill_1.gameObject.SetActive(false);
        }
        if (pokemon.skill_two != 0)
        {
            Sprite_Skill_2.GetComponent<SkillInit>().SkillInfoInit(pokemon.skill_two);
        }
        else
        {
            Sprite_Skill_2.gameObject.SetActive(false);
        }
        if (pokemon.skill_three != 0)
        {
            Sprite_Skill_3.GetComponent<SkillInit>().SkillInfoInit(pokemon.skill_three);
        }
        else
        {
            Sprite_Skill_3.gameObject.SetActive(false);
        }
        if (pokemon.skill_four != 0)
        {
            Sprite_Skill_4.GetComponent<SkillInit>().SkillInfoInit(pokemon.skill_four);
        }
        else
        {
            Sprite_Skill_4.gameObject.SetActive(false);
        }
    }

    public void CarryPokemonSkillCheck()
    {
        int pcPokemonIndex = PcKeyManager.GetComponent<PcPokemonKeyManager>().selectPokemonIndex;
        PokemonData pokemon = HeroPokemonManager.Instance.carryPokemonList[pcPokemonIndex];

        label_Name.text = pokemon.name;
        label_Level.text = "Lv." + pokemon.level.ToString();
        Sprite_Pokemon.spriteName = pokemon.no + "_1";

        if (pokemon.skill_one != 0)
        {
            Sprite_Skill_1.GetComponent<SkillInit>().SkillInfoInit(pokemon.skill_one);
        }
        else
        {
            Sprite_Skill_1.gameObject.SetActive(false);
        }
        if (pokemon.skill_two != 0)
        {
            Sprite_Skill_2.GetComponent<SkillInit>().SkillInfoInit(pokemon.skill_two);
        }
        else
        {
            Sprite_Skill_2.gameObject.SetActive(false);
        }
        if (pokemon.skill_three != 0)
        {
            Sprite_Skill_3.GetComponent<SkillInit>().SkillInfoInit(pokemon.skill_three);
        }
        else
        {
            Sprite_Skill_3.gameObject.SetActive(false);
        }
        if (pokemon.skill_four != 0)
        {
            Sprite_Skill_4.GetComponent<SkillInit>().SkillInfoInit(pokemon.skill_four);
        }
        else
        {
            Sprite_Skill_4.gameObject.SetActive(false);
        }
    }
    
}
