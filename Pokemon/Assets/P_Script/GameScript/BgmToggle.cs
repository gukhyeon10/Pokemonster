using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmToggle : MonoBehaviour {

    [SerializeField]
    UISprite turnOnMusic;
    [SerializeField]
    UISprite turnOffMusic;

    void OnEnable()
    {
        if (OptionManager.Instance.isBGM)
        {
            ClickTurnOnMusic();
        }
        else
        {
            ClickTurnOffMusic();
        }
    }

    public void ClickTurnOnMusic()
    {
        turnOnMusic.spriteName = "Option_Check_Button";
        turnOffMusic.spriteName = "Option_UnCheck_Button";
        OptionManager.Instance.isBGM = true;
        OptionManager.Instance.BgmControl();
    }

    public void ClickTurnOffMusic()
    {
        turnOnMusic.spriteName = "Option_UnCheck_Button";
        turnOffMusic.spriteName = "Option_Check_Button";
        OptionManager.Instance.isBGM = false;
        OptionManager.Instance.BgmControl();
    }
}
