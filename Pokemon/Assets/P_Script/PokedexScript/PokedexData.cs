using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Linq;
using PokemonSpace;

// 포켓몬 도감 정보 추가 스크립트
public class PokedexData : MonoBehaviour {

    [SerializeField]
    UISprite sprite_Front;

    [SerializeField]
    UIInput input_Name;
    [SerializeField]
    UIInput input_Kind;
    [SerializeField]
    UIInput input_Height;
    [SerializeField]
    UIInput input_Weight;
    [SerializeField]
    UIInput input_DetailExp;


    Dictionary<string, PokemonSpace.PokedexData> dicPokedexData;
    Dictionary<string, TribeData> dicTribeData;

    void Awake()
    {
        dicPokedexData = new Dictionary<string, PokemonSpace.PokedexData>();
        dicTribeData = new Dictionary<string, TribeData>();
    }

    void Start()
    {
        PokedexLoad();
        TribeValueLoad();
    }


    void PokedexLoad()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(PokemonSpace.FilePath.PokemonDataPath + "Pokedex.xml");

        XmlNodeList pokeList = xmlDoc.SelectNodes("Pokedex/Pokemon");
        
        foreach(XmlNode pokeInfo in pokeList)
        {
            PokemonSpace.PokedexData pokeData = new PokemonSpace.PokedexData();
            pokeData.DataSet(pokeInfo.SelectSingleNode("No").InnerText,
                             pokeInfo.SelectSingleNode("Name").InnerText,
                             pokeInfo.SelectSingleNode("Kind").InnerText,
                             pokeInfo.SelectSingleNode("Height").InnerText,
                             pokeInfo.SelectSingleNode("Weight").InnerText,
                             pokeInfo.SelectSingleNode("Detail").InnerText
                            );

            dicPokedexData.Add(pokeData.no, pokeData);
        }
    }

    void TribeValueLoad()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(PokemonSpace.FilePath.PokemonDataPath + "TribeValue.xml");

        XmlNodeList tirbeValueList = xmlDoc.SelectNodes("TribeValue/Pokemon");

        foreach (XmlNode tribeValue in tirbeValueList)
        {
            TribeData tribeData = new TribeData(tribeValue.SelectSingleNode("No").InnerText,
                                                tribeValue.SelectSingleNode("Name").InnerText,
                                                tribeValue.SelectSingleNode("Hp").InnerText,
                                                tribeValue.SelectSingleNode("Attack").InnerText,
                                                tribeValue.SelectSingleNode("Defence").InnerText,
                                                tribeValue.SelectSingleNode("Sp_Attack").InnerText,
                                                tribeValue.SelectSingleNode("Sp_Defence").InnerText,
                                                tribeValue.SelectSingleNode("Speed").InnerText
                                                );

            dicTribeData.Add(tribeData.no, tribeData);
        }

    }


    public void PokedexDetailLoad(string no)
    {

        if (dicPokedexData.ContainsKey(no))
        {
            sprite_Front.spriteName = no + "_0";
            input_Name.value = dicPokedexData[no].name;
            input_Kind.value = dicPokedexData[no].kind;
            input_Height.value = dicPokedexData[no].height;
            input_Weight.value = dicPokedexData[no].weight;
            input_DetailExp.value = dicPokedexData[no].detail;
        }
        else
        {
            Debug.Log("해당 번호에 대한 정보가 없습니다.");
        }
    }

    public void PokedexUpdate(string no, string name, string kind, string height, string weight, string detail)
    {
        if((no.Length > 0) && (name.Length > 0) && (kind.Length > 0) && (height.Length > 0) && (weight.Length > 0) && (detail.Length > 0))
        {
            PokemonSpace.PokedexData Pokedata = new PokemonSpace.PokedexData();
            Pokedata.DataSet(no, name, kind, height, weight, detail);
            if (dicPokedexData.ContainsKey(no))
            {
                dicPokedexData.Remove(no);
            }
            dicPokedexData.Add(no, Pokedata);


            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));

            XmlNode root = xmlDoc.CreateNode(XmlNodeType.Element, "Pokedex", string.Empty);
            xmlDoc.AppendChild(root);

            foreach (KeyValuePair<string, PokemonSpace.PokedexData> pair in dicPokedexData)
            {
                //여기서 반복문 돌아야함
                XmlNode info = xmlDoc.CreateNode(XmlNodeType.Element, "Pokemon", string.Empty);
                root.AppendChild(info);

                XmlElement infoNo = xmlDoc.CreateElement("No");
                infoNo.InnerText = pair.Value.no;
                info.AppendChild(infoNo);

                XmlElement infoName = xmlDoc.CreateElement("Name");
                infoName.InnerText = pair.Value.name;
                info.AppendChild(infoName);

                XmlElement infoKind = xmlDoc.CreateElement("Kind");
                infoKind.InnerText = pair.Value.kind;
                info.AppendChild(infoKind);

                XmlElement infoHeight = xmlDoc.CreateElement("Height");
                infoHeight.InnerText = pair.Value.height;
                info.AppendChild(infoHeight);

                XmlElement infoWeight = xmlDoc.CreateElement("Weight");
                infoWeight.InnerText = pair.Value.weight;
                info.AppendChild(infoWeight);

                XmlElement infoDetail = xmlDoc.CreateElement("Detail");
                infoDetail.InnerText = pair.Value.detail;
                info.AppendChild(infoDetail);
            }

            xmlDoc.Save("./Assets/Resources/Pokedex/Pokedex.xml");
            Debug.Log("Pokedex Update Success!");


        }
        else
        {
            Debug.Log("정보 입력이 누실되었습니다.");
        }

    }

    public void TribeValueUpdate(string no, string name, string hp, string attack, string defence, string sp_attack, string sp_defence, string speed)
    {

        if(no.Length <= 0)
        {
            return;
        }

        TribeData tribeData = new TribeData(no, name, hp, attack, defence, sp_attack, sp_defence, speed);

        if (dicTribeData.ContainsKey(no))
        {
            dicTribeData.Remove(no);
        }

        dicTribeData.Add(no, tribeData);

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        XmlNode root = xmlDoc.CreateNode(XmlNodeType.Element, "TribeValue", string.Empty);
        xmlDoc.AppendChild(root);

        foreach (KeyValuePair<string, TribeData> pair in dicTribeData)
        {
            //여기서 반복문 돌아야함
            XmlNode tribeValue = xmlDoc.CreateNode(XmlNodeType.Element, "Pokemon", string.Empty);
            root.AppendChild(tribeValue);

            XmlElement noValue = xmlDoc.CreateElement("No");
            noValue.InnerText = pair.Value.no;
            tribeValue.AppendChild(noValue);

            XmlElement nameValue = xmlDoc.CreateElement("Name");
            nameValue.InnerText = pair.Value.name;
            tribeValue.AppendChild(nameValue);

            XmlElement hpValue = xmlDoc.CreateElement("Hp");
            hpValue.InnerText = pair.Value.hp;
            tribeValue.AppendChild(hpValue);

            XmlElement attackValue = xmlDoc.CreateElement("Attack");
            attackValue.InnerText = pair.Value.attack;
            tribeValue.AppendChild(attackValue);

            XmlElement defenceValue = xmlDoc.CreateElement("Defence");
            defenceValue.InnerText = pair.Value.defence;
            tribeValue.AppendChild(defenceValue);

            XmlElement spAttackValue = xmlDoc.CreateElement("Sp_Attack");
            spAttackValue.InnerText = pair.Value.sp_attack;
            tribeValue.AppendChild(spAttackValue);

            XmlElement spDefenceValue = xmlDoc.CreateElement("Sp_Defence");
            spDefenceValue.InnerText = pair.Value.sp_defence;
            tribeValue.AppendChild(spDefenceValue);

            XmlElement speedValue = xmlDoc.CreateElement("Speed");
            speedValue.InnerText = pair.Value.speed;
            tribeValue.AppendChild(speedValue);
        }

        xmlDoc.Save("./Assets/Resources/Pokedex/TribeValue.xml");
        Debug.Log("TribeValue Update Success!");

    }
}
