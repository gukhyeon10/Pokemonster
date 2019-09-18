using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningMeoc : MonoBehaviour {

    private static OpeningMeoc instance = null;

    public static OpeningMeoc Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField]
    UISprite mSprite;
    [SerializeField]
    UISpriteAnimation animation;

    [SerializeField]
    GameObject effect;
    [SerializeField]
    UISprite mEffectSprite;
    [SerializeField]
    UISpriteAnimation effectAnimation;


    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void OnDestroy()
    {
        instance = null;    
    }

    public void MeocActive()
    {
        StartCoroutine(Walking());
    }

    IEnumerator Walking()
    {
        while(true)
        {
            yield return null;
            if(transform.localPosition.y >200)
            {
                transform.Translate(Vector3.down * Time.deltaTime * 0.7f);
            }
            else
            {
                animation.Pause();
                break;
            }
        }
        StartCoroutine(Effect());
    }

    IEnumerator Effect()
    {
        yield return new WaitForSeconds(1f);
        effect.SetActive(true);
        mEffectSprite.spriteName = "OpeningEffect_0";
        effectAnimation.Play();
        while(true)
        {
            yield return null;
            if(effectAnimation.frameIndex == 7)
            {
                effectAnimation.Pause();
                break;
            }
        }
        StartCoroutine(EffectScaleUp());
    }

    IEnumerator EffectScaleUp()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.05f);
            if(effect.transform.localScale.x>10f)
            {
                break;
            }
            else
            {
                effect.transform.localScale += Vector3.one;
            }
        }

        OpeningManager.Instance.FadeEffect();
    }

}
