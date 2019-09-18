using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAnimationToogle : MonoBehaviour
{

    [SerializeField]
    UISprite watch_animation;
    [SerializeField]
    UISprite no_watch_animation;

    void OnEnable()
    {
        if (OptionManager.Instance.isBattleAnimation)
        {
            ClickWatchAnimation();
        }
        else
        {
            ClickDoNotWatchAnimation();
        }
    }

    public void ClickWatchAnimation()
    {
        watch_animation.spriteName = "Option_Check_Button";
        no_watch_animation.spriteName = "Option_UnCheck_Button";
        OptionManager.Instance.isBattleAnimation = true;
    }

    public void ClickDoNotWatchAnimation()
    {
        watch_animation.spriteName = "Option_UnCheck_Button";
        no_watch_animation.spriteName = "Option_Check_Button";
        OptionManager.Instance.isBattleAnimation = false;
    }
}
