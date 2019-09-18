using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


public class CharacterScript : MonoBehaviour {
    
    [SerializeField]
    UISprite mSprite;

    [SerializeField]
    UISpriteAnimation animation;

    const int EAST = 1, WEST = 2, SOUTH = 3, NORTH = 4;

    int tileNumber;
    float keyTime = 0f;
    bool isMove = false;
    bool isRun = false;
    bool isSaving = false;
    float moveSpeed = 1f;
    int direct = SOUTH;

    public bool isNpcDialog = false;
    bool isBattle = false;

    int mapWidth;
    void Awake()
    {
        tileNumber = HeroInfoManager.Instance.mapTileLocation;   //초기 타일위치값 안주면 맵전환후 이동 버그 생김 조심하자..
        MapCameraMove.Instance.character = this.transform;
        mapWidth = GameMapDataManager.Instance.width;
        mSprite.depth = tileNumber / mapWidth + 1;
    }

    void Update()
    {
        if(!GameKeyManager.Instance.isSaving)
        {

            if(!GameKeyManager.Instance.isRecovery &&!isNpcDialog && !isBattle)
            {
                    CharacterSpeed();
                    CharacterMove();
                    CharacterDialog();
            }
        }
        else
        {
            CharacterSave();
        }

        if(isBattle && Input.GetKeyDown(KeyCode.B)) // 배틀 체크 임시 해제 키
        {
            isBattle = false;
            GameKeyManager.Instance.isBattle = false;
        }
            
    }

    //세이브 로직과 이동 로직 충돌 방지를 위한 메서드
    void CharacterSave()
    {
        if(!this.isSaving)
        {
            animation.Pause();
            StartCoroutine(SaveCoroutine());
        }
    }

    IEnumerator SaveCoroutine()
    {
        isMove = false;
        GameKeyManager.Instance.isMove = false;
        isSaving = true;
        mSprite.spriteName = "save_0";
        yield return new WaitForSeconds(0.5f);
        mSprite.spriteName = "save_1";
        yield return new WaitForSeconds(1f);

        HeroInfoManager.Instance.SaveHero();             //주인공에 대한 정보 저장
        HeroPokemonManager.Instance.SaveCarryPokemon();  //주인공 포켓몬에 대한 정보 저장
        HeroPokemonManager.Instance.SavePcPokemon();     //주인공의 PC에 있는 포켓몬에 대한 정보 저장

        GameKeyManager.Instance.isSaving = false;
        isSaving = false;
    }

    //기본 이동 로직
    void CharacterMove()
    {
        if (isMove)
        {
            animation.Play();
        }
        else
        {
            if (Input.anyKey == false)
            {
                Stop();
                keyTime = 0;
            }
            else
            {
                Stop();
                keyTime += Time.deltaTime;
                
            }

            if (keyTime < 0.3f)
            {

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    mSprite.spriteName = "man_walk_e_1";
                    direct = EAST;
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    mSprite.spriteName = "man_walk_w_1";
                    direct = WEST;
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    mSprite.spriteName = "man_walk_s_0";
                    direct = SOUTH;
                }
                else if (Input.GetKey(KeyCode.UpArrow))
                {
                    mSprite.spriteName = "man_walk_n_1";
                    direct = NORTH;
                }
                keyTime += Time.deltaTime;

            }
            else
            {
                if(!EnterPortal())
                {
                    if (Input.GetKey(KeyCode.RightArrow) && tileNumber % mapWidth < mapWidth - 1 && GameMap.Instance.dicMovable[tileNumber + 1] == 0)
                    {
                        AnimationSetting("e");
                        Move(EAST);
                    }
                    else if (Input.GetKey(KeyCode.LeftArrow) && tileNumber % mapWidth > 0 && GameMap.Instance.dicMovable[tileNumber - 1] == 0)
                    {
                        AnimationSetting("w");
                        Move(WEST);
                    }
                    else if (Input.GetKey(KeyCode.DownArrow) && tileNumber / mapWidth < GameMapDataManager.Instance.height - 1 && GameMap.Instance.dicMovable[tileNumber + mapWidth] == 0)
                    {
                        AnimationSetting("s");
                        Move(SOUTH);
                    }
                    else if (Input.GetKey(KeyCode.UpArrow) && tileNumber / mapWidth > 0 && GameMap.Instance.dicMovable[tileNumber - mapWidth] == 0)
                    {
                        AnimationSetting("n");
                        Move(NORTH);
                    }
                    else
                    {
                        animation.Pause();
                        animation.frameIndex = 0;
                        keyTime = 0;
                    }
                }

            }
        } 
            
    }

    //캐릭터의 무빙 상태에 따라 속도 설정
    void CharacterSpeed()
    {
        if (GameKeyManager.Instance.isRun)
        {
            moveSpeed = 1.6f;
            animation.framesPerSecond = 6;
            isRun = true;
        }
        else
        {
            moveSpeed = 0.8f;
            animation.framesPerSecond = 5;
            isRun = false;
        }
    }

    void AnimationSetting(string characterDirect)
    {
        if (isRun)
        {
            string animationName = "man_run_" + characterDirect + "_";
            if (animation.namePrefix.Equals(animationName) == false)
            {
                mSprite.spriteName = animationName + "0";
            }

            animation.namePrefix = animationName;

        }
        else
        {
            string animationName = "man_walk_" + characterDirect + "_";
            if (animation.namePrefix.Equals(animationName) == false)
            {
                mSprite.spriteName = animationName + "0";
            }

            animation.namePrefix = animationName;

        }
    }

    void Move(int position)
    {
        direct = position;
        int targetTileNumber;
        switch (position)
        {
            case EAST:
                {
                    targetTileNumber = tileNumber + 1;
                    break;
                }
            case WEST:
                {
                    targetTileNumber = tileNumber - 1;
                    break;
                }
            case SOUTH:
                {
                    targetTileNumber = tileNumber + mapWidth;
                    break;
                }
            case NORTH:
                { 
                    targetTileNumber = tileNumber - mapWidth;
                    mSprite.depth--;
                    break;
                }
            default:
                {
                    targetTileNumber = tileNumber;
                    break;
                }
        }

        if(!isMove)
        {
            StartCoroutine(walkCoroutine(targetTileNumber));
        }

    }

    void Stop()
    {
        if (!Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            switch (direct)
            {
                case EAST:
                    {
                        mSprite.spriteName = "man_walk_e_1";
                        break;
                    }
                case WEST:
                    {
                        mSprite.spriteName = "man_walk_w_1";
                        break;
                    }
                case SOUTH:
                    {
                        mSprite.spriteName = "man_walk_s_0";
                        break;
                    }
                case NORTH:
                    {
                        mSprite.spriteName = "man_walk_n_1";
                        break;
                    }
            }
            animation.Pause();
            animation.frameIndex = 0;
            isMove = false;
            GameKeyManager.Instance.isMove = false;
        }
    }

    //타일에서 타일로 움직이는 코루틴 (무조건 타일 단위로 이동하기 위함)
    IEnumerator walkCoroutine(int targetTileNumber)
    {
        isMove = true;
        GameKeyManager.Instance.isMove = true;
        Vector3 destination = GameMap.Instance.dicTile[targetTileNumber].transform.localPosition;
        //mSprite.depth = targetTileNumber / mapWidth + 1;
        while (true)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, 400f * Time.deltaTime * moveSpeed);
          
            yield return null;

            if(transform.localPosition == destination)
            {
                tileNumber = targetTileNumber;
                isMove = false;
                HeroInfoManager.Instance.mapTileLocation = tileNumber;
                GameKeyManager.Instance.isMove = false;
                mSprite.depth = tileNumber / mapWidth + 1;

                WildBattleCheck(); //타일을 한번 이동할때 풀숲인지 체크하고 풀숲이면 일정확률로 배틀할 수 있도록 체크
                break;
            }
        }

    }

    //이동한 타일에 포탈이 존재할 경우 여부 파악 과 전환할 맵 셋팅
    bool EnterPortal()
    {
        bool isEnterPortal = false;
        string nextMapName = "";
        int portalTileNumber;
        Dictionary<int, string> dicPortal = GameMapDataManager.Instance.dicPortal;

        if (Input.GetKey(KeyCode.RightArrow) && tileNumber % mapWidth < mapWidth - 1 && dicPortal.ContainsKey(tileNumber+1))
        {
            isEnterPortal = true;
            portalTileNumber = tileNumber + 1;
            nextMapName = dicPortal[portalTileNumber];
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && tileNumber % mapWidth > 0 && dicPortal.ContainsKey(tileNumber - 1))
        {
            isEnterPortal = true;
            portalTileNumber = tileNumber - 1;
            nextMapName = dicPortal[portalTileNumber];
        }
        else if (Input.GetKey(KeyCode.DownArrow) && tileNumber / mapWidth < GameMapDataManager.Instance.height - 1 && dicPortal.ContainsKey(tileNumber + mapWidth))
        {
            isEnterPortal = true;
            portalTileNumber = tileNumber + mapWidth;
            nextMapName = dicPortal[portalTileNumber];
        }
        else if (Input.GetKey(KeyCode.UpArrow) && tileNumber / mapWidth > 0 && dicPortal.ContainsKey(tileNumber - mapWidth))
        {
            isEnterPortal = true;
            portalTileNumber = tileNumber - mapWidth;
            nextMapName = dicPortal[portalTileNumber];
        }

        if(isEnterPortal)
        {
            Debug.Log("Enter " + nextMapName);
            GameMapDataManager.Instance.LoadMapData(nextMapName);
            HeroInfoManager.Instance.mapName = nextMapName;
            HeroInfoManager.Instance.mapTileLocation = 0;
            GameMap.Instance.LoadMap(GameMapDataManager.Instance.width, GameMapDataManager.Instance.height);
            return true;
        }
        else
        {
            return false;
        }
        
    }

    void CharacterDialog()
    {
        if(!isMove && Input.GetKeyDown(KeyCode.A))
        {
            switch(direct)
            {
                case EAST:
                    {
                        if(tileNumber%mapWidth < mapWidth -1)
                        {
                            GameMap.Instance.NpcDialog(tileNumber + 1, direct);
                        }
                        break;
                    }
                case WEST:
                    {
                        if(tileNumber%mapWidth > 0)
                        {
                            GameMap.Instance.NpcDialog(tileNumber - 1, direct);
                        }
                        break;
                    }
                case SOUTH:
                    {
                        if(tileNumber / mapWidth < GameMapDataManager.Instance.height - 1)
                        {
                            GameMap.Instance.NpcDialog(tileNumber + mapWidth, direct);
                        }
                        break;
                    }
                case NORTH:
                    {
                        if(tileNumber / mapWidth > 0)
                        {
                            GameMap.Instance.NpcDialog(tileNumber - mapWidth, direct);
                        }
                        break;
                    }
            }
        }
    }

    //배틀 유무 확인
    void WildBattleCheck()
    {
        //먼저 풀숲에 위치해 있는지 체크
        if (GameMap.Instance.dicBush.ContainsKey(tileNumber))
        { 
            if (Random.Range(-10, 11) < -8)   //일정 확률로 야생 포켓몬 출현
            {                               //해당 풀숲에 출현하는 포켓몬 목록 로드  HeroInfoManager에 타일 위치 기록
                                            //전투 씬 전환 후 heroInfoManager의 맵이름과 타일위치 키값을 이용하여 해당 풀숲의 출현 포켓몬 목록 로드
                                            //포켓몬마다 확률 계산 및 해당 포켓몬 등장
                Debug.Log("배틀 시작!");
                isBattle = !isBattle;
                GameKeyManager.Instance.isBattle = isBattle;
                OpponentPokemonManager.Instance.AppearWildPokemon();
                if (isBattle)
                {
                    animation.Pause();
                }

                GameKeyManager.Instance.BattleStart();
            }

        }
    }
}
