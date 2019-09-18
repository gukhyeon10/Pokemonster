using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PokemonSpace;

public class SkillButtonScript : MonoBehaviour {

    [SerializeField]
    UISprite sprite_button;
    [SerializeField]
    UILabel Label_SkillName;
    [SerializeField]
    UILabel Label_RemainPp;
    [SerializeField]
    UILabel Label_MaxPp;

    public int skillNo = 0;

	public void SkillActive(int no, int remainPp)
    {
        if(no != 0)
        {
            skillNo = no;
            sprite_button.spriteName = "Button_" + SkillManager.Instance.dicSkill[no].skill_Type.ToString();

            Label_SkillName.text = SkillManager.Instance.dicSkill[no].name;
            Label_RemainPp.text = remainPp.ToString();
            Label_MaxPp.text = SkillManager.Instance.dicSkill[no].pp.ToString();

        }
        else
        {
            this.gameObject.SetActive(false);
        }

    }

}
