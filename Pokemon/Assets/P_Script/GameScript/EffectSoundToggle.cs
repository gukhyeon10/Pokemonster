using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSoundToggle : MonoBehaviour {

    [SerializeField]
    UISprite turnOnEffectSound;
    [SerializeField]
    UISprite turnOffEffectSound;

    void OnEnable()
    {
        if (OptionManager.Instance.isEffectSound)
        {
            ClickTurnOnEffectSound();
        }
        else
        {
            ClickTurnOffEffectSound();
        }
    }

    public void ClickTurnOnEffectSound()
    {
        turnOnEffectSound.spriteName = "Option_Check_Button";
        turnOffEffectSound.spriteName = "Option_UnCheck_Button";
        OptionManager.Instance.isEffectSound = true;
    }

    public void ClickTurnOffEffectSound()
    {
        turnOnEffectSound.spriteName = "Option_UnCheck_Button";
        turnOffEffectSound.spriteName = "Option_Check_Button";
        OptionManager.Instance.isEffectSound = false;
    }
}
