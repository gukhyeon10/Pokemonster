using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PokemonSpace;

public class BattleManager : MonoBehaviour {

    private static BattleManager instance = null;
    public static BattleManager Instance
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

    [SerializeField]
    GameObject UIManager;
    BattleUIManager UI_Manager;


    [SerializeField]
    UISprite MyPokemon;

    const int ME = 0;
    const int OP = 1;

    public int standPokemonNumber = 0;
    public bool isBattleOver = false;
    public bool isBattleAnimation = false; // 전투 중 피가 깎이거나 포켓몬이 움직이는 에니메이션 중인지 체크
    public bool isChangeAnimation = false;
    public bool isItemAnimation = false;
    public bool isCatchSucces = false;
    bool isTurnOver = false;
    int mySkillNo = 0;

    public int myAttackRank = 0;
    public int myDefenceRank = 0;
    public int opAttackRank = 0;
    public int opDefenceRank = 0;


    void Start()
    {
        UI_Manager = UIManager.GetComponent<BattleUIManager>();
        UI_Manager.OpRemainHp = OpponentPokemonManager.Instance.wildPokemon.remainHp;
    }

    public void LaunchPokemon(int number)
    { 
        //해당 인덱스의 포켓몬이 존재하면
        if(HeroPokemonManager.Instance.carryPokemonList.Count>number)
        {
            standPokemonNumber = number;
            MyPokemon.GetComponent<UISprite>().spriteName = HeroPokemonManager.Instance.carryPokemonList[number].no + "_01";
            UI_Manager.DialogPrint("가라! " + HeroPokemonManager.Instance.carryPokemonList[number].name + "!");
            StartCoroutine(AppearMyPokemon(number));
            UI_Manager.SkillActive(standPokemonNumber);
        }
    }

    IEnumerator AppearMyPokemon(int number)
    {
        Vector3 myPokemonDestination = new Vector3(0.3f, MyPokemon.transform.localPosition.y);
        MyPokemon.transform.localPosition = new Vector3(-85f, MyPokemon.transform.localPosition.y);
        MyPokemon.gameObject.SetActive(true);
        while (true)
        {
            MyPokemon.transform.localPosition = Vector3.MoveTowards(MyPokemon.transform.localPosition, myPokemonDestination, 120f * Time.deltaTime);
            yield return null;

            if(MyPokemon.transform.localPosition == myPokemonDestination)
            {
                break;
            }

        }
        yield return null;
        UI_Manager.MyPokemonInfoBar(HeroPokemonManager.Instance.carryPokemonList[number].name, 
                                    HeroPokemonManager.Instance.carryPokemonList[number].level, 
                                    HeroPokemonManager.Instance.carryPokemonList[number].remainHp, 
                                    HeroPokemonManager.Instance.carryPokemonList[number].maxHp);


        isChangeAnimation = false;
        if (!isTurnOver)
        { 
            UI_Manager.DialogPrint(HeroPokemonManager.Instance.carryPokemonList[number].name + "는 무엇을 할까?");
            UI_Manager.ActiveChoicePanel(); // 선택지 활성화
        }
    }

    public void TurnProgress(int skillNo, GameObject SkillPanel)
    {
        SkillPanel.gameObject.SetActive(false);

        mySkillNo = skillNo;
        isBattleOver = false;
        isTurnOver = false;
        if(HeroPokemonManager.Instance.carryPokemonList[standPokemonNumber].speed >= OpponentPokemonManager.Instance.wildPokemon.speed)
        {
            MyTurn();
        }
        else
        {
            OpTurn();
        }
    }

    void MyTurn()
    {
        PokemonData standPokemon = HeroPokemonManager.Instance.carryPokemonList[standPokemonNumber];
        string message = standPokemon.name + "의 " + SkillManager.Instance.dicSkill[mySkillNo].name + "!";
        UI_Manager.DialogPrint(message);
        StartCoroutine(MyAttackProgress(mySkillNo));
    }

    //나의 공격 루틴
    IEnumerator MyAttackProgress(int skillNo)
    {
        while (true)
        {
            yield return null;
            if (!isBattleAnimation && Input.anyKeyDown)
            {
                break;
            }
        }

        UI_Manager.SkillAnimation(ME, skillNo);
        while (true)
        {
            yield return null;
            if(!isBattleAnimation)
            {
                break;
            }
        }

        //데미지 계산
        string message;
        if(SkillManager.Instance.DemageHandling(ME, standPokemonNumber, skillNo))
        {
            message = "상대 " + OpponentPokemonManager.Instance.wildPokemon.name + "는 데미지를 입었다.";
            UI_Manager.DialogPrint(message);

            while (true)
            {
                yield return null;
                if (!isBattleAnimation && Input.anyKeyDown)
                {
                    break;
                }
            }

            if(OpponentPokemonManager.Instance.wildPokemon.remainHp <=0)
            {
                message = "상대 " + OpponentPokemonManager.Instance.wildPokemon.name + "는 쓰러졌다!";
                UI_Manager.DialogPrint(message);
                while (true)
                {
                    yield return null;
                    if (Input.anyKeyDown)
                    {
                        break;
                    }
                }
                isBattleOver = true;
            }

        }

        //기술 효과
        message = SkillManager.Instance.SkillEffect(ME, standPokemonNumber, skillNo);
        if(message != null)
        {
            while (true)
            {
                UI_Manager.DialogPrint(message);

                yield return null;
                if (Input.anyKeyDown)
                {
                    break;
                }
            }
        }

        if(!isBattleOver)  // 턴 계속 진행
        {
            if (isTurnOver)
            {
                UI_Manager.DialogPrint(HeroPokemonManager.Instance.carryPokemonList[standPokemonNumber].name + "는 무엇을 할까?");
                UI_Manager.ActiveChoicePanel();
            }
            else
            {
                isTurnOver = !isTurnOver;
                OpTurn();
            }

        }
        else   // 배틀 종료
        {
            BattleUIManager.Instance.SceneChange();
        }
    }


    void OpTurn()
    {
        int indexSkill=  Random.Range(0, OpponentPokemonManager.Instance.skillCount);

        switch (indexSkill)
        {
            case 0:
                {
                    StartCoroutine(OpAttackProgress(OpponentPokemonManager.Instance.wildPokemon.skill_one));
                    break;
                }
            case 1:
                {
                    StartCoroutine(OpAttackProgress(OpponentPokemonManager.Instance.wildPokemon.skill_two));
                    break;
                }
            case 2:
                {
                    StartCoroutine(OpAttackProgress(OpponentPokemonManager.Instance.wildPokemon.skill_three));
                    break;
                }
            case 3:
                {
                    StartCoroutine(OpAttackProgress(OpponentPokemonManager.Instance.wildPokemon.skill_four));
                    break;
                }
        }
    }

    //상대의 공격 루틴
    IEnumerator OpAttackProgress(int skillNo)
    {
        string message = OpponentPokemonManager.Instance.wildPokemon.name + "의 " + SkillManager.Instance.dicSkill[skillNo].name + "!";
        UI_Manager.DialogPrint(message);
        while (true)
        {
            yield return null;
            if (!isBattleAnimation && Input.anyKeyDown)
            {
                break;
            }
        }

        UI_Manager.SkillAnimation(OP, skillNo);
        while (true)
        {
            yield return null;
            if (!isBattleAnimation)
            {
                break;
            }
        }

        //데미지 계산
        if (SkillManager.Instance.DemageHandling(OP, standPokemonNumber, skillNo))
        {
            message = HeroPokemonManager.Instance.carryPokemonList[standPokemonNumber].name + "는 데미지를 입었다.";
            UI_Manager.DialogPrint(message);

            while (true)
            {
                yield return null;
                if (!isBattleAnimation && Input.anyKeyDown)
                {
                    break;
                }
            }

            if (HeroPokemonManager.Instance.carryPokemonList[standPokemonNumber].remainHp <= 0)
            {
                message = HeroPokemonManager.Instance.carryPokemonList[standPokemonNumber].name+ "는 쓰러졌다!";
                UI_Manager.DialogPrint(message);
                while (true)
                {
                    yield return null;
                    if (Input.anyKeyDown)
                    {
                        break;
                    }
                }
                isBattleOver = true;
            }

        }

        //기술 효과
        message = SkillManager.Instance.SkillEffect(OP, standPokemonNumber, skillNo);
        if (message != null)
        {
            while (true)
            {
                UI_Manager.DialogPrint(message);

                yield return null;
                if (Input.anyKeyDown)
                {
                    break;
                }
            }
        }


        if (!isBattleOver)  //턴 계속 진행
        {
            if (isTurnOver)
            {
                UI_Manager.DialogPrint(HeroPokemonManager.Instance.carryPokemonList[standPokemonNumber].name + "는 무엇을 할까?");
                UI_Manager.ActiveChoicePanel();

            }
            else
            {
                isTurnOver = !isTurnOver;
                MyTurn();
            }
        }
        else // 배틀 종료
        {
            BattleUIManager.Instance.SceneChange();
        }

    }

    public void PokemonChange(GameObject pokemonChangePanel, GameObject hidePanel)
    {
        hidePanel.gameObject.SetActive(false);
        pokemonChangePanel.gameObject.SetActive(false);
        
        Debug.Log(HeroPokemonManager.Instance.carryPokemonList[BattleKeyManager.Instance.changePokemonIndex].name + "으로 교체");

        //포켓몬 교체 애니메이션
        StartCoroutine(PokemonChangeCorutine(BattleKeyManager.Instance.changePokemonIndex));

    }

    IEnumerator PokemonChangeCorutine(int changePokemonIndex)
    {

        //수고했어 돌아와" 다이얼로그
        string message = HeroPokemonManager.Instance.carryPokemonList[standPokemonNumber].name +  " 수고했어! 돌아와!";
        UI_Manager.DialogPrint(message);
        while (true)
        {
            yield return null;
            if (Input.anyKeyDown)
            {
                break;
            }
        }

        //들어오는 애니메이션, 정보창 나가는 애니메이션
        isChangeAnimation = true;
        UI_Manager.OutMyPokemon();
        while (true)
        {
            yield return null;
            if (!isChangeAnimation)
            {
                break;
            }
        }

        //가라 ~~! " 다이얼로그
        standPokemonNumber = changePokemonIndex;
        MyPokemon.spriteName = HeroPokemonManager.Instance.carryPokemonList[standPokemonNumber].no + "_01";
        UI_Manager.SkillActive(standPokemonNumber);
        message = "가라! " +  HeroPokemonManager.Instance.carryPokemonList[standPokemonNumber].name + "!";
        UI_Manager.DialogPrint(message);
        while (true)
        {
            yield return null;
            if (Input.anyKeyDown)
            {
                break;
            }
        }

        isTurnOver = true;  // 교체를 함으로써 턴 끝남
        //나가는 애니메이션, 정보창 들어오는 애니메이션
        isChangeAnimation = true;
        StartCoroutine(AppearMyPokemon(changePokemonIndex));

        while (true)
        {
            yield return null;
            if (!isChangeAnimation)
            {
                break;
            }
        }

        yield return new WaitForSeconds(1f);

        OpTurn();
    }

    public void UseItem(int itemNumber)
    {
        StartCoroutine(UseItemCorutine(itemNumber));
    }

    IEnumerator UseItemCorutine(int itemNumber)
    {
        //아이템 사용 다이얼로그
        string message = "";

        isItemAnimation = false;
        bool isBallThrow = false;
        //무슨 타입의 아이템을 썼는지
        switch(HeroItemManager.Instance.dicItemInfo[itemNumber].type)
        {
            case (int)ItemType.RECOVERY:
                {
                    isItemAnimation = true;
                    message = HeroItemManager.Instance.dicItemInfo[itemNumber].name + "를 사용하였다.";
                    break;
                }
            case (int)ItemType.BALL:
                {
                    message = "주인공은 몬스터볼을 던졌다!";
                    isBallThrow = true;
                    break;
                }
            case (int)ItemType.FRUIT:
                {
                    isItemAnimation = true;
                    message = HeroItemManager.Instance.dicItemInfo[itemNumber].name + "를 사용하였다.";
                    break;
                }
        }
    
        UI_Manager.DialogPrint(message);
        yield return new WaitForSeconds(1f);

        if(isBallThrow)
        {
            StartCoroutine(CatchCorutine());
        }
        else
        {
            BattleUIManager.Instance.HpRecovery(itemNumber);

            while (true)
            {
                yield return null;
                if (!isItemAnimation)
                {
                    break;
                }
            }

            message = HeroPokemonManager.Instance.carryPokemonList[standPokemonNumber].name + "는 체력을 회복하였다!";
            UI_Manager.DialogPrint(message);
            while (true)
            {
                yield return null;
                if (Input.anyKeyDown)
                {
                    break;
                }
            }


            isTurnOver = true;  // 아이템을 사용함으로써 턴 끝남

            yield return new WaitForSeconds(1f);

            OpTurn();
        }

    }

    // 포획 코루틴
    IEnumerator CatchCorutine()
    {
        isItemAnimation = true;
        isCatchSucces = false;
        UI_Manager.ThrowBall();

        while (true)
        {
            yield return null;
            if (!isItemAnimation)
            {
                break;
            }
        }

        string message;

        if(!isCatchSucces)
        {
            message = "아깝다! 조금만 더 하면 됬는데!";
            UI_Manager.DialogPrint(message);
            isTurnOver = true;  // 아이템을 사용함으로써 턴 끝남

            yield return new WaitForSeconds(2f);

            OpTurn();

        }
        else
        {
            message = "포켓몬 포획 성공!";
            UI_Manager.DialogPrint(message);

            bool isCarryPokemon = false;
            isCarryPokemon = HeroPokemonManager.Instance.JoinPokemon(OpponentPokemonManager.Instance.wildPokemon); 

            yield return new WaitForSeconds(2f);

            if (!isCarryPokemon)
            {
                message = OpponentPokemonManager.Instance.wildPokemon.name + "은 포켓몬PC로 전송되었다!";
                UI_Manager.DialogPrint(message);
                yield return new WaitForSeconds(2f);
            }

            BattleUIManager.Instance.SceneChange();
        }

    }

}
