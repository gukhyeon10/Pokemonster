using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OpeningManager : MonoBehaviour {

    private static OpeningManager instance = null;

    public static OpeningManager Instance
    {
        get
        {
            return instance;
        }
    }

    float blinkTime;
    bool isBlinkLabel = false;
    float start = 0f;
    float end = 1f;
    float time = 0f;
    [SerializeField]
    UISprite fadeSprite;
    [SerializeField]
    GameObject openingBg;
    [SerializeField]
    GameObject meoc;

    [SerializeField]
    GameObject logo;
    [SerializeField]
    GameObject meocfront;

    [SerializeField]
    GameObject noticeLabel;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        Screen.SetResolution(1340, 800, false);

    }

    private void OnDestroy()
    {
        instance = null;
    }

    public void FadeEffect()
    {
        StartCoroutine(FadeIn());
    }


    void Update()
    {
        blinkTime += Time.deltaTime;
        if(isBlinkLabel && blinkTime > 0.4f)
        {
            noticeLabel.gameObject.SetActive(!(noticeLabel.activeSelf));
            blinkTime = 0f;
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene(1);
        }
    }


    IEnumerator FadeIn()
    {
        Color color = fadeSprite.color;
        time = 0f;
        color.a = Mathf.Lerp(start, end, time);
        while(color.a < 1f)
        {

            time += Time.deltaTime / 2f;

            color.a = Mathf.Lerp(start, end, time);
            fadeSprite.color = color;

            yield return null;
        }

        Destroy(meoc);
        openingBg.SetActive(true);
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        start = 1f;
        end = 0f;
        Color color = fadeSprite.color;
        time = 0f;
        color.a = Mathf.Lerp(start, end, time);

        while(color.a > 0f)
        {
            time += Time.deltaTime / 2f;

            color.a = Mathf.Lerp(start, end, time);

            fadeSprite.color = color;

            yield return null;
        }

        StartCoroutine(LogoActive());


    }

    IEnumerator LogoActive()
    {
        while(true)
        {
            yield return null;
            if(logo.transform.localPosition.x <0)
            {
                logo.transform.Translate(Vector3.right * Time.deltaTime *3.5f);
                meocfront.transform.Translate(Vector3.left * Time.deltaTime * 3.5f);
            }
            else
            {
                break;
            }
        }

        yield return new WaitForSeconds(0.5f);
        meocfront.GetComponent<UISprite>().spriteName = "moec_02";
        meocfront.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.5f);
        meocfront.GetComponent<UISprite>().spriteName = "moec_01";
        blinkTime = 0f;
        isBlinkLabel = true;
    }
}
