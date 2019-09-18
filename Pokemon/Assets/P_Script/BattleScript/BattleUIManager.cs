using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PokemonSpace;

public class BattleUIManager : MonoBehaviour {

    private static BattleUIManager instance = null;
    public static BattleUIManager Instance
    {
        get
        {
            return instance;
        }
    }

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
    const int ME = 0;
    const int OP = 1;

    [SerializeField]
    UISprite sprite_FadeOut;

    [SerializeField]
    GameObject OpField;
    [SerializeField]
    GameObject OpHpBar;
    [SerializeField]
    GameObject MyField;
    [SerializeField]
    GameObject MyHpBar;
    [SerializeField]
    UISprite sprite_MyHp;
    [SerializeField]
    UISprite sprite_OpHP;
    [SerializeField]
    UISlider slider_MyHp;
    [SerializeField]
    UISlider slider_OpHp;


    [SerializeField]
    UISprite OpPokemon;
    [SerializeField]
    UILabel OpName;
    [SerializeField]
    UILabel OpLevel;
    public int OpRemainHp;

    [SerializeField]
    GameObject HeroBack;
    [SerializeField]
    UISprite MyPokemon;
    [SerializeField]
    UILabel MyName;
    [SerializeField]
    UILabel MyLevel;
    [SerializeField]
    UILabel MyRemainHp;
    [SerializeField]
    UILabel MyMaxHp;
    [SerializeField]
    GameObject MyConditionEffect;
    [SerializeField]
    GameObject OpConditionEffect;


    [SerializeField]
    UILabel Label_Dialog;

    Vector3 hpBarDestination;

    [SerializeField]
    GameObject First_Skill;
    [SerializeField]
    GameObject Second_Skill;
    [SerializeField]
    GameObject Third_Skill;
    [SerializeField]
    GameObject Forth_Skill;

    [SerializeField]
    GameObject ProgressPanel;
    [SerializeField]
    GameObject ChoicePanel;

    [SerializeField]
    GameObject MonsterBall;

    // Use this for initialization
    void Start () {
        HeroBack.GetComponent<UISpriteAnimation>().Pause();

        PokemonData opPokemon = OpponentPokemonManager.Instance.wildPokemon;
        OpPokemon.spriteName = opPokemon.no + "_0";
        OpName.text = opPokemon.name;
        OpLevel.text = opPokemon.level.ToString();

        hpBarDestination = MyHpBar.transform.localPosition;

        for (int i = 0; i < HeroPokemonManager.Instance.carryPokemonList.Count; i++)  // 체력이 남은 순서가 앞선 포켓몬
        {
            if (HeroPokemonManager.Instance.carryPokemonList[i].remainHp > 0)
            {
                BattleManager.Instance.standPokemonNumber = i;   // 주자 포켓몬 인덱스
                break;
            }
        }

        HpBarInit();

        StartCoroutine(FadeOutCorutine());
        StartCoroutine(WildPokemonAppear());
        StartCoroutine(I_Appear());
    }
	
    IEnumerator FadeOutCorutine()
    {
        Color color = sprite_FadeOut.color;
        float time = 0f;
        color.a = Mathf.Lerp(1f, 0f, time);

        while(color.a > 0f)
        {
            time += Time.deltaTime / 0.6f;

            color.a = Mathf.Lerp(1f, 0f, time);

            sprite_FadeOut.color = color;

            yield return null;
        }
    }

	IEnumerator WildPokemonAppear()
    {

        Vector3 hpBarDestination = OpHpBar.transform.localPosition;
        Vector3 destination = OpField.transform.localPosition;
        OpHpBar.transform.localPosition = new Vector3(-145f, OpHpBar.transform.localPosition.y);

        OpField.transform.localPosition = new Vector3(-148f, OpField.transform.localPosition.y);
        while (true)
        {
            OpField.transform.localPosition = Vector3.MoveTowards(OpField.transform.localPosition, destination, 150f * Time.deltaTime);
            yield return null;

            if (OpField.transform.localPosition == destination)
            {
                break;
            }
        }
        
        while (true)
        {
            OpHpBar.transform.localPosition = Vector3.MoveTowards(OpHpBar.transform.localPosition, hpBarDestination, 250f * Time.deltaTime);
            yield return null;

            if (OpHpBar.transform.localPosition == destination)
            {
                break;
            }
        }
    }

    IEnumerator I_Appear()
    {
       
        Vector3 destination = MyField.transform.localPosition;
        MyHpBar.transform.localPosition = new Vector3(150f, MyHpBar.transform.localPosition.y);
        MyField.transform.localPosition = new Vector3(170f, MyField.transform.localPosition.y);
        while (true)
        {
            MyField.transform.localPosition = Vector3.MoveTowards(MyField.transform.localPosition, destination, 180f * Time.deltaTime);
            yield return null;

            if (MyField.transform.localPosition == destination)
            {
                break;
            }
        }
        yield return null;

        Label_Dialog.text = "앗! 야생의 " + OpName.text + "가 나타났다!";

        
        while(true)
        {
            yield return null;
            if(Input.anyKeyDown)
            {
                
                 BattleManager.Instance.LaunchPokemon(BattleManager.Instance.standPokemonNumber);  // 출전할 포켓몬 인덱스
                 
                StartCoroutine(I_Off());
                break;
            }
        }

        HeroBack.GetComponent<UISpriteAnimation>().Play(); 

    }

    IEnumerator I_Off()
    {
        Vector3 destination = new Vector3(-80f, HeroBack.transform.localPosition.y);
        while (true)
        {
            HeroBack.transform.localPosition = Vector3.MoveTowards(HeroBack.transform.localPosition, destination, 70f * Time.deltaTime);
            yield return null;

            if (HeroBack.transform.localPosition == destination)
            {
                break;
            }
        }

    }

    public void MyPokemonInfoBar(string name, int level, int remainHp, int maxHp)
    {
        MyName.text = name;
        MyLevel.text = level.ToString();
        MyRemainHp.text = remainHp.ToString();
        MyMaxHp.text = maxHp.ToString();

        slider_MyHp.value = float.Parse(MyRemainHp.text) / float.Parse(MyMaxHp.text);
        HpBarInit();

        StartCoroutine(AppearMyHpBar());
    }

    IEnumerator AppearMyHpBar()
    {
        while (true)
        {
            MyHpBar.transform.localPosition = Vector3.MoveTowards(MyHpBar.transform.localPosition, hpBarDestination, 180f * Time.deltaTime);
            yield return null;

            if (MyHpBar.transform.localPosition == hpBarDestination)
            {
                break;
            }
        }
    }

    public void OutMyPokemon()
    {
        StartCoroutine(OutMyHpBarCorutine());
        StartCoroutine(OutMyPokemonCorutine());
    }

    IEnumerator OutMyHpBarCorutine()
    {
        Vector3 destination = new Vector3(150f, MyHpBar.transform.localPosition.y);
        while (true)
        {
            MyHpBar.transform.localPosition = Vector3.MoveTowards(MyHpBar.transform.localPosition, destination, 180f * Time.deltaTime);
            yield return null;

            if (MyHpBar.transform.localPosition == destination)
            {
                break;
            }
        }
    }
    
    IEnumerator OutMyPokemonCorutine()
    {
        Vector3 destination = new Vector3(-85f, MyPokemon.transform.localPosition.y);
        while (true)
        {
            MyPokemon.transform.localPosition = Vector3.MoveTowards(MyPokemon.transform.localPosition, destination, 180f * Time.deltaTime);
            yield return null;

            if (MyPokemon.transform.localPosition == destination)
            {
                BattleManager.Instance.isChangeAnimation = false;
                break;
            }
        }
    }

    public void ActiveChoicePanel()
    {
        ChoicePanel.gameObject.SetActive(true);
    }

    public void SkillActive(int pokemonIndex)
    {
        PokemonData pokemonData = HeroPokemonManager.Instance.carryPokemonList[pokemonIndex];
        First_Skill.GetComponent<SkillButtonScript>().SkillActive(pokemonData.skill_one, pokemonData.skill_one_pp);
        Second_Skill.GetComponent<SkillButtonScript>().SkillActive(pokemonData.skill_two, pokemonData.skill_two_pp);
        Third_Skill.GetComponent<SkillButtonScript>().SkillActive(pokemonData.skill_three, pokemonData.skill_three_pp);
        Forth_Skill.GetComponent<SkillButtonScript>().SkillActive(pokemonData.skill_four, pokemonData.skill_four_pp);
    }

    //포켓몬 공격 모션
    public void SkillAnimation(int subject, int skillNo)
    {
        if(SkillManager.Instance.dicSkill[skillNo].power != 0)
        {
            BattleManager.Instance.isBattleAnimation = true;
            StartCoroutine(AttackAnimationCorutine(subject));
        }
    }

    IEnumerator AttackAnimationCorutine(int subject)
    {
        UISprite subjectPokemon = null;
        Vector3 inPlace = new Vector3();
        Vector3 front = new Vector3();
        switch (subject)
        {
            case ME:
                {
                    subjectPokemon = MyPokemon;
                    front = subjectPokemon.transform.localPosition;
                    front.x += 24f;
                    break;
                }
            case OP:
                {
                    subjectPokemon = OpPokemon;
                    front = subjectPokemon.transform.localPosition;
                    front.x -= 24f;

                    break;
                }
        }
        inPlace = subjectPokemon.transform.localPosition;
        while(true)
        {
            yield return new WaitForSeconds(0.033f);
            subjectPokemon.transform.localPosition = Vector3.MoveTowards(subjectPokemon.transform.localPosition, front, 800f * Time.deltaTime);
            if(subjectPokemon.transform.localPosition == front)
            {
                break;
            }
        }

        while (true)
        {
            yield return new WaitForSeconds(0.033f);
            subjectPokemon.transform.localPosition = Vector3.MoveTowards(subjectPokemon.transform.localPosition, inPlace, 800f * Time.deltaTime);
            if(subjectPokemon.transform.localPosition == inPlace)
            {
                break;
            }
        }


        BattleManager.Instance.isBattleAnimation = false;
    }


    public void ConditionEffect(int subject)
    {
        StartCoroutine(ConditionEffectCorutine(subject));  
    }

    IEnumerator ConditionEffectCorutine(int subject)
    {
        yield return null;
        GameObject ConditionEffect = null;
        switch(subject)
        {
            case ME:
                {
                    ConditionEffect = MyConditionEffect;
                    break;
                }
            case OP:
                {
                    ConditionEffect = OpConditionEffect;
                    break;
                }
        }
        ConditionEffect.gameObject.SetActive(true);
        ConditionEffect.GetComponent<UISpriteAnimation>().Play();
        while (true)
        {
            yield return null;
            if(!(ConditionEffect.GetComponent<UISpriteAnimation>().isPlaying))
            {
                ConditionEffect.GetComponent<UISprite>().spriteName = "ConditionEffect_0";
                ConditionEffect.gameObject.SetActive(false);
                break;
            }
        }
    }

    public void HpChange(int subject)  //hp변동이 있는 객체가 누군지
    {
        BattleManager.Instance.isBattleAnimation = true;
        switch (subject)
        {
            case ME:
                {
                    StartCoroutine(HpChangeAnimation(ME));
                    break;
                }
            case OP:
                {
                    StartCoroutine(HpChangeAnimation(OP));
                    break;
                }
        }

    }

    void HpBarInit()
    {
        slider_MyHp.value = (float)(HeroPokemonManager.Instance.carryPokemonList[BattleManager.Instance.standPokemonNumber].remainHp) / (float)(HeroPokemonManager.Instance.carryPokemonList[BattleManager.Instance.standPokemonNumber].maxHp);
         
        if (slider_MyHp.value >= 0.4f)
        {
            sprite_MyHp.spriteName= "Hp_Green";
        }
        else if (slider_MyHp.value >= 20f)
        {
            sprite_MyHp.spriteName = "Hp_Yellow";
        }
        else
        {
            sprite_MyHp.spriteName = "Hp_Red";
        }

        slider_OpHp.value = (float)(OpponentPokemonManager.Instance.wildPokemon.remainHp) / (float)(OpponentPokemonManager.Instance.wildPokemon.maxHp);

        if (slider_OpHp.value >= 0.4f)
        {
            sprite_OpHP.spriteName = "Hp_Green";
        }
        else if (slider_OpHp.value >= 20f)
        {
            sprite_OpHP.spriteName = "Hp_Yellow";
        }
        else
        {
            sprite_OpHP.spriteName = "Hp_Red";
        }

    }

    IEnumerator HpChangeAnimation(int subject)
    {
        int maxHp = 0;
        float firstHp = 0;
        float lastHp = 0;
        UISprite sprite_Hp = null;
        UISlider slider_Hp = null;
        switch (subject)
        {
            case ME:
                {
                    maxHp = HeroPokemonManager.Instance.carryPokemonList[BattleManager.Instance.standPokemonNumber].maxHp;
                    firstHp = int.Parse(MyRemainHp.text);
                    lastHp = int.Parse(HeroPokemonManager.Instance.carryPokemonList[BattleManager.Instance.standPokemonNumber].remainHp.ToString());
                    sprite_Hp = sprite_MyHp;
                    slider_Hp = slider_MyHp;
                    break;
                }
            case OP:
                {
                    maxHp = OpponentPokemonManager.Instance.wildPokemon.maxHp;
                    firstHp = OpRemainHp;
                    lastHp = OpponentPokemonManager.Instance.wildPokemon.remainHp;
                    sprite_Hp = sprite_OpHP;
                    slider_Hp = slider_OpHp;
                    break;
                }
        }
        if (lastHp <= 0)
        {
            lastHp = 0;
        }

        //hp 바 닳는 애니메이션 
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if(firstHp <= lastHp)
            {
                if(subject == OP)
                {
                    OpRemainHp = (int)lastHp;
                }
                break;
            }
            else
            {
                firstHp-=0.5f;
                if (subject == ME)
                {
                    MyRemainHp.text = ((int)firstHp).ToString();
                }
                float hpPercent = (float)firstHp / (float)maxHp * 100f;
                slider_Hp.value = (float)firstHp /(float)maxHp;
                if(firstHp <= 0.5f)   //0.5만 남으면 사실상 게임오버
                {
                    slider_Hp.value = 0f;
                }

                if (hpPercent >= 40f)
                {
                    sprite_Hp.spriteName = "Hp_Green"; 
                }
                else if(hpPercent >= 20f)
                {
                    sprite_Hp.spriteName = "Hp_Yellow";
                }
                else
                {
                    sprite_Hp.spriteName = "Hp_Red";
                }
                
            }

        }
        BattleManager.Instance.isBattleAnimation = false;
    }

    public void HpRecovery(int itemNumber)
    {
        float recoveryHp = 0f;

        switch(itemNumber)
        {
            case 1: // 상처약
                {
                    recoveryHp = 20f;
                    break;
                }
            case 2: // 오랭열매
                {
                    recoveryHp = 10f;
                    break;
                }
        }

        StartCoroutine(HpRecoveryCorutine(recoveryHp));
    }

    IEnumerator HpRecoveryCorutine(float recoveryHp)
    {
        float maxHp = 0;
        float firstHp = 0;
        UISprite sprite_Hp = null;
        UISlider slider_Hp = null;
        
        maxHp = HeroPokemonManager.Instance.carryPokemonList[BattleManager.Instance.standPokemonNumber].maxHp;
        firstHp = int.Parse(HeroPokemonManager.Instance.carryPokemonList[BattleManager.Instance.standPokemonNumber].remainHp.ToString());
        sprite_Hp = sprite_MyHp;
        slider_Hp = slider_MyHp;
          
       
        //hp 회복 애니메이션 
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
           
            if(firstHp >= maxHp || recoveryHp <= 0f)
            {
                PokemonData pokemonRecovery = HeroPokemonManager.Instance.carryPokemonList[BattleManager.Instance.standPokemonNumber];
                pokemonRecovery.remainHp = (int)firstHp;
                HeroPokemonManager.Instance.carryPokemonList[BattleManager.Instance.standPokemonNumber] = pokemonRecovery;
                break;
            }

            firstHp += 0.5f;
            recoveryHp -= 0.5f;

            MyRemainHp.text = ((int)firstHp).ToString();

            float hpPercent = (float)firstHp / (float)maxHp * 100f;
            slider_Hp.value = (float)firstHp / (float)maxHp;
            
            if (hpPercent >= 40f)
            {
                sprite_Hp.spriteName = "Hp_Green";
            }
            else if (hpPercent >= 20f)
            {
                sprite_Hp.spriteName = "Hp_Yellow";
            }
            else
            {
                sprite_Hp.spriteName = "Hp_Red";
            }
            
  
        }

        BattleManager.Instance.isItemAnimation = false;
    }

    public void ThrowBall()
    {
        StartCoroutine(ThrowBallCorutine());
    }

    IEnumerator ThrowBallCorutine()
    {
        //하드코딩... 승찬이형 죄송해요.. ㅋㅋㅋ 시간관계상 일단 이렇게 해놓고 시간남으면 그때 고치도록 하겠슴돠.. ㅜㅜ 
        MonsterBall.gameObject.SetActive(true);

        MonsterBall.transform.localPosition = new Vector3(-96f, -4f);
        MonsterBall.GetComponent<UISprite>().spriteName = "ball_00";
        yield return new WaitForSeconds(0.03f);

        MonsterBall.transform.localPosition = new Vector3(-83f, 9f);
        MonsterBall.GetComponent<UISprite>().spriteName = "ball_00";
        yield return new WaitForSeconds(0.03f);

        MonsterBall.transform.localPosition = new Vector3(-68f, 22f);
        MonsterBall.GetComponent<UISprite>().spriteName = "ball_01";
        yield return new WaitForSeconds(0.03f);

        MonsterBall.transform.localPosition = new Vector3(-53f, 31f);
        MonsterBall.GetComponent<UISprite>().spriteName = "ball_02";
        yield return new WaitForSeconds(0.03f);

        MonsterBall.transform.localPosition = new Vector3(-43f, 38f);
        MonsterBall.GetComponent<UISprite>().spriteName = "ball_02";
        yield return new WaitForSeconds(0.03f);

        MonsterBall.transform.localPosition = new Vector3(-28f, 53f);
        MonsterBall.GetComponent<UISprite>().spriteName = "ball_03";
        yield return new WaitForSeconds(0.03f);

        MonsterBall.transform.localPosition = new Vector3(-16f, 48f);
        MonsterBall.GetComponent<UISprite>().spriteName = "ball_04";
        yield return new WaitForSeconds(0.03f);

        MonsterBall.transform.localPosition = new Vector3(-1.5f, 50.5f);
        MonsterBall.GetComponent<UISprite>().spriteName = "ball_04";
        yield return new WaitForSeconds(0.03f);

        MonsterBall.transform.localPosition = new Vector3(10f, 46f);
        MonsterBall.GetComponent<UISprite>().spriteName = "ball_05";
        yield return new WaitForSeconds(0.03f);

        MonsterBall.transform.localPosition = new Vector3(21f, 37f);
        MonsterBall.GetComponent<UISprite>().spriteName = "ball_05";
        yield return new WaitForSeconds(0.03f);

        MonsterBall.transform.localPosition = new Vector3(27f, 29f);
        MonsterBall.GetComponent<UISprite>().spriteName = "ball_05";
        yield return new WaitForSeconds(0.03f);

        MonsterBall.transform.localPosition  = new Vector3(36f, 25f);
        MonsterBall.GetComponent<UISprite>().spriteName = "ball_06";
        yield return new WaitForSeconds(0.03f);

        MonsterBall.transform.localPosition = new Vector3(36f, 25f);
        MonsterBall.GetComponent<UISprite>().spriteName = "ball_07";
        yield return new WaitForSeconds(0.03f);

        MonsterBall.transform.localPosition = new Vector3(36f, 25f);
        MonsterBall.GetComponent<UISprite>().spriteName = "ball_08";
        yield return new WaitForSeconds(0.03f);

        MonsterBall.transform.localPosition = new Vector3(36f, 25f);
        MonsterBall.GetComponent<UISprite>().spriteName = "ball_09";
        yield return new WaitForSeconds(1f);

        //포켓몬 작아지고
        while(true)
        {
            yield return new WaitForSeconds(0.05f);
            if(OpPokemon.transform.localScale.x <= 0.1f)
            {
                OpPokemon.gameObject.SetActive(false);
                break;
            }

            OpPokemon.transform.localScale -= new Vector3(0.1f, 0.1f, 0f);
            
        }


        //몬스터볼 닫기
        MonsterBall.transform.localPosition = new Vector3(36f, 25f);
        MonsterBall.GetComponent<UISprite>().spriteName = "ball_10";
        yield return new WaitForSeconds(0.08f);

        MonsterBall.transform.localPosition = new Vector3(36f, 25f);
        MonsterBall.GetComponent<UISprite>().spriteName = "ball_11";
        yield return new WaitForSeconds(0.08f);

        MonsterBall.transform.localPosition = new Vector3(36f, 25f);
        MonsterBall.GetComponent<UISprite>().spriteName = "ball_12";
        yield return new WaitForSeconds(0.08f);

        MonsterBall.transform.localPosition = new Vector3(36f, 25f);
        MonsterBall.GetComponent<UISprite>().spriteName = "ball_13";
        yield return new WaitForSeconds(0.08f);

        MonsterBall.GetComponent<UISprite>().spriteName = "ball_14";
        yield return new WaitForSeconds(0.08f);

        MonsterBall.GetComponent<UISprite>().spriteName = "ball_15";
        yield return new WaitForSeconds(0.08f);


        //몬스터볼 땅에 떨어지기
        MonsterBall.GetComponent<UISprite>().spriteName = "ball_10";
        while (true)
        {
            yield return null;
            if (MonsterBall.transform.localPosition == new Vector3(36f, 11f, 0f))
            {
                break;
            }
            MonsterBall.transform.localPosition = Vector3.MoveTowards(MonsterBall.transform.localPosition, new Vector3(36f, 11f), Time.deltaTime * 80f);
        }

        MonsterBall.GetComponent<UISprite>().spriteName = "ball_08";
        while (true)
        {
            yield return null;
            if (MonsterBall.transform.localPosition == new Vector3(36f, 16f, 0f))
            {
                break;
            }
            MonsterBall.transform.localPosition = Vector3.MoveTowards(MonsterBall.transform.localPosition, new Vector3(36f, 16f), Time.deltaTime * 60f);
        }

        MonsterBall.GetComponent<UISprite>().spriteName = "ball_10";
        while (true)
        {
            yield return null;
            if (MonsterBall.transform.localPosition == new Vector3(36f, 11f, 0f))
            {
                break;
            }
            MonsterBall.transform.localPosition = Vector3.MoveTowards(MonsterBall.transform.localPosition, new Vector3(36f, 11f), Time.deltaTime * 100f);
        }

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(StruggleCorutine());

    }

    IEnumerator StruggleCorutine()
    {
        yield return null;

        // 포켓몬 발버둥
        BattleManager.Instance.isCatchSucces = true;
        for(int i=0; i<3; i++)
        {
            MonsterBall.GetComponent<UISprite>().spriteName = "ball_16";
            yield return new WaitForSeconds(0.1f);

            MonsterBall.GetComponent<UISprite>().spriteName = "ball_18";
            yield return new WaitForSeconds(0.1f);

            MonsterBall.GetComponent<UISprite>().spriteName = "ball_17";
            yield return new WaitForSeconds(0.8f);

            if(Random.Range(0, 10) < 3)
            {
                BattleManager.Instance.isCatchSucces = false;
                break;
            }

        }

        if(!(BattleManager.Instance.isCatchSucces))   // 포획 실패
        {
            MonsterBall.GetComponent<UISprite>().spriteName = "ball_08";
            yield return new WaitForSeconds(0.1f);
            MonsterBall.GetComponent<UISprite>().spriteName = "ball_09";

            //포켓몬 커지고
            OpPokemon.gameObject.SetActive(true);
            while (true)
            {
                yield return new WaitForSeconds(0.05f);
                if (OpPokemon.transform.localScale.x >= 0.59f)
                {
                    break;
                }

                OpPokemon.transform.localScale += new Vector3(0.1f, 0.1f, 0f);
            }
            MonsterBall.gameObject.SetActive(false);
        }
        else    // 포획 성공
        {
            MonsterBall.GetComponent<UISprite>().spriteName = "ball_19";
            yield return new WaitForSeconds(0.1f);
            MonsterBall.GetComponent<UISprite>().spriteName = "ball_20";
        }
        
        BattleManager.Instance.isItemAnimation = false;
    }

    public void DialogPrint(string message)
    {
        Label_Dialog.text = message;
    }

    public void SceneChange()
    {
        StartCoroutine(FadeInCorutine());
    }

    IEnumerator FadeInCorutine()
    {
        Color color = sprite_FadeOut.color;
        float time = 0f;
        color.a = Mathf.Lerp(0f, 1f, time);

        while (color.a < 1f)
        {
            time += Time.deltaTime / 0.5f;

            color.a = Mathf.Lerp(0f, 1f, time);

            sprite_FadeOut.color = color;

            yield return null;
        }

        OptionManager.Instance.WalkBgm();
        SceneManager.LoadScene(1);

    }
}
