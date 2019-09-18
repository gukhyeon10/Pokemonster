using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonAbility : MonoBehaviour {

    [SerializeField]
    UILabel label_Name;
    [SerializeField]
    UILabel label_Level;
    [SerializeField]
    UISprite sprite_Pokemon;

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

	public void AbilitySet(string no, string name, int level, int remainHp, int maxHp, int attack, int defence, int specialAttack, int specialDefence, int speed)
    {
        label_Name.text = name;
        label_Level.text = "Lv." + level.ToString();
        sprite_Pokemon.spriteName = no + "_0";

        label_Hp.text = remainHp.ToString() + " / " + maxHp.ToString();
        label_Attack.text = attack.ToString();
        label_Defence.text = defence.ToString();
        label_SpecialAttack.text = specialAttack.ToString();
        label_SpecialDefence.text = specialDefence.ToString();
        label_Speed.text = speed.ToString();

        this.gameObject.SetActive(true);

    }

    public void AbilityEnable()
    {
        this.gameObject.SetActive(false);
    }
}
