using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokedexInfoDetail : MonoBehaviour {

    [SerializeField]
    UILabel label_No;
    [SerializeField]
    UILabel label_Name;
    [SerializeField]
    UILabel label_Kind;
    [SerializeField]
    UILabel label_Height;
    [SerializeField]
    UILabel label_Weight;
    [SerializeField]
    UILabel label_Explain;


    public void DetailSet(string no, string name, string kind, string height, string weight, string explain)
    {
        label_No.text = no;
        label_Name.text = name;
        label_Kind.text = kind;
        label_Height.text = height + " m";
        label_Weight.text = weight + " kg";
        label_Explain.text = explain;
    }
}
