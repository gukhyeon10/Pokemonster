using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameKeyManager : MonoBehaviour {

    private static GameKeyManager instance = null;

    public static GameKeyManager Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField]
    GameObject Option;
    [SerializeField]
    GameObject User;

    [SerializeField]
    GameObject Fade;


    public bool isMove = false;
    public bool isRun = false;
    public bool isSaving = false;
    public bool isRecovery = false;
    public bool isDialog = false;
    public bool isBattle = false;

    void Awake()
    {
        instance = this;   
    }

    void OnDestroy()
    {
        instance = null;    
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.S))
        {
            
            Running();
        }
        else
        {
            NotRunnig();
        }

    }

    public void Running()
    {
        isRun = true;
    }

    public void NotRunnig()
    {
        isRun = false;
    }

    public void OpenCarryPokemon()
    {
        if(!isMove && !isDialog &&!isBattle)
        {
            Debug.Log("내가 지니고 있는 포켓몬을 확인합니다.");
            SceneManager.LoadScene(4);
        }
    }

    public void OpenPokedex()
    {
        if(!isMove && !isDialog && !isBattle)
        {
            Debug.Log("도감을 엽니다.");
            SceneManager.LoadScene(3);
        }
    }

    public void OpenBag()
    {
        if (!isMove && !isDialog && !isBattle)
        {
            Debug.Log("가방 버튼 클릭!");
            SceneManager.LoadScene(2);
        }
    }

    public void OpenOption()
    {
        Option.SetActive(true);
    }

    public void CloseOption()
    {
        Option.SetActive(false);
    }

    public void OpenTrainerInfo()
    {

        User.SetActive(true);
    }

    public void CloseTrainerInfo()
    {
        User.SetActive(false);
    }

    public void Saving()
    {
        if(!isMove && !isDialog && !isBattle)
        {
            isSaving = true;
        }
    }

    public void BattleStart()
    {
        Fade.GetComponent<FadeIn>().StartFadeIn();
        
    }
}
