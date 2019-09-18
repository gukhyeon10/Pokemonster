using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRuleToggle : MonoBehaviour {

    [SerializeField]
    UISprite Substitute;
    [SerializeField]
    UISprite Tournament;

    void OnEnable()
    {
        if(OptionManager.Instance.isBattleRuleSubstitute)
        {
            ClickSubstitute();
        }
        else
        {
            ClickTournament();
        }
    }

    public void ClickSubstitute()
    {
        Substitute.spriteName = "Option_Check_Button";
        Tournament.spriteName = "Option_UnCheck_Button";
        OptionManager.Instance.isBattleRuleSubstitute = true;
    }

    public void ClickTournament()
    {
        Substitute.spriteName = "Option_UnCheck_Button";
        Tournament.spriteName = "Option_Check_Button";
        OptionManager.Instance.isBattleRuleSubstitute = false;
    }
}
