using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PokemonSpace;

public class CarryPokemonKeyManager : MonoBehaviour {

    [SerializeField]
    UILabel label_Dialog;
    [SerializeField]
    GameObject gb_somethingChoice;
    [SerializeField]
    GameObject gb_pokemonAbility;

    public GameObject[] carryPokemonArray;
    int selectIndex = 0;
    bool isSelected = false;
    bool isSomething = false;
    bool isAbilityCheck = false;
    bool isPlaceChange = false;
    bool isChangeAnimation = false;
    int carryPokemonCount;

	// Use this for initialization
	void Start () {
        carryPokemonCount = HeroPokemonManager.Instance.carryPokemonList.Count;
        if(carryPokemonCount>6)  // 행여나 지니고있는 포켓몬이 6마리 이상인 오류가 발생했을시에 6마리로 조정
        {
            carryPokemonCount = 6;
        }

    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.X))
        {
            CarryPokemonSceneReturn();
        }

        SelectKeyDown();

        SelectPokemonChoiceActive();

        AbilityKeyDown();
	}

    void SelectKeyDown()  // 키보드로 포켓몬 선택창 움직이기
    {
        if( !isSomething && isSelected )
        {
            int changeIndex = selectIndex;
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                changeIndex -= 2; 
                if(changeIndex < 0)
                {
                    changeIndex += 2;
                }
            }
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                changeIndex += 2;
                if (changeIndex >= carryPokemonCount)
                {
                    changeIndex -= 2;
                }
            }
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                changeIndex -= 1;
                if(changeIndex < 0)
                {
                    changeIndex = 0;
                }
            }
            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                changeIndex += 1;
                if(changeIndex >= carryPokemonCount)
                {
                    changeIndex = carryPokemonCount-1;
                }
            }

            carryPokemonArray[selectIndex].GetComponent<CarryPokemonCellScript>().UnSelectedPokemon();

            selectIndex = changeIndex;

            carryPokemonArray[selectIndex].GetComponent<CarryPokemonCellScript>().SelectedPokemon();
        }
    }

    void AbilityKeyDown()
    {
        if (isAbilityCheck)
        {
            int changeIndex = selectIndex;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                changeIndex -= 2;
                if (changeIndex < 0)
                {
                    changeIndex += 2;
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                changeIndex += 2;
                if (changeIndex >= carryPokemonCount)
                {
                    changeIndex -= 2;
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                changeIndex -= 1;
                if (changeIndex < 0)
                {
                    changeIndex = 0;
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                changeIndex += 1;
                if (changeIndex >= carryPokemonCount)
                {
                    changeIndex = carryPokemonCount - 1;
                }
            }
            
            if(selectIndex != changeIndex)
            {
                carryPokemonArray[selectIndex].GetComponent<CarryPokemonCellScript>().UnSelectedPokemon();
                carryPokemonArray[selectIndex].GetComponent<CarryPokemonCellScript>().SelectedPokemonChioceEnable();

                selectIndex = changeIndex;

                carryPokemonArray[selectIndex].GetComponent<CarryPokemonCellScript>().SelectedPokemon();
                carryPokemonArray[selectIndex].GetComponent<CarryPokemonCellScript>().SelectedPokemonChioceActive();

                SelectedPokemonAbility();
            }
        }
    }

    void SelectPokemonChoiceActive()  // 선택한 포켓몬으로 무얼 할지 선택지 활성화 
    {
        if(!isSomething && isSelected && Input.GetKeyDown(KeyCode.A))
        {
            isSomething = true;
            label_Dialog.text = "무엇을 할까요?";
            carryPokemonArray[selectIndex].GetComponent<CarryPokemonCellScript>().SelectedPokemonChioceActive();
            gb_somethingChoice.SetActive(true);
        }
    }

    public void CarryPokemonSceneReturn() // 뒤로가기 버튼
    {
        if(isChangeAnimation)
        {
            return;
        }

        if(isAbilityCheck)
        {
            isAbilityCheck = false;
            isSomething = false;
            gb_pokemonAbility.GetComponent<PokemonAbility>().AbilityEnable();
            carryPokemonArray[selectIndex].GetComponent<CarryPokemonCellScript>().SelectedPokemonChioceEnable();
            label_Dialog.text = "포켓몬을 선택해 주십시오";
        }
        else if(isPlaceChange)
        {
            isPlaceChange = false;
            isSomething = false;
            carryPokemonArray[selectIndex].GetComponent<CarryPokemonCellScript>().SelectedPokemonChioceEnable();
            label_Dialog.text = "포켓몬을 선택해 주십시오";
        }
        else if(isSomething)
        {
            isSomething = false;
            carryPokemonArray[selectIndex].GetComponent<CarryPokemonCellScript>().SelectedPokemonChioceEnable();
            label_Dialog.text = "포켓몬을 선택해 주십시오";
            gb_somethingChoice.SetActive(false);
        }
        else if(isSelected) //선택된 포켓몬 선택 취소
        {
            carryPokemonArray[selectIndex].GetComponent<CarryPokemonCellScript>().UnSelectedPokemon();
            isSelected = false;
        }
        else     //선택된 포켓몬이 아무것도 없는 상태서 취소시 게임씬으로 전환
        {
            SceneManager.LoadScene(1);
        }
    }

    public void ClickPokemonCell(int cellNumber)    //클릭한 포켓몬 활성화,  능력치를 보거나 순서를 바꾸거나 선택지를 보고있는 상황에는 클릭이벤트 막음
    {
        if(!isChangeAnimation)
        {
            if (!isAbilityCheck && !isPlaceChange && isSelected && cellNumber == selectIndex)
            {
                isSomething = true;
                label_Dialog.text = "무엇을 할까요?";
                carryPokemonArray[selectIndex].GetComponent<CarryPokemonCellScript>().SelectedPokemonChioceActive();
                gb_somethingChoice.SetActive(true);
            }
            else if (!isSomething)
            {
                carryPokemonArray[selectIndex].GetComponent<CarryPokemonCellScript>().UnSelectedPokemon();

                selectIndex = cellNumber;
                isSelected = true;

                carryPokemonArray[selectIndex].GetComponent<CarryPokemonCellScript>().SelectedPokemon();

            }
        }

    }

    public void SelectedPokemonAbility()   // 선택한 포켓몬의 능력치 확인
    {
        PokemonData pokemonAbility = HeroPokemonManager.Instance.carryPokemonList[selectIndex];
        gb_pokemonAbility.GetComponent<PokemonAbility>().AbilitySet(pokemonAbility.no,
                                                                    pokemonAbility.name,
                                                                    pokemonAbility.level,
                                                                    pokemonAbility.remainHp,
                                                                    pokemonAbility.maxHp,
                                                                    pokemonAbility.attack,
                                                                    pokemonAbility.defence,
                                                                    pokemonAbility.specialAttack,
                                                                    pokemonAbility.specialDefence,
                                                                    pokemonAbility.speed
                                                                    );
        isAbilityCheck = true;
        gb_somethingChoice.SetActive(false);
        label_Dialog.text = "능력치를 확인합니다.";
    }

    public void SelectedPokemonPlaceChange()
    {
        isPlaceChange = true;
        gb_somethingChoice.SetActive(false);
        label_Dialog.text = "자리를 바꿀 포켓몬을 선택해 주십시오.";
    }

    public void PlaceChangePokemonClick(int changeIndex)   // 바꿀 포켓몬이 선택되어있는 포켓몬과 인덱스가 다르면 교체
    {
        if(!isChangeAnimation && isPlaceChange && selectIndex != changeIndex)
        {
            StartCoroutine(ChangeAnimation(changeIndex));
        }
    }

    IEnumerator ChangeAnimation(int changeIndex)  // 포켓몬을 바꾸는 짧은 효과
    {
        isChangeAnimation = true;
        for(int i=0; i<6; i++)
        {
            carryPokemonArray[selectIndex].transform.localScale /= 2;
            carryPokemonArray[changeIndex].transform.localScale /= 2;
            yield return new WaitForSeconds(0.04f);
        }

        PokemonData selectPokemon = HeroPokemonManager.Instance.carryPokemonList[selectIndex];
        HeroPokemonManager.Instance.carryPokemonList[selectIndex] = HeroPokemonManager.Instance.carryPokemonList[changeIndex];
        HeroPokemonManager.Instance.carryPokemonList[changeIndex] = selectPokemon;
        carryPokemonArray[selectIndex].GetComponent<CarryPokemonCellScript>().InitInfo();
        carryPokemonArray[selectIndex].GetComponent<CarryPokemonCellScript>().HpBarSet();
        carryPokemonArray[changeIndex].GetComponent<CarryPokemonCellScript>().InitInfo();
        carryPokemonArray[changeIndex].GetComponent<CarryPokemonCellScript>().HpBarSet();

        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(0.04f);
            carryPokemonArray[selectIndex].transform.localScale *= 2;
            carryPokemonArray[changeIndex].transform.localScale *= 2;
        }

        isChangeAnimation = false;
        CarryPokemonSceneReturn();
    }
}
