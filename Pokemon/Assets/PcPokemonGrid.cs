using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcPokemonGrid : MonoBehaviour {

    [SerializeField]
    UIGrid gridPc;
    [SerializeField]
    GameObject gbPcPokemon;

	// Use this for initialization
	void Awake() {
        GridPcSet();
	}
	
    void GridPcSet()
    {
        for(int i=0; i<HeroPokemonManager.Instance.pcPokemonList.Count; i++)
        {
            GameObject pcPokemon = NGUITools.AddChild(gridPc.gameObject, gbPcPokemon);
            pcPokemon.GetComponent<PcPokemon>().pcPokemonIndex = i;
            pcPokemon.GetComponent<PcPokemon>().PcPokemonSet();
        }

        gridPc.Reposition();
    }

    public void PokemonPutPc()
    {
        GameObject pcPokemon = NGUITools.AddChild(gridPc.gameObject, gbPcPokemon);
        pcPokemon.GetComponent<PcPokemon>().pcPokemonIndex = HeroPokemonManager.Instance.pcPokemonList.Count - 1;
        pcPokemon.GetComponent<PcPokemon>().PcPokemonSet();

        gridPc.Reposition();

    }

    public void PcPokemonReposition()
    {
        gridPc.transform.DestroyChildren();
        GridPcSet();
    }

}
