using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningPokemon : MonoBehaviour {

    [SerializeField]
    UISprite mSprite;
    [SerializeField]
    UISpriteAnimation animation;

	// Use this for initialization
	void Start () {
        StartCoroutine(startRun());
	}
	
	
    IEnumerator startRun()
    {
        while(true)
        {
            yield return null;
            if(OpeningCharacter.Instance.isRun)
            {
                break;
                
            }
        }

        if (animation.namePrefix.Equals("025_s_"))
        {
            StartCoroutine(BackAndRun());
        }
        else
        {
            StartCoroutine(Running());
        }
    }

    IEnumerator Running()
    {
        while (true)
        {
            yield return null;
            if (transform.localPosition.y > -1000)
            {
                transform.Translate(Vector3.down * Time.deltaTime);
            }
            else
            {
                animation.Pause();
                break;
            }
        }
    }

    IEnumerator BackAndRun()
    {
        while (true)
        {
            if (transform.localPosition.y >50)
            {
                transform.Translate(Vector3.down * Time.deltaTime);
                yield return null;
            }
            else
            {
                animation.Pause();
                mSprite.spriteName = "025_s_0";
                break;
            }
        }

        yield return new WaitForSeconds(1f);

        mSprite.spriteName = "025_n_1";
        OpeningMeoc.Instance.MeocActive();

        yield return new WaitForSeconds(1f);
        mSprite.spriteName = "025_s_0";
        animation.Play();
        animation.namePrefix = "025_s_";
        while (true)
        {
            if (transform.localPosition.y > -1000)
            {
                transform.Translate(Vector3.down * Time.deltaTime * 1.2f);
                yield return null;
            }
            else
            {
                animation.Pause();
                break;
            }
        }
    }
}
