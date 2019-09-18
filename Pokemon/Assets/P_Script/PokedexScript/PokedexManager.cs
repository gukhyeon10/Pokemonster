using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.SceneManagement;
using PokemonSpace;

public class PokedexManager : MonoBehaviour {

    private static PokedexManager instance = null;

    Dictionary<string, PokemonSpace.PokedexData> dicPokedexData;

    [SerializeField]
    GameObject pokedexCellPrefab;

    [SerializeField]
    UIGrid pokedexGrid;

    [SerializeField]
    GameObject selectPokemonFront;
    [SerializeField]
    UISprite focusPokemonFront;

    [SerializeField]
    GameObject audioManager;

    [SerializeField]
    GameObject pokedexListPanel;
    [SerializeField]
    GameObject pokedexInfoPanel;

    string selectPokemonNo;
    IEnumerator selectPokemonActionCorutine;


    public static PokedexManager Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
        dicPokedexData = new Dictionary<string, PokemonSpace.PokedexData>();
        PokedexLoad();
        selectPokemonNo = "001";
    }

    void OnDestroy()
    {
        instance = null;    
    }

    void Update()
    {
        PokemonDetail();

    }

    //포켓몬 도감 xml 로드
    void PokedexLoad()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("Pokedex/Pokedex");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNodeList pokeList = xmlDoc.SelectNodes("Pokedex/Pokemon");

        foreach (XmlNode pokeInfo in pokeList)
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

            GameObject pokedexCell = NGUITools.AddChild(pokedexGrid.gameObject, pokedexCellPrefab);
            pokedexCell.transform.localScale = new Vector3(0.26f, 0.26f);
            pokedexCell.GetComponent<PokedexCellScript>().CellSetting(pokeInfo.SelectSingleNode("No").InnerText, pokeInfo.SelectSingleNode("Name").InnerText);


        }

        pokedexGrid.Reposition();
    }

  
    public void SelectPokemon(string pokemonSprite)
    {
        selectPokemonFront.GetComponent<UISprite>().spriteName = pokemonSprite;
        focusPokemonFront.spriteName = pokemonSprite;
        selectPokemonNo = pokemonSprite.Substring(0, 3);
        audioManager.GetComponent<PokedexAudioManager>().PlayAudio(int.Parse(selectPokemonNo.TrimStart('0')));

        //기존에 실행되고있는 코루틴은 중단하고 다시 코루틴을 실행
        if(selectPokemonActionCorutine!=null)
        {
            StopCoroutine(selectPokemonActionCorutine);
        }
        selectPokemonActionCorutine = SelectPokemonAction();
        StartCoroutine(selectPokemonActionCorutine);
    }

    //포켓몬 도감 전체 리스트에서 선택한 포켓몬 짧은 액션
    IEnumerator SelectPokemonAction()
    {

        string pokemonNo = selectPokemonFront.GetComponent<UISprite>().spriteName;
        yield return new WaitForSeconds(0.3f);
        selectPokemonFront.GetComponent<UISprite>().spriteName = pokemonNo.Substring(0,3) + "_1";
        yield return new WaitForSeconds(0.5f);
        selectPokemonFront.GetComponent<UISprite>().spriteName = pokemonNo;

    }

    public void NextPokemon()
    {
        int number = int.Parse(selectPokemonNo.TrimStart('0'));
        number++;
        string nextNo = number.ToString("D3");
        if(dicPokedexData.ContainsKey(nextNo))
        {
            SelectPokemon(nextNo + "_0");
            PokemonSpace.PokedexData data = dicPokedexData[selectPokemonNo];
            pokedexInfoPanel.GetComponent<PokedexInfoDetail>().DetailSet(data.no, data.name, data.kind, data.height, data.weight, data.detail);
        }
    }

    public void PreviousPokemon()
    {
        int number = int.Parse(selectPokemonNo.TrimStart('0'));
        number--;
        string nextNo = number.ToString("D3");
        if (dicPokedexData.ContainsKey(nextNo))
        {
            SelectPokemon(nextNo + "_0");
            PokemonSpace.PokedexData data = dicPokedexData[selectPokemonNo];
            pokedexInfoPanel.GetComponent<PokedexInfoDetail>().DetailSet(data.no, data.name, data.kind, data.height, data.weight, data.detail);
        }
    }


    //포켓몬 정보 패널 활성화/비활성화
    void PokemonDetail()
    {
        if (Input.GetKeyDown(KeyCode.A) && pokedexListPanel.activeSelf == true)
        {   //정보를 볼 포켓몬의 데이터 셋팅
            string pokemonNo = selectPokemonFront.GetComponent<UISprite>().spriteName.Substring(0, 3);
            PokemonSpace.PokedexData data =  dicPokedexData[pokemonNo];
            pokedexInfoPanel.GetComponent<PokedexInfoDetail>().DetailSet(data.no, data.name, data.kind, data.height, data.weight, data.detail);

            pokedexListPanel.SetActive(false);
            pokedexInfoPanel.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.X) && pokedexInfoPanel.activeSelf == true)
        {
            pokedexListPanel.SetActive(true);
            pokedexInfoPanel.SetActive(false);
        }
    }

    public void ClosePokemonDetail()
    {
        pokedexListPanel.SetActive(true);
        pokedexInfoPanel.SetActive(false);
    }

    public void ClosePokedex()
    {
        Debug.Log("도감을 끈다.");
        SceneManager.LoadScene(1);
    }
}
