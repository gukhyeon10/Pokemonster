using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using PokemonSpace;

public class HeroPokemonManager : MonoBehaviour {

    private static HeroPokemonManager instance = null;

    public List<PokemonData> carryPokemonList = new List<PokemonData>();

    public List<PokemonData> pcPokemonList = new List<PokemonData>();
    
    public static HeroPokemonManager Instance
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
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        LoadCarryPokemon();
        LoadPcPokemon();
	}
    
    void LoadCarryPokemon()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Application.streamingAssetsPath + "/Hero/CarryPokemonFile.xml");

        XmlNodeList carryPokemonNodeList = xmlDoc.SelectNodes("CarryPokemon/Pokemon");

        foreach(XmlNode carryPokemon in carryPokemonNodeList)
        {

            PokemonData pokemon = new PokemonData(carryPokemon.SelectSingleNode("No").InnerText,
                                                          carryPokemon.SelectSingleNode("Name").InnerText,
                                                          int.Parse(carryPokemon.SelectSingleNode("Level").InnerText),
                                                          int.Parse(carryPokemon.SelectSingleNode("Attack").InnerText),
                                                          int.Parse(carryPokemon.SelectSingleNode("Defence").InnerText),
                                                          int.Parse(carryPokemon.SelectSingleNode("SpecialAttack").InnerText),
                                                          int.Parse(carryPokemon.SelectSingleNode("SpecialDefence").InnerText),
                                                          int.Parse(carryPokemon.SelectSingleNode("Speed").InnerText),
                                                          int.Parse(carryPokemon.SelectSingleNode("RemainHp").InnerText),
                                                          int.Parse(carryPokemon.SelectSingleNode("MaxHp").InnerText)
                                                         );

            pokemon.skill_one = int.Parse(carryPokemon.SelectSingleNode("SkillOne").InnerText);
            pokemon.skill_one_pp = int.Parse(carryPokemon.SelectSingleNode("SkillOnePp").InnerText);
            pokemon.skill_two = int.Parse(carryPokemon.SelectSingleNode("SkillTwo").InnerText);
            pokemon.skill_two_pp = int.Parse(carryPokemon.SelectSingleNode("SkillTwoPp").InnerText);
            pokemon.skill_three = int.Parse(carryPokemon.SelectSingleNode("SkillThree").InnerText);
            pokemon.skill_three_pp = int.Parse(carryPokemon.SelectSingleNode("SkillThreePp").InnerText);
            pokemon.skill_four = int.Parse(carryPokemon.SelectSingleNode("SkillFour").InnerText);
            pokemon.skill_four_pp = int.Parse(carryPokemon.SelectSingleNode("SkillFourPp").InnerText);


            carryPokemonList.Add(pokemon);
        }
        
    }

    void LoadPcPokemon()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Application.streamingAssetsPath + "/Hero/PcPokemonFile.xml");

        XmlNodeList pcPokemonNodeList = xmlDoc.SelectNodes("PcPokemon/Pokemon");

        foreach (XmlNode pcPokemon in pcPokemonNodeList)
        {

            PokemonData pokemon = new PokemonData(pcPokemon.SelectSingleNode("No").InnerText,
                                                          pcPokemon.SelectSingleNode("Name").InnerText,
                                                          int.Parse(pcPokemon.SelectSingleNode("Level").InnerText),
                                                          int.Parse(pcPokemon.SelectSingleNode("Attack").InnerText),
                                                          int.Parse(pcPokemon.SelectSingleNode("Defence").InnerText),
                                                          int.Parse(pcPokemon.SelectSingleNode("SpecialAttack").InnerText),
                                                          int.Parse(pcPokemon.SelectSingleNode("SpecialDefence").InnerText),
                                                          int.Parse(pcPokemon.SelectSingleNode("Speed").InnerText),
                                                          int.Parse(pcPokemon.SelectSingleNode("RemainHp").InnerText),
                                                          int.Parse(pcPokemon.SelectSingleNode("MaxHp").InnerText)
                                                         );

            pokemon.skill_one = int.Parse(pcPokemon.SelectSingleNode("SkillOne").InnerText);
            pokemon.skill_one_pp = int.Parse(pcPokemon.SelectSingleNode("SkillOnePp").InnerText);
            pokemon.skill_two = int.Parse(pcPokemon.SelectSingleNode("SkillTwo").InnerText);
            pokemon.skill_two_pp = int.Parse(pcPokemon.SelectSingleNode("SkillTwoPp").InnerText);
            pokemon.skill_three = int.Parse(pcPokemon.SelectSingleNode("SkillThree").InnerText);
            pokemon.skill_three_pp = int.Parse(pcPokemon.SelectSingleNode("SkillThreePp").InnerText);
            pokemon.skill_four = int.Parse(pcPokemon.SelectSingleNode("SkillFour").InnerText);
            pokemon.skill_four_pp = int.Parse(pcPokemon.SelectSingleNode("SkillFourPp").InnerText);


            pcPokemonList.Add(pokemon);
        }

        Debug.Log("PC 포켓몬 로드 성공");
    }

    public void SaveCarryPokemon()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        XmlNode root = xmlDoc.CreateNode(XmlNodeType.Element, "CarryPokemon", string.Empty);
        xmlDoc.AppendChild(root);

        for(int i = 0; i< carryPokemonList.Count; i++)
        {
            XmlNode pokemon = xmlDoc.CreateNode(XmlNodeType.Element, "Pokemon", string.Empty);
            root.AppendChild(pokemon);

            XmlElement no = xmlDoc.CreateElement("No");
            no.InnerText = carryPokemonList[i].no;
            pokemon.AppendChild(no);

            XmlElement name = xmlDoc.CreateElement("Name");
            name.InnerText = carryPokemonList[i].name;
            pokemon.AppendChild(name);

            XmlElement level = xmlDoc.CreateElement("Level");
            level.InnerText = carryPokemonList[i].level.ToString();
            pokemon.AppendChild(level);

            XmlElement attack = xmlDoc.CreateElement("Attack");
            attack.InnerText = carryPokemonList[i].attack.ToString();
            pokemon.AppendChild(attack);

            XmlElement defence = xmlDoc.CreateElement("Defence");
            defence.InnerText = carryPokemonList[i].defence.ToString();
            pokemon.AppendChild(defence);

            XmlElement specialAttack = xmlDoc.CreateElement("SpecialAttack");
            specialAttack.InnerText = carryPokemonList[i].specialAttack.ToString();
            pokemon.AppendChild(specialAttack);

            XmlElement specialDefence = xmlDoc.CreateElement("SpecialDefence");
            specialDefence.InnerText = carryPokemonList[i].specialDefence.ToString();
            pokemon.AppendChild(specialDefence);

            XmlElement speed = xmlDoc.CreateElement("Speed");
            speed.InnerText = carryPokemonList[i].speed.ToString();
            pokemon.AppendChild(speed);

            XmlElement remainHp = xmlDoc.CreateElement("RemainHp");
            remainHp.InnerText = carryPokemonList[i].remainHp.ToString();
            pokemon.AppendChild(remainHp);

            XmlElement maxHp = xmlDoc.CreateElement("MaxHp");
            maxHp.InnerText = carryPokemonList[i].maxHp.ToString();
            pokemon.AppendChild(maxHp);

            XmlElement skill_One = xmlDoc.CreateElement("SkillOne");
            skill_One.InnerText = carryPokemonList[i].skill_one.ToString();
            pokemon.AppendChild(skill_One);

            XmlElement skill_One_Pp = xmlDoc.CreateElement("SkillOnePp");
            skill_One_Pp.InnerText = carryPokemonList[i].skill_one_pp.ToString();
            pokemon.AppendChild(skill_One_Pp);

            XmlElement skill_Two = xmlDoc.CreateElement("SkillTwo");
            skill_Two.InnerText = carryPokemonList[i].skill_two.ToString();
            pokemon.AppendChild(skill_Two);

            XmlElement skill_Two_Pp = xmlDoc.CreateElement("SkillTwoPp");
            skill_Two_Pp.InnerText = carryPokemonList[i].skill_two_pp.ToString();
            pokemon.AppendChild(skill_Two_Pp);

            XmlElement skill_Three = xmlDoc.CreateElement("SkillThree");
            skill_Three.InnerText = carryPokemonList[i].skill_three.ToString();
            pokemon.AppendChild(skill_Three);

            XmlElement skill_Three_Pp = xmlDoc.CreateElement("SkillThreePp");
            skill_Three_Pp.InnerText = carryPokemonList[i].skill_three_pp.ToString();
            pokemon.AppendChild(skill_Three_Pp);

            XmlElement skill_Four = xmlDoc.CreateElement("SkillFour");
            skill_Four.InnerText = carryPokemonList[i].skill_four.ToString();
            pokemon.AppendChild(skill_Four);

            XmlElement skill_Four_Pp = xmlDoc.CreateElement("SkillFourPp");
            skill_Four_Pp.InnerText = carryPokemonList[i].skill_four_pp.ToString();
            pokemon.AppendChild(skill_Four_Pp);

            xmlDoc.Save(Application.streamingAssetsPath + "/Hero/CarryPokemonFile.xml");
        
        }
        Debug.Log("CarryPokemon Save End!");
    }

    public void SavePcPokemon()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        XmlNode root = xmlDoc.CreateNode(XmlNodeType.Element, "PcPokemon", string.Empty);
        xmlDoc.AppendChild(root);

        Debug.Log(pcPokemonList.Count);
        for (int i = 0; i < pcPokemonList.Count; i++)
        {
            XmlNode pokemon = xmlDoc.CreateNode(XmlNodeType.Element, "Pokemon", string.Empty);
            root.AppendChild(pokemon);

            XmlElement no = xmlDoc.CreateElement("No");
            no.InnerText = pcPokemonList[i].no;
            pokemon.AppendChild(no);

            XmlElement name = xmlDoc.CreateElement("Name");
            name.InnerText = pcPokemonList[i].name;
            pokemon.AppendChild(name);

            XmlElement level = xmlDoc.CreateElement("Level");
            level.InnerText = pcPokemonList[i].level.ToString();
            pokemon.AppendChild(level);

            XmlElement attack = xmlDoc.CreateElement("Attack");
            attack.InnerText = pcPokemonList[i].attack.ToString();
            pokemon.AppendChild(attack);

            XmlElement defence = xmlDoc.CreateElement("Defence");
            defence.InnerText = pcPokemonList[i].defence.ToString();
            pokemon.AppendChild(defence);

            XmlElement specialAttack = xmlDoc.CreateElement("SpecialAttack");
            specialAttack.InnerText = pcPokemonList[i].specialAttack.ToString();
            pokemon.AppendChild(specialAttack);

            XmlElement specialDefence = xmlDoc.CreateElement("SpecialDefence");
            specialDefence.InnerText = pcPokemonList[i].specialDefence.ToString();
            pokemon.AppendChild(specialDefence);

            XmlElement speed = xmlDoc.CreateElement("Speed");
            speed.InnerText = pcPokemonList[i].speed.ToString();
            pokemon.AppendChild(speed);

            XmlElement remainHp = xmlDoc.CreateElement("RemainHp");
            remainHp.InnerText = pcPokemonList[i].remainHp.ToString();
            pokemon.AppendChild(remainHp);

            XmlElement maxHp = xmlDoc.CreateElement("MaxHp");
            maxHp.InnerText = pcPokemonList[i].maxHp.ToString();
            pokemon.AppendChild(maxHp);

            XmlElement skill_One = xmlDoc.CreateElement("SkillOne");
            skill_One.InnerText = pcPokemonList[i].skill_one.ToString();
            pokemon.AppendChild(skill_One);

            XmlElement skill_One_Pp = xmlDoc.CreateElement("SkillOnePp");
            skill_One_Pp.InnerText = pcPokemonList[i].skill_one_pp.ToString();
            pokemon.AppendChild(skill_One_Pp);

            XmlElement skill_Two = xmlDoc.CreateElement("SkillTwo");
            skill_Two.InnerText = pcPokemonList[i].skill_two.ToString();
            pokemon.AppendChild(skill_Two);

            XmlElement skill_Two_Pp = xmlDoc.CreateElement("SkillTwoPp");
            skill_Two_Pp.InnerText = pcPokemonList[i].skill_two_pp.ToString();
            pokemon.AppendChild(skill_Two_Pp);

            XmlElement skill_Three = xmlDoc.CreateElement("SkillThree");
            skill_Three.InnerText = pcPokemonList[i].skill_three.ToString();
            pokemon.AppendChild(skill_Three);

            XmlElement skill_Three_Pp = xmlDoc.CreateElement("SkillThreePp");
            skill_Three_Pp.InnerText = pcPokemonList[i].skill_three_pp.ToString();
            pokemon.AppendChild(skill_Three_Pp);

            XmlElement skill_Four = xmlDoc.CreateElement("SkillFour");
            skill_Four.InnerText = pcPokemonList[i].skill_four.ToString();
            pokemon.AppendChild(skill_Four);

            XmlElement skill_Four_Pp = xmlDoc.CreateElement("SkillFourPp");
            skill_Four_Pp.InnerText = pcPokemonList[i].skill_four_pp.ToString();
            pokemon.AppendChild(skill_Four_Pp);

            xmlDoc.Save(Application.streamingAssetsPath + "/Hero/PcPokemonFile.xml");

        }
        Debug.Log("PcPokemon Save End!");
    }

    public void RecoveryPokemon()
    {
        for(int i=0; i< carryPokemonList.Count; i++)
        {
            PokemonData pokemon = carryPokemonList[i];
            pokemon.remainHp = pokemon.maxHp;

            carryPokemonList[i] = pokemon;
        }
    }

    public bool JoinPokemon(PokemonData newPokemon)
    {
        if(carryPokemonList.Count <6) // 6마리 미만을 소지했을때
        {
            if(newPokemon.skill_one != 0)
            {
                newPokemon.skill_one_pp = SkillManager.Instance.dicSkill[newPokemon.skill_one].pp;
            }
            if(newPokemon.skill_two != 0)
            {
                newPokemon.skill_two_pp = SkillManager.Instance.dicSkill[newPokemon.skill_two].pp;
            }
            if(newPokemon.skill_three != 0)
            {
                newPokemon.skill_three_pp = SkillManager.Instance.dicSkill[newPokemon.skill_three].pp;
            }
            if(newPokemon.skill_four != 0)
            {
                newPokemon.skill_four_pp = SkillManager.Instance.dicSkill[newPokemon.skill_four].pp;
            }

            carryPokemonList.Add(newPokemon);  // 포켓몬 합류

            return true;
        }
        else
        {
            pcPokemonList.Add(newPokemon);
            return false;
        }
    }

}
