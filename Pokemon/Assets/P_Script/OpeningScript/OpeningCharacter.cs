using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningCharacter : MonoBehaviour {

    private static OpeningCharacter instance = null;

    public static OpeningCharacter Instance
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

    public bool isRun = false;

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

    // Use this for initialization
    void Start () {
        animation.namePrefix = "man_walk_s_";
        animation.Play();
        StartCoroutine(DownWalk());
    }
	
	// Update is called once per frame
	void Update () {
        


    }

    IEnumerator DownWalk()
    {
        while(true)
        {
            yield return null;
            if (transform.localPosition.y >150)
            {
                transform.Translate(Vector3.down * Time.deltaTime * 0.6f);
            }
            else
            {
                animation.Pause();
                mSprite.spriteName = "man_walk_s_0";
                break;
            }
        }
        
        StartCoroutine(BackAndRun());
    }

    IEnumerator BackAndRun()
    {
        yield return new WaitForSeconds(1f);

        mSprite.spriteName = "man_walk_n_1";
        isRun = true;

        yield return new WaitForSeconds(1f);
        mSprite.spriteName = "man_run_s_0";
        animation.Play();
        animation.namePrefix = "man_run_s_";
        while (true)
        {
            if (transform.localPosition.y > -1000)
            {
                transform.Translate(Vector3.down * Time.deltaTime);
                yield return null;
            }
            else
            {
                animation.Pause();
                mSprite.spriteName = "man_walk_s_0";
                break;
            }
        }
    }
}
