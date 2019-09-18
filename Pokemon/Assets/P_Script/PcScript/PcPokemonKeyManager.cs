using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PokemonSpace;

public class PcPokemonKeyManager : MonoBehaviour {

    public int pointPokemonIndex = 0;
    public int selectPokemonIndex = 0;

    [SerializeField]
    GameObject PcManager;
    [SerializeField]
    UIGrid gridPc;

    [SerializeField]
    GameObject Pokemon_1;
    [SerializeField]
    GameObject Pokemon_2;
    [SerializeField]
    GameObject Pokemon_3;
    [SerializeField]
    GameObject Pokemon_4;
    [SerializeField]
    GameObject Pokemon_5;
    [SerializeField]
    GameObject Pokemon_6;

    GameObject SelectPokemonCell = null;

    [SerializeField]
    GameObject PcPokemonChoicePanel;
    [SerializeField]
    GameObject CarryPokemonChoicePanel;
    [SerializeField]
    GameObject PcPokemonAbilityPanel;
    [SerializeField]
    GameObject PcPokemonSkillPanel;
    [SerializeField]
    GameObject CarryPokemonSkillPanel;
    [SerializeField]
    GameObject CarryPokemonAbilityPanel;

    [SerializeField]
    UILabel label_Dialog;

    // Use this for initialization
    void Start () {
        if(HeroPokemonManager.Instance.pcPokemonList.Count > 0)
        {
            gridPc.GetChild(pointPokemonIndex).GetChild(0).transform.localPosition += (Vector3.up * 15);
            gridPc.GetChild(pointPokemonIndex).GetComponent<PcPokemon>().PokemonPointerAble();
        }
    }

    void Update()
    {
        if (HeroPokemonManager.Instance.pcPokemonList.Count > 0)
        {
            PointMove();
            ActivePcPokemonChoicePanel();
            InputKeyCarryPokemonChoicePanel();
        }
    }

    void PointMove()
    {
        if(!(PcPokemonChoicePanel.gameObject.activeSelf) && !(PcPokemonAbilityPanel.gameObject.activeSelf) && !(PcPokemonSkillPanel.gameObject.activeSelf))
        {
            if (!(CarryPokemonAbilityPanel.gameObject.activeSelf) && !(CarryPokemonSkillPanel.gameObject.activeSelf))
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) && pointPokemonIndex - 6 >= 0 && HeroPokemonManager.Instance.pcPokemonList.Count > pointPokemonIndex - 6)
                {
                    gridPc.GetChild(pointPokemonIndex).GetComponent<PcPokemon>().PokemonPointerEnable();
                    gridPc.GetChild(pointPokemonIndex).GetChild(0).transform.localPosition += (Vector3.down * 15);
                    pointPokemonIndex -= 6;
                    gridPc.GetChild(pointPokemonIndex).GetChild(0).transform.localPosition += (Vector3.up * 15);
                    gridPc.GetChild(pointPokemonIndex).GetComponent<PcPokemon>().PokemonPointerAble();
                }
                if (Input.GetKeyDown(KeyCode.DownArrow) && HeroPokemonManager.Instance.pcPokemonList.Count > pointPokemonIndex + 6)
                {
                    gridPc.GetChild(pointPokemonIndex).GetComponent<PcPokemon>().PokemonPointerEnable();
                    gridPc.GetChild(pointPokemonIndex).GetChild(0).transform.localPosition += (Vector3.down * 15);
                    pointPokemonIndex += 6;
                    gridPc.GetChild(pointPokemonIndex).GetChild(0).transform.localPosition += (Vector3.up * 15);
                    gridPc.GetChild(pointPokemonIndex).GetComponent<PcPokemon>().PokemonPointerAble();
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow) && pointPokemonIndex - 1 >= 0)
                {
                    gridPc.GetChild(pointPokemonIndex).GetComponent<PcPokemon>().PokemonPointerEnable();
                    gridPc.GetChild(pointPokemonIndex).GetChild(0).transform.localPosition += (Vector3.down * 15);
                    pointPokemonIndex--;
                    gridPc.GetChild(pointPokemonIndex).GetChild(0).transform.localPosition += (Vector3.up * 15);
                    gridPc.GetChild(pointPokemonIndex).GetComponent<PcPokemon>().PokemonPointerAble();
                }
                if (Input.GetKeyDown(KeyCode.RightArrow) && pointPokemonIndex + 1 < HeroPokemonManager.Instance.pcPokemonList.Count)
                {
                    gridPc.GetChild(pointPokemonIndex).GetComponent<PcPokemon>().PokemonPointerEnable();
                    gridPc.GetChild(pointPokemonIndex).GetChild(0).transform.localPosition += (Vector3.down * 15);
                    pointPokemonIndex++;
                    gridPc.GetChild(pointPokemonIndex).GetChild(0).transform.localPosition += (Vector3.up * 15);
                    gridPc.GetChild(pointPokemonIndex).GetComponent<PcPokemon>().PokemonPointerAble();
                }
            }
        }
    }

    

    public void PointPokemonBring()   // 포켓몬 데려오기
    {
            if (HeroPokemonManager.Instance.carryPokemonList.Count < 6)
            {

                // 데려오다
                HeroPokemonManager.Instance.carryPokemonList.Add(HeroPokemonManager.Instance.pcPokemonList[pointPokemonIndex]);

                //PC 포켓몬 리스트 삭제
                HeroPokemonManager.Instance.pcPokemonList.RemoveAt(pointPokemonIndex);

                //PC 포켓몬 재정렬
                PcManager.GetComponent<PcPokemonGrid>().PcPokemonReposition();

                //재정렬
                RepositionCarryPokemon();

                if(pointPokemonIndex >= HeroPokemonManager.Instance.pcPokemonList.Count)
                {
                    pointPokemonIndex = HeroPokemonManager.Instance.pcPokemonList.Count - 1;
                }
                else if(pointPokemonIndex < 0)
                {
                    pointPokemonIndex = 0;
                }
                
                if(HeroPokemonManager.Instance.pcPokemonList.Count > 0)
                {
                    gridPc.GetChild(pointPokemonIndex).GetChild(0).transform.localPosition += (Vector3.up * 15);
                    gridPc.GetChild(pointPokemonIndex).GetComponent<PcPokemon>().PokemonPointerAble();
                }

                label_Dialog.text = "[000000]포켓몬을 선택해 주십시오[-]";
            }
        else
        {
            label_Dialog.text = "[000000]포켓몬은 6마리까지 소지 가능합니다[-]";
        }
    }

    void ActivePcPokemonChoicePanel()
    {
        if(!(CarryPokemonChoicePanel.gameObject.activeSelf))
        {
            if (Input.GetKeyDown(KeyCode.A) && !(PcPokemonChoicePanel.gameObject.activeSelf) && !(PcPokemonAbilityPanel.gameObject.activeSelf) && !(PcPokemonSkillPanel.gameObject.activeSelf))
            {
                if (!(CarryPokemonAbilityPanel.gameObject.activeSelf) && !(CarryPokemonSkillPanel.gameObject.activeSelf))
                {
                    PcPokemonChoicePanel.gameObject.SetActive(true);
                    label_Dialog.text = "[000000]" + HeroPokemonManager.Instance.pcPokemonList[pointPokemonIndex].name + "을 어떻게 할까요?[-]";
                }
                
            }
            if (Input.GetKeyDown(KeyCode.X) && PcPokemonChoicePanel.gameObject.activeSelf)
            {
                PcPokemonChoicePanel.gameObject.SetActive(false);
                label_Dialog.text = "[000000]포켓몬을 선택해 주십시오[-]";
            }
        }
    }

    void InputKeyCarryPokemonChoicePanel()
    {
        if (Input.GetKeyDown(KeyCode.X) && CarryPokemonChoicePanel.gameObject.activeSelf)
        {
            UnActiveCarryPokemonChoicePanel();
        }   
    }

    public void UnActiveCarryPokemonChoicePanel()
    {
        CarryPokemonChoicePanel.gameObject.SetActive(false);
        label_Dialog.text = "[000000]포켓몬을 선택해 주십시오[-]";
        Pokemon_1.GetComponent<PcPokemonCellScript>().UnSelectedPokemon();
        Pokemon_2.GetComponent<PcPokemonCellScript>().UnSelectedPokemon();
        Pokemon_3.GetComponent<PcPokemonCellScript>().UnSelectedPokemon();
        Pokemon_4.GetComponent<PcPokemonCellScript>().UnSelectedPokemon();
        Pokemon_5.GetComponent<PcPokemonCellScript>().UnSelectedPokemon();
        Pokemon_6.GetComponent<PcPokemonCellScript>().UnSelectedPokemon();
    }

    public void SelectCarryPokemon(int PokemonIndex, GameObject SelectCell)
    {
        if(!(PcPokemonChoicePanel.gameObject.activeSelf) && !(PcPokemonAbilityPanel.gameObject.activeSelf) && !(PcPokemonSkillPanel.gameObject.activeSelf))
        {
            if (SelectPokemonCell != null)
            {
                SelectPokemonCell.GetComponent<PcPokemonCellScript>().UnSelectedPokemon();
            }
            SelectPokemonCell = SelectCell;
            selectPokemonIndex = PokemonIndex;
            SelectCell.GetComponent<PcPokemonCellScript>().SelectedPokemon();
        }
    }


    public void PokemonPutPc()
    {
        GameObject CarryPokemon = null;
        switch (selectPokemonIndex)
        {
            case 0:
                {
                    CarryPokemon = Pokemon_1;
                    break;
                }
            case 1:
                {
                    CarryPokemon = Pokemon_2;
                    break;
                }
            case 2:
                {
                    CarryPokemon = Pokemon_3;
                    break;
                }
            case 3:
                {
                    CarryPokemon = Pokemon_4;
                    break;
                }
            case 4:
                {
                    CarryPokemon = Pokemon_5;
                    break;
                }
            case 5:
                {
                    CarryPokemon = Pokemon_6;
                    break;
                }
        }

        if(HeroPokemonManager.Instance.carryPokemonList.Count > 1)  // 2마리 이상 소지할 경우 맡기기 가능
        {
            CarryPokemon.GetComponent<PcPokemonCellScript>().HideInfo();

            //이동할 포켓몬의 인덱스
            int putPokemonIndex = CarryPokemon.GetComponent<PcPokemonCellScript>().index;

            //pc포켓몬 리스트에 추가
            HeroPokemonManager.Instance.pcPokemonList.Add(HeroPokemonManager.Instance.carryPokemonList[putPokemonIndex]);

            //pc포켓몬 그리드 갱신
            PcManager.GetComponent<PcPokemonGrid>().PokemonPutPc();

            //지니고 있는 포켓몬 리스트에서 빼기
            HeroPokemonManager.Instance.carryPokemonList.RemoveAt(putPokemonIndex);

            //재정렬
            RepositionCarryPokemon();

            label_Dialog.text = "[000000]포켓몬을 선택해 주십시오[-]";
        }
        else  // 1마리밖에 소지하고있을때
        {
            Pokemon_1.GetComponent<PcPokemonCellScript>().UnSelectedPokemon();
            label_Dialog.text = "[000000]포켓몬은 한마리 이상 소지하여야 합니다[-]";
        }

    }

    // PC포켓몬 놓아주기
    public void PcPokemonLetOut()
    {
        //PC 포켓몬 리스트 제거
        HeroPokemonManager.Instance.pcPokemonList.RemoveAt(pointPokemonIndex);

        //PC 포켓몬 재정렬
        PcManager.GetComponent<PcPokemonGrid>().PcPokemonReposition();

        
        pointPokemonIndex = 0;
        

        if (HeroPokemonManager.Instance.pcPokemonList.Count > 0)
        {
            gridPc.GetChild(pointPokemonIndex).GetChild(0).transform.localPosition += (Vector3.up * 15);
            gridPc.GetChild(pointPokemonIndex).GetComponent<PcPokemon>().PokemonPointerAble();
        }

        DialogInit();


    }

    void RepositionCarryPokemon()
    {
        Pokemon_1.GetComponent<PcPokemonCellScript>().InitInfo();
        Pokemon_1.GetComponent<PcPokemonCellScript>().HpBarSet();
        Pokemon_2.GetComponent<PcPokemonCellScript>().InitInfo();
        Pokemon_2.GetComponent<PcPokemonCellScript>().HpBarSet();
        Pokemon_3.GetComponent<PcPokemonCellScript>().InitInfo();
        Pokemon_3.GetComponent<PcPokemonCellScript>().HpBarSet();
        Pokemon_4.GetComponent<PcPokemonCellScript>().InitInfo();
        Pokemon_4.GetComponent<PcPokemonCellScript>().HpBarSet();
        Pokemon_5.GetComponent<PcPokemonCellScript>().InitInfo();
        Pokemon_5.GetComponent<PcPokemonCellScript>().HpBarSet();
        Pokemon_6.GetComponent<PcPokemonCellScript>().InitInfo();
        Pokemon_6.GetComponent<PcPokemonCellScript>().HpBarSet();
    }

    public void TransformPanel(GameObject ActivePanel, GameObject HidePanel)
    {
        ActivePanel.gameObject.SetActive(true);
        HidePanel.gameObject.SetActive(false);
    }
    
    public void ActivePanel(GameObject ActivePanel)
    {
        ActivePanel.gameObject.SetActive(true);
    }

    public void HideGrid()
    {
        gridPc.gameObject.SetActive(false);
    }

    public void ActiveGrid()
    {
        gridPc.gameObject.SetActive(true);
    }

    public void ActiveCarryPokemonChoicePanel()
    {
        if (!(PcPokemonChoicePanel.gameObject.activeSelf) && !(PcPokemonAbilityPanel.gameObject.activeSelf) && !(PcPokemonSkillPanel.gameObject.activeSelf))
        {
            if(!(CarryPokemonAbilityPanel.gameObject.activeSelf) && !(CarryPokemonSkillPanel.gameObject.activeSelf))
            {
                label_Dialog.text = label_Dialog.text = "[000000]데리고 있는 " + HeroPokemonManager.Instance.carryPokemonList[selectPokemonIndex].name + "를 어떻게 할까요?[-]";
                CarryPokemonChoicePanel.gameObject.SetActive(true);
            }
        }
    }

    public void DialogInit()
    {
        label_Dialog.text = "[000000]포켓몬을 선택해 주십시오[-]";
    }
    


    public void PcEnd()   // pc 종료
    {
        SceneManager.LoadScene(1);
    }
}
