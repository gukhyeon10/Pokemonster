using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioManager : MonoBehaviour {

    private static GameAudioManager instance = null;

    public static GameAudioManager Instance
    {
        get
        {
            return instance;
        }
    }

    public AudioClip[] audio_;
    [SerializeField]
    AudioSource gameAudio;

    void Awake()
    {
        instance = this;
    }

    void OnDestroy()
    {
        instance = null;    
    }

    public void SaveAudio()
    {
        gameAudio.clip = audio_[0];
        gameAudio.Play();
    }

    public void RecoveryAudio()
    {
        OptionManager.Instance.isBGM = false;
        OptionManager.Instance.BgmControl();
        gameAudio.clip = audio_[1];
        gameAudio.Play();

        StartCoroutine(BgmRestart());
    }

    IEnumerator BgmRestart()    // 효과음이 끝나면 다시 배경음 재생
    {
        GameKeyManager.Instance.isRecovery = true;
        while(true)
        {
            if(!(gameAudio.isPlaying))
            {
                break;
            }
            yield return null;
        }

        GameMap.Instance.RecoveryEndDialog();

        GameKeyManager.Instance.isRecovery = false;
        OptionManager.Instance.isBGM = true;
        OptionManager.Instance.BgmControl();


    }

}
