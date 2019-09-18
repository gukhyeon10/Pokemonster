using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PokemonSpace;

public class SkillInit : MonoBehaviour {

    [SerializeField]
    UILabel label_Name;
    [SerializeField]
    UILabel label_Remain;
    [SerializeField]
    UILabel label_Max;

    public void SkillInfoInit(int skillNo)
    {
        this.GetComponent<UISprite>().spriteName = "Button_" + SkillManager.Instance.dicSkill[skillNo].skill_Type.ToString();
        label_Name.text = SkillManager.Instance.dicSkill[skillNo].name;
        label_Remain.text = SkillManager.Instance.dicSkill[skillNo].pp.ToString();
        label_Max.text = SkillManager.Instance.dicSkill[skillNo].pp.ToString();
    }
}
