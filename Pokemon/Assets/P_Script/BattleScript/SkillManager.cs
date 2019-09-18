using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using PokemonSpace;

public class SkillManager : MonoBehaviour {

    private static SkillManager instance = null;

    const int ME = 0;
    const int OP = 1;

    public Dictionary<int, Skill> dicSkill;

    public static SkillManager Instance
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
            dicSkill = new Dictionary<int, Skill>();
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start () {

        TextAsset textAsset = (TextAsset)Resources.Load("Skill/SkillList");

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNodeList skills = xmlDoc.SelectNodes("List/Skill");

        foreach(XmlNode skill in skills)
        {
            Skill skill_Info = new Skill(int.Parse(skill.SelectSingleNode("No").InnerText),
                                         skill.SelectSingleNode("Name").InnerText,
                                         int.Parse(skill.SelectSingleNode("Type").InnerText),
                                         int.Parse(skill.SelectSingleNode("Class").InnerText),
                                         int.Parse(skill.SelectSingleNode("Power").InnerText),
                                         int.Parse(skill.SelectSingleNode("HitPercent").InnerText),
                                         int.Parse(skill.SelectSingleNode("Pp").InnerText)
                                         );

            dicSkill.Add(skill_Info.no, skill_Info);
            
        }
	}

    //데미지 계산 및 적용
    public bool DemageHandling(int subject, int standPokemonIndex, int skillNo)
    {
        Skill skill = SkillManager.Instance.dicSkill[skillNo];
        if(skill.power != 0)
        {
            switch(subject) 
            {
                case ME:       // 공격자가 주인공
                    {
                        PokemonData standPokemon = HeroPokemonManager.Instance.carryPokemonList[standPokemonIndex];
                        float demage = ((float)skill.power);
                        //demage *= (float)standPokemon.attack;
                        demage *= AbilityRank(ME, (int)AbilityKind.ATTACK);
                        demage *= ((float)standPokemon.level * 2f / 5f) + 2f;
                        //demage /= (float)OpponentPokemonManager.Instance.wildPokemon.defence;
                        demage /= AbilityRank(OP, (int)AbilityKind.DEFENCE);
                        demage /= 50f;
                        demage += 2f;

                        Debug.Log("나의 데미지 : " + demage);

                        OpponentPokemonManager.Instance.wildPokemon.remainHp -= (int)demage;
                        BattleUIManager.Instance.HpChange(OP);

                        break;
                    }
                case OP:       // 공격자가 상대
                    {
                        PokemonData wildPokemon = OpponentPokemonManager.Instance.wildPokemon;
                        float demage = ((float)skill.power);
                        //demage *= (float)wildPokemon.attack;
                        demage *= AbilityRank(OP, (int)AbilityKind.ATTACK);
                        demage *= ((float)wildPokemon.level * 2f / 5f) + 2f;
                        //demage /= (float)OpponentPokemonManager.Instance.wildPokemon.defence;
                        demage /= AbilityRank(ME, (int)AbilityKind.DEFENCE);
                        demage /= 50f;
                        demage += 2f;

                        Debug.Log("상대의 데미지 : "+ demage);

                        PokemonData standPokemon = HeroPokemonManager.Instance.carryPokemonList[standPokemonIndex];
                        standPokemon.remainHp -= (int)demage;
                        if(standPokemon.remainHp < 0)
                        {
                            standPokemon.remainHp = 0;
                        }
                        HeroPokemonManager.Instance.carryPokemonList[standPokemonIndex] = standPokemon;

                        BattleUIManager.Instance.HpChange(ME);
                        break;
                    }
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    //기술 효과
    public string SkillEffect(int subject, int standPokemonIndex, int skillNo)
    {
        Skill skill = SkillManager.Instance.dicSkill[skillNo];

        switch(skill.no)
        {
            case 18:  // 날려버리기
                {
                    if(subject == ME)
                    {
                        BattleManager.Instance.isBattleOver = true;
                        return HeroPokemonManager.Instance.carryPokemonList[standPokemonIndex].name+ "는 상대를 날려버렸다!";
                    }
                    break;
                }
            case 43:  // 째려보기
                {
                    if(subject == ME)
                    {
                        BattleUIManager.Instance.ConditionEffect(OP);
                        DefenceRankDown(OP);
                        return "상대 " +OpponentPokemonManager.Instance.wildPokemon.name + "의 방어력이 감소하였다!";
                    }
                    else
                    {
                        BattleUIManager.Instance.ConditionEffect(ME);
                        DefenceRankDown(ME);
                        return HeroPokemonManager.Instance.carryPokemonList[standPokemonIndex].name + "의 방어력이 감소하였다!";
                    }
                }
            case 45:  // 울음소리
                {
                    if (subject == ME)
                    {
                        BattleUIManager.Instance.ConditionEffect(OP);
                        AttackRankDown(OP);
                        return "상대 " + OpponentPokemonManager.Instance.wildPokemon.name + "의 공격력이 감소하였다!";
                    }
                    else
                    {
                        AttackRankDown(ME);
                        BattleUIManager.Instance.ConditionEffect(ME);
                        return HeroPokemonManager.Instance.carryPokemonList[standPokemonIndex].name + "의 공격력이 감소하였다!";
                    }
                }
            case 52:  // 불꽃세례
                {
                    if (subject == ME)
                    {
                        return "상대 " + OpponentPokemonManager.Instance.wildPokemon.name + "는 화상을 당했다!";
                    }
                    else
                    {
                        return HeroPokemonManager.Instance.carryPokemonList[standPokemonIndex].name + "는 화상을 당했다!";
                    }
                }
            case 60:  // 환상빔
                {
                    if (subject == ME)
                    {
                        return "상대 " + OpponentPokemonManager.Instance.wildPokemon.name + "는 혼란스러운 상태가 되었다!";
                    }
                    else
                    {
                        return HeroPokemonManager.Instance.carryPokemonList[standPokemonIndex].name + "는 혼란스러운 상태가 되었다!";
                    }
                }
            case 71:  // 흡수
                {
                    if (subject == ME)
                    {
                        BattleUIManager.Instance.ConditionEffect(ME);
                        return HeroPokemonManager.Instance.carryPokemonList[standPokemonIndex].name + "는 HP를 흡수하였다!";
                    }
                    else
                    {
                        BattleUIManager.Instance.ConditionEffect(OP);
                        return "상대 " + OpponentPokemonManager.Instance.wildPokemon.name + "는 HP를 흡수하였다!";
                    }
                }
            case 97:  // 고속이동
                {
                    if (subject == ME)
                    {
                        BattleUIManager.Instance.ConditionEffect(ME);
                        return HeroPokemonManager.Instance.carryPokemonList[standPokemonIndex].name + "의 스피드가 상승하였다!";
                    }
                    else
                    {
                        BattleUIManager.Instance.ConditionEffect(OP);
                        return "상대 " + OpponentPokemonManager.Instance.wildPokemon.name + "의 스피드가 상승하였다!";
                    }
                }
            case 106: // 단단해지기
                {
                    if (subject == ME)
                    {
                        DefenceRankUp(ME);
                        BattleUIManager.Instance.ConditionEffect(ME);
                        return HeroPokemonManager.Instance.carryPokemonList[standPokemonIndex].name + "의 방어력이 상승하였다!";
                    }
                    else
                    {
                        DefenceRankUp(OP);
                        BattleUIManager.Instance.ConditionEffect(OP);
                        return "상대 " + OpponentPokemonManager.Instance.wildPokemon.name + "의 방어력이 상승하였다!";
                    }
                }
            default:
                {
                    return null;
                }
        }

        return null;
    }

    void AttackRankUp(int subject)
    {
        switch(subject)
        {
            case ME:
                {
                    if(BattleManager.Instance.myAttackRank<6)
                    {
                        BattleManager.Instance.myAttackRank++;
                    }
                    break;
                }
            case OP:
                {
                    if (BattleManager.Instance.opAttackRank< 6)
                    {
                        BattleManager.Instance.opAttackRank++;
                    }
                    break;
                }
        }
    }

    void DefenceRankUp(int subject)
    {
        switch (subject)
        {
            case ME:
                {
                    if (BattleManager.Instance.myDefenceRank < 6)
                    {
                        BattleManager.Instance.myDefenceRank++;
                    }
                    break;
                }
            case OP:
                {
                    if (BattleManager.Instance.opDefenceRank < 6)
                    {
                        BattleManager.Instance.opDefenceRank++;
                    }
                    break;
                }
        }
    }

    void AttackRankDown(int subject)
    {
        switch (subject)
        {
            case ME:
                {
                    if (BattleManager.Instance.myAttackRank > -6)
                    {
                        BattleManager.Instance.myAttackRank--;
                    }
                    break;
                }
            case OP:
                {
                    if (BattleManager.Instance.opAttackRank > -6)
                    {
                        BattleManager.Instance.opAttackRank--;
                    }
                    break;
                }
        }
    }

    void DefenceRankDown(int subject)
    {
        switch (subject)
        {
            case ME:
                {
                    if (BattleManager.Instance.myAttackRank > -6)
                    {
                        BattleManager.Instance.myAttackRank--;
                    }
                    break;
                }
            case OP:
                {
                    if (BattleManager.Instance.myAttackRank > -6)
                    {
                        BattleManager.Instance.myAttackRank--;
                    }
                    break;
                }
        }
    }

    float AbilityRank(int subject, int kind)
    {
        float ability = 0f;
        int rank = 0;
        switch(subject)
        {
            case ME:
                {
                    switch(kind)
                    {
                        case (int)AbilityKind.ATTACK:
                            {
                                ability = HeroPokemonManager.Instance.carryPokemonList[BattleManager.Instance.standPokemonNumber].attack;
                                rank = BattleManager.Instance.myAttackRank;

                                break;
                            }
                        case (int)AbilityKind.DEFENCE:
                            {
                                ability = HeroPokemonManager.Instance.carryPokemonList[BattleManager.Instance.standPokemonNumber].defence;
                                rank = BattleManager.Instance.myDefenceRank;

                                break;
                            }
                    }
                    break;
                }
            case OP:
                {
                    switch (kind)
                    {
                        case (int)AbilityKind.ATTACK:
                            {
                                ability = OpponentPokemonManager.Instance.wildPokemon.attack;
                                rank = BattleManager.Instance.opAttackRank;
                                break;
                            }
                        case (int)AbilityKind.DEFENCE:
                            {
                                ability = OpponentPokemonManager.Instance.wildPokemon.defence;
                                rank = BattleManager.Instance.opDefenceRank;
                                break;
                            }
                    }
                    break;
                }
        }

        switch(rank)
        {
            case -6:
                {
                    ability *= 0.25f;
                    break;
                }
            case -5:
                {
                    ability *= 0.29f;
                    break;
                }
            case -4:
                {
                    ability *= 0.33f;
                    break;
                }
            case -3:
                {
                    ability *= 0.4f;
                    break;
                }
            case -2:
                {
                    ability *= 0.5f;
                    break;
                }
            case -1:
                {
                    ability *= 0.66f;
                    break;
                }
            case 0:
                {
                    ability *= 1f;
                    break;
                }
            case 1:
                {
                    ability *= 1.5f;
                    break;
                }
            case 2:
                {
                    ability *= 2f;
                    break;
                }
            case 3:
                {
                    ability *= 2.5f;
                    break;
                }
            case 4:
                {
                    ability *= 3f;
                    break;
                }
            case 5:
                {
                    ability *= 3.5f;
                    break;
                }
            case 6:
                {
                    ability *= 4f;
                    break;
                }
        }

        return ability;
    }

   

}
