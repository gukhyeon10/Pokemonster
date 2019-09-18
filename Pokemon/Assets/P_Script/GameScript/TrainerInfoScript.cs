using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainerInfoScript : MonoBehaviour {

    [SerializeField]
    UILabel labelName;
    [SerializeField]
    UILabel labelMoney;
    [SerializeField]
    UILabel labelTotalPokemon;
    [SerializeField]
    UILabel labelPlayTime;
    [SerializeField]
    UILabel labelStartPlayTime;
    
    public void HeroInfoSet()
    {
        labelName.text = HeroInfoManager.Instance.heroName;
        labelMoney.text = HeroInfoManager.Instance.money.ToString() + " 원";
        labelTotalPokemon.text = HeroInfoManager.Instance.totalPokemon.ToString() + " 마리";
        labelPlayTime.text = HeroInfoManager.Instance.playTime.ToString();
        labelStartPlayTime.text = HeroInfoManager.Instance.startPlayTime.ToString();
    }

}
