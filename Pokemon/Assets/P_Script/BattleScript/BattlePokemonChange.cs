using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePokemonChange : MonoBehaviour {

    [SerializeField]
    GameObject[] pokemon;


    void OnEnable()
    {
        pokemon[BattleManager.Instance.standPokemonNumber].GetComponent<CarryPokemonCellScript>().InitInfo();
        pokemon[BattleManager.Instance.standPokemonNumber].GetComponent<CarryPokemonCellScript>().HpBarSet();
    }
}
