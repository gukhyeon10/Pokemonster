using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour {

    private static OptionManager instance = null;

    public static OptionManager Instance
    {
        get
        {
            return instance;
        }
    }

    AudioSource bgm;

    public bool isBattleAnimation = true;
    public bool isBattleRuleSubstitute = true;
    public bool isBGM = true;
    public bool isEffectSound = true;

    [SerializeField]
    AudioClip walkBgm;
    [SerializeField]
    AudioClip battleBgm;


    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        bgm = this.GetComponent<AudioSource>();
        bgm.loop = true;
    }

    public void BgmControl()
    {
        if(isBGM)
        {
            if(!bgm.isPlaying)
            {
                bgm.Play();
            }
        }
        else
        {
            bgm.Pause();
        }

    }

    void ChangeBgm(AudioClip audio)
    {
        isBGM = false;
        BgmControl();
        bgm.clip = audio;
        isBGM = true;
        BgmControl();

    }

    public void BattleBgm()
    {
        ChangeBgm(battleBgm);
    }

    public void WalkBgm()
    {
        ChangeBgm(walkBgm);
    }
}
