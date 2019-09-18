using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeIn : MonoBehaviour {

    [SerializeField]
    UISprite sprite_FadeIn;

    [SerializeField]
    GameObject Ball_One;
    [SerializeField]
    GameObject Ball_Two;
    [SerializeField]
    GameObject Ball_Three;
    [SerializeField]
    GameObject Black_One;
    [SerializeField]
    GameObject Black_Two;
    [SerializeField]
    GameObject Black_Three;

    public void StartFadeIn()
    {
        StartCoroutine(FadeInCorutine());

        OptionManager.Instance.BattleBgm();
    }

    IEnumerator FadeInCorutine()
    {

        Color color = sprite_FadeIn.color;
        float time = 0f;
        color.a = Mathf.Lerp(0f, 1f, time);

        // 화면 반짝 반짝
        for(int i=0; i<3; i++)
        {
            time = 0f;
            while (color.a < 1f)
            {
                time += Time.deltaTime / 0.2f;

                color.a = Mathf.Lerp(0f, 1f, time);

                sprite_FadeIn.color = color;

                yield return null;
            }

            time = 0f;
            while (color.a > 0f)
            {
                time += Time.deltaTime / 0.3f;

                color.a = Mathf.Lerp(1f, 0f, time);

                sprite_FadeIn.color = color;

                yield return null;
            }

        }

        Ball_One.gameObject.SetActive(true);
        Ball_One.transform.localPosition = sprite_FadeIn.transform.localPosition + new Vector3(-2435, -418, 0);
        Black_One.gameObject.SetActive(true);
        Black_One.transform.localPosition = Ball_One.transform.localPosition;
        Ball_Two.gameObject.SetActive(true);
        Ball_Two.transform.localPosition = sprite_FadeIn.transform.localPosition + new Vector3(2435, 0, 0);
        Black_Two.gameObject.SetActive(true);
        Black_Two.transform.localPosition = Ball_Two.transform.localPosition;
        Ball_Three.gameObject.SetActive(true);
        Ball_Three.transform.localPosition = sprite_FadeIn.transform.localPosition + new Vector3(-2435, 418, 0);
        Black_Three.gameObject.SetActive(true);
        Black_Three.transform.localPosition = Ball_Three.transform.localPosition;

        Vector3 Ball_One_Dest = new Vector3(Ball_Two.transform.localPosition.x, Ball_One.transform.localPosition.y, 0);
        while(Ball_One.transform.localPosition.x < Ball_One_Dest.x)
        {
            yield return null;

            Ball_One.transform.localPosition = Vector3.MoveTowards(Ball_One.transform.localPosition, Ball_One_Dest, 3000f * Time.deltaTime);
            Black_One.transform.localPosition = Ball_One.transform.localPosition;
            Ball_Three.transform.localPosition = new Vector3(Ball_One.transform.localPosition.x, Ball_Three.transform.localPosition.y);
            Black_Three.transform.localPosition = Ball_Three.transform.localPosition;
            Ball_Two.transform.localPosition = new Vector3(Ball_One.transform.localPosition.x * -1f, Ball_Two.transform.localPosition.y);
            Black_Two.transform.localPosition = Ball_Two.transform.localPosition;

            Ball_One.transform.Rotate(Vector3.forward * Time.deltaTime * 300f);
            Ball_Three.transform.localRotation = Ball_One.transform.localRotation;
            Ball_Two.transform.Rotate(Vector3.back * Time.deltaTime * 300f);
            
        }

        SceneManager.LoadScene(5);
    }

}
