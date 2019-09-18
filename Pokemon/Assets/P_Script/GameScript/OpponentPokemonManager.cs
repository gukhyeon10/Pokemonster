using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using PokemonSpace;

public class OpponentPokemonManager : MonoBehaviour {

    private static OpponentPokemonManager instance = null;

    public static OpponentPokemonManager Instance
    {
        get
        {
            return instance;
        }
    }

    Dictionary<int, TribeData> dicTribeData;
    public PokemonData wildPokemon = new PokemonData();
    public int skillCount = 0;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            dicTribeData = new Dictionary<int, TribeData>();
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        LoadTribeData();
    }

    void LoadTribeData()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("Pokedex/TribeValue");

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNodeList tribeDataList = xmlDoc.SelectNodes("TribeValue/Pokemon");

        foreach(XmlNode tribeDataNode in tribeDataList)
        {
            TribeData tribeData = new TribeData(tribeDataNode.SelectSingleNode("No").InnerText,
                                                tribeDataNode.SelectSingleNode("Name").InnerText,
                                                tribeDataNode.SelectSingleNode("Hp").InnerText,
                                                tribeDataNode.SelectSingleNode("Attack").InnerText,
                                                tribeDataNode.SelectSingleNode("Defence").InnerText,
                                                tribeDataNode.SelectSingleNode("Sp_Attack").InnerText,
                                                tribeDataNode.SelectSingleNode("Sp_Defence").InnerText,
                                                tribeDataNode.SelectSingleNode("Speed").InnerText);
            dicTribeData.Add(dicTribeData.Count + 1, tribeData);
        }
    }


    public void AppearWildPokemon()
    {
        int pokemonNo = Random.Range(1, 23);
        if (dicTribeData.ContainsKey(pokemonNo))
        {

            TribeData tribeData = dicTribeData[pokemonNo];
            //종족값 셋팅
            int pokemonLevel = Random.Range(4, 11);
            int pokemonHp = int.Parse(tribeData.hp);
            int pokemonAttack = int.Parse(tribeData.attack);
            int pokemonDefence = int.Parse(tribeData.defence);
            int pokemonSpecialAttack = int.Parse(tribeData.sp_attack);
            int pokemonSpecialDefence = int.Parse(tribeData.sp_defence);
            int pokemonSpeed = int.Parse(tribeData.speed);


            wildPokemon.no = pokemonNo.ToString("D3");
            wildPokemon.name = tribeData.name;
            wildPokemon.level = pokemonLevel;
            wildPokemon.maxHp = HpSet(pokemonHp, pokemonLevel);
            wildPokemon.remainHp = wildPokemon.maxHp;
            wildPokemon.attack = AbilitySet(pokemonAttack, pokemonLevel);
            wildPokemon.defence = AbilitySet(pokemonDefence, pokemonLevel);
            wildPokemon.specialAttack = AbilitySet(pokemonSpecialAttack, pokemonLevel);
            wildPokemon.specialDefence = AbilitySet(pokemonSpecialDefence, pokemonLevel);
            wildPokemon.speed = AbilitySet(pokemonSpeed, pokemonLevel);

            WildPokemonSkillSet();

            Debug.Log(wildPokemon.no + ") " +wildPokemon.name + "이 나타났다!");
            
            if(wildPokemon.skill_one != 0)
            {
                Debug.Log(SkillManager.Instance.dicSkill[wildPokemon.skill_one].name);
            }
            if (wildPokemon.skill_two != 0)
            {
                Debug.Log(SkillManager.Instance.dicSkill[wildPokemon.skill_two].name);
            }
            if (wildPokemon.skill_three != 0)
            {
                Debug.Log(SkillManager.Instance.dicSkill[wildPokemon.skill_three].name);
            }
            if (wildPokemon.skill_four != 0)
            {
                Debug.Log(SkillManager.Instance.dicSkill[wildPokemon.skill_four].name);
            }
        }

    }
    
    int HpSet(int hp, int level)
    {
        float f_Hp = (float)hp;
        f_Hp *= 2f;  // 종족값 x2
        f_Hp += 20f;  // + 개체값  20이라고 임의로 지정
        f_Hp *= ((float)level / 100f);
        f_Hp = f_Hp + 10 + level;
        return (int)f_Hp;
    }

    int AbilitySet(int ability, int level)
    {
        float f_Ability = (float)ability;
        f_Ability *= 2f;
        f_Ability += 20f;
        f_Ability *= ((float)level / 100f);
        f_Ability += 5f;

        return (int)f_Ability;
    }

    void WildPokemonSkillSet()
    {
        wildPokemon.skill_one = 0;
        wildPokemon.skill_two = 0;
        wildPokemon.skill_three = 0;
        wildPokemon.skill_four = 0;

        TextAsset textAsset = (TextAsset)Resources.Load("Skill/"+ wildPokemon.no + "_SKILL");

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNodeList skill_List = xmlDoc.SelectNodes("PossibleList/Skill");

        int indexUpperLimit = 0; //해당 포켓몬의 레벨에 따른 스킬 리스트의 인덱스 상한선

        for (int i=skill_List.Count-1; i>0; i--)
        {
            if (int.Parse(skill_List[i].SelectSingleNode("Level").InnerText) <= wildPokemon.level)
            {
                indexUpperLimit = i;
                break;
            }

        }

        skillCount = 0;

        for(int i =0; i<4; i++)
        {
            int indexSkill = Random.Range(0, indexUpperLimit + 1);
            int skillNo = int.Parse(skill_List[indexSkill].SelectSingleNode("No").InnerText);
            if(wildPokemon.skill_one !=skillNo && wildPokemon.skill_two != skillNo && wildPokemon.skill_three != skillNo && wildPokemon.skill_four != skillNo)
            {
                switch(skillCount)
                {
                    case 0:
                        {
                            wildPokemon.skill_one = skillNo;
                            skillCount++;
                            break;
                        }
                    case 1:
                        {
                            wildPokemon.skill_two = skillNo;
                            skillCount++;
                            break;
                        }
                    case 2:
                        {
                            wildPokemon.skill_three = skillNo;
                            skillCount++;
                            break;
                        }
                    case 3:
                        {
                            wildPokemon.skill_four = skillNo;
                            skillCount++;
                            break;
                        }
                }
            }
            

        }


    }

}
