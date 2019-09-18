using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PokemonSpace;
public class GameMap : MonoBehaviour {

    private static GameMap instance = null;

    [SerializeField]
    UIGrid grid_Tile;

    [SerializeField]
    UIPanel table_Object;

    [SerializeField]
    GameObject object_Tile;
    [SerializeField]
    GameObject object_Bush;
    [SerializeField]
    GameObject object_Build;
    [SerializeField]
    GameObject object_Npc;

    [SerializeField]
    GameObject object_Character;
    [SerializeField]
    UIPanel panel_DialogBox;
    [SerializeField]
    UILabel label_DialogBox;

    public Dictionary<int, int> dicMovable;
    public Dictionary<int, GameObject> dicTile;
    Dictionary<int, GameObject> dicBuild;
    Dictionary<int, GameObject> dicNpc;
    public Dictionary<int, GameObject> dicBush;

    GameObject hero;
    string npcGiveItemCode ="0";
    
    public static GameMap Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
        dicMovable = new Dictionary<int, int>();
        dicTile = new Dictionary<int, GameObject>();
        dicBuild = new Dictionary<int, GameObject>();
        dicNpc = new Dictionary<int, GameObject>();
        dicBush = new Dictionary<int, GameObject>();

    }

    void OnDestroy()
    {
        instance = null;    
    }

    void Start()
    {
        if(GameMapDataManager.Instance.isMapLoad)
        {
            LoadMap(GameMapDataManager.Instance.width, GameMapDataManager.Instance.height);
        }
    }

    public void InitMap()
    {
        dicMovable.Clear();
        dicTile.Clear();
        if(grid_Tile.transform.childCount != 0)
        {
            grid_Tile.transform.DestroyChildren();
        }

        dicBush.Clear();
        dicBuild.Clear();
        dicNpc.Clear();
        if(table_Object.transform.childCount != 0)
        {
            table_Object.transform.DestroyChildren();
        }

        grid_Tile.Reposition();

    }

    public void LoadMap(int width, int height)
    {
        InitMap();

        grid_Tile.maxPerLine = width;
        Dictionary<int, MapData> dicMapData = GameMapDataManager.Instance.dicMapData;
        for (int i = 0; i < width * height; i++)
        {
            GameObject tile = NGUITools.AddChild(grid_Tile.gameObject, object_Tile);
            tile.GetComponent<UISprite>().spriteName = dicMapData[i].tileCode;
            tile.GetComponent<GameTileScript>().tileNumber = dicMapData[i].tileNumber;
            tile.transform.localEulerAngles = new Vector3(0, 0, dicMapData[i].tileRotate);
            dicTile.Add(i, tile);

            dicMovable.Add(i, 0);
        }

        grid_Tile.Reposition();

        Dictionary<int, BushData> dicBushData = GameMapDataManager.Instance.dicBushData;
        Dictionary<int, NpcData> dicNpcData = GameMapDataManager.Instance.dicNpcData;
        for (int i = 0; i < width * height; i++)
        {
            Vector3 position = dicTile[i].transform.localPosition;
            if(dicBushData.ContainsKey(i))
            {
                GameObject bush = NGUITools.AddChild(table_Object.gameObject, object_Bush);
                dicBush.Add(i, bush);

                bush.transform.localPosition = position;
                GameBushScript gameBush =  bush.GetComponent<GameBushScript>();
                gameBush.tileNumber = i;
                gameBush.m_Bush.spriteName = dicBushData[i].bushCode;
                gameBush.bushAngle = dicBushData[i].bushRotate;
                gameBush.m_Bush.depth = 0;
                gameBush.SetBushObject();
            }

            if (dicMapData[i].buildCode != null)
            {
                GameObject build = NGUITools.AddChild(table_Object.gameObject, object_Build);
                dicBuild.Add(i, build);

                build.transform.localPosition = position;
                build.GetComponent<GameBuildScript>().tileNumber = i;
                build.GetComponent<GameBuildScript>().m_Build.spriteName = dicMapData[i].buildCode;
                build.GetComponent<GameBuildScript>().m_Build.depth = i / width + 3;
                build.GetComponent<GameBuildScript>().SetBuildObject();
            }

            if (dicMapData[i].npcCode != null)
            {
                GameObject npc = NGUITools.AddChild(table_Object.gameObject, object_Npc);
                dicNpc.Add(i, npc);
                npc.transform.localPosition = position;
                npc.GetComponent<GameNpcScript>().tileNumber = i;
                npc.GetComponent<GameNpcScript>().m_Npc.spriteName = dicMapData[i].npcCode;
                npc.GetComponent<GameNpcScript>().m_Npc.depth = i / width + 1;
                npc.GetComponent<GameNpcScript>().SetNpcObject();

                if (dicNpcData.ContainsKey(i))
                {
                    npc.GetComponent<GameNpcScript>().SetNpcDetail(dicNpcData[i]);
                }
            }

        }
        SetCharater();
        
    }

    void SetCharater()
    {
        GameObject character = NGUITools.AddChild(table_Object.gameObject, object_Character);
        character.transform.localPosition = dicTile[HeroInfoManager.Instance.mapTileLocation].transform.localPosition;  // 해당 타일의 위치에 생성 (캐릭터 스크립트 내의 tileNumber변수또한 awake함수 통해서 초기값 설정)
        character.transform.localScale = new Vector3(1.4f, 1.4f);
        hero = character;

        HeroInfoManager.Instance.NpcCheck();
    }

    public void NpcDialog(int tileNumber, int heroDirect)
    {
        if(dicNpc.ContainsKey(tileNumber))
        {
            
            hero.GetComponent<CharacterScript>().isNpcDialog = true;
            panel_DialogBox.gameObject.SetActive(true);
            GameKeyManager.Instance.isDialog = true;

            GameNpcScript npc = dicNpc[tileNumber].GetComponent<GameNpcScript>();
            npc.LookAtHero(heroDirect);                                                           


            if (npc.m_Npc.spriteName.Substring(0, 6).Equals("NPC_02")) //간호사 NPC인지
            {
                StartCoroutine(NurseNpcDialogCorutine(npc.dialog));
            }
            else if(npc.m_Npc.spriteName.Substring(0, 6).Equals("NPC_03"))  //PC NPC인지
            {
                StartCoroutine(PC_NpcDialogCorutine(npc.dialog));
            }
            else if (HeroInfoManager.Instance.IsNpcPastDialog(tileNumber)) // 한번이라도 대화를 한적 있는지
            {
                StartCoroutine(NpcDialogCorutine(npc.dialog));
            }                                                         // 대화를 건 npc가 배틀npc 인지 (추가해야함)
            else if(!npc.itemNumber.Equals("0"))                    //지니고 있는 아이템이 있는지
            {
                npcGiveItemCode = npc.itemNumber;
                StartCoroutine(NpcDialogCorutine(npc.itemDialog));
            }
            else                                                        //그 외는 그냥 대화 시도
            {
                StartCoroutine(NpcDialogCorutine(npc.dialog));
            }
        }
    }

    IEnumerator NurseNpcDialogCorutine(string dialog)
    {
        yield return null;
        int dialogStart = 0;
        int dialogEnd = dialog.Length;
        int dialogLength = 20;
        if (dialogStart <= dialogEnd - dialogLength)
        {
            label_DialogBox.text = "[000000]" + dialog.Substring(dialogStart, dialogLength) + "[-]";
            dialogStart += dialogLength;
        }
        else
        {
            label_DialogBox.text = "[000000]" + dialog.Substring(dialogStart, dialogEnd - dialogStart) + "[-]";
            dialogStart = dialogEnd;
        }

        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (dialogStart == dialogEnd)
                {
                    break;
                }

                if (dialogStart <= dialogEnd - dialogLength)
                {
                    label_DialogBox.text = "[000000]" + dialog.Substring(dialogStart, dialogLength) + "[-]";
                    dialogStart += dialogLength;
                }
                else
                {
                    label_DialogBox.text = "[000000]" + dialog.Substring(dialogStart, dialogEnd - dialogStart) + "[-]";
                    dialogStart = dialogEnd;
                }
            }
        }
       
        hero.GetComponent<CharacterScript>().isNpcDialog = false;
        panel_DialogBox.gameObject.SetActive(false);
        GameKeyManager.Instance.isDialog = false;

        //포켓몬 치료 루틴
        //페이드 인/아웃 
        GameAudioManager.Instance.RecoveryAudio();
        label_DialogBox.text = "";
    }

    IEnumerator PC_NpcDialogCorutine(string dialog)  // PC Npc와 대화 시도
    {
        yield return null;
        int dialogStart = 0;
        int dialogEnd = dialog.Length;
        int dialogLength = 20;
        if (dialogStart <= dialogEnd - dialogLength)
        {
            label_DialogBox.text = "[000000]" + dialog.Substring(dialogStart, dialogLength) + "[-]";
            dialogStart += dialogLength;
        }
        else
        {
            label_DialogBox.text = "[000000]" + dialog.Substring(dialogStart, dialogEnd - dialogStart) + "[-]";
            dialogStart = dialogEnd;
        }

        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.A))
            {
                    break;
            }
        }

        SceneManager.LoadScene(6);
        
    }

    public void RecoveryEndDialog()
    {
        StartCoroutine(NurseNpcRecoveryEndDialogCorutine());
    }

    IEnumerator NurseNpcRecoveryEndDialogCorutine()
    {
        HeroPokemonManager.Instance.RecoveryPokemon();

        label_DialogBox.text = "[000000]" + "자! 치료가 끝났습니다! ㅎㅎ" + "[-]";

        hero.GetComponent<CharacterScript>().isNpcDialog = true;
        panel_DialogBox.gameObject.SetActive(true);
        GameKeyManager.Instance.isDialog = true;

        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.A))
            {
                break;
            }
        }

        hero.GetComponent<CharacterScript>().isNpcDialog = false;
        panel_DialogBox.gameObject.SetActive(false);
        GameKeyManager.Instance.isDialog = false;
        label_DialogBox.text = "";
    }

    IEnumerator NpcDialogCorutine(string dialog)
    {
        yield return null;
        int dialogStart = 0;
        int dialogEnd = dialog.Length;
        int dialogLength = 20;
        if (dialogStart <= dialogEnd - dialogLength)
        {
            label_DialogBox.text = "[000000]" + dialog.Substring(dialogStart, dialogLength) + "[-]";
            dialogStart += dialogLength;
        }
        else
        {
            label_DialogBox.text = "[000000]" + dialog.Substring(dialogStart, dialogEnd - dialogStart) + "[-]";
            dialogStart = dialogEnd;
        }

        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.A))
            {
                if(dialogStart == dialogEnd)
                {
                    break;
                }

                if (dialogStart <= dialogEnd - dialogLength)
                {
                    label_DialogBox.text = "[000000]" + dialog.Substring(dialogStart, dialogLength) +"[-]";
                    dialogStart += dialogLength;
                }
                else
                {
                    label_DialogBox.text = "[000000]" + dialog.Substring(dialogStart, dialogEnd-dialogStart) + "[-]";
                    dialogStart = dialogEnd;
                }
            }
        }

        if (!npcGiveItemCode.Equals("0"))
        {
            StartCoroutine(NpcGiveItemCorutine());
        }
        else
        {
            hero.GetComponent<CharacterScript>().isNpcDialog = false;
            panel_DialogBox.gameObject.SetActive(false);
            GameKeyManager.Instance.isDialog = false;
            label_DialogBox.text = "";
        }

    }

    IEnumerator NpcGiveItemCorutine()
    {
        yield return null;
        int itemNumberCode = int.Parse(npcGiveItemCode);
        if(HeroItemManager.Instance.dicItemInfo.ContainsKey(itemNumberCode))
        {
            label_DialogBox.text = "[000000]" + HeroInfoManager.Instance.heroName + "은(는) " + "[-]" + "[7401DF]" + HeroItemManager.Instance.dicItemInfo[itemNumberCode].name + "[-]" + "[000000]" + "을 획득하였다!" + "[-]";
            if(HeroItemManager.Instance.dicHeroItem.ContainsKey(itemNumberCode))  // 기존에 해당 아이템을 지니고 있을때 
            {
                HeroItemManager.Instance.dicHeroItem[itemNumberCode]++;
            }
            else  // 해당 아이템이 없고 새로 받을때
            {
                HeroItemManager.Instance.dicHeroItem.Add(itemNumberCode, 1);
            }
        }
        else
        {
            // 등록되지 않은 아이템 코드일 경우 코드넘버를 그대로 출력
            label_DialogBox.text = "[000000]" + HeroInfoManager.Instance.heroName + "은(는) " + "[-]" + "[7401DF]" + npcGiveItemCode + "[-]" + "[000000]" + "을 획득하였다!" + "[-]";
        }

        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.A))
            {
                    break;
            }
        }
        hero.GetComponent<CharacterScript>().isNpcDialog = false;
        panel_DialogBox.gameObject.SetActive(false);
        GameKeyManager.Instance.isDialog = false;
        npcGiveItemCode = "0";
        label_DialogBox.text = "";
    }
}
