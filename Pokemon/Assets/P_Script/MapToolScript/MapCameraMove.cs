using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraMove : MonoBehaviour {

    private static MapCameraMove instance = null;

    Transform this_Transform;
    public Transform character;

    public Transform fadePanel;

    public static MapCameraMove Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    { 
        instance = this;
        this_Transform = this.transform;
    }

    void OnDestroy()
    {
        instance = null;    
    }

    void LateUpdate()
    {
        if(character != null)
        {
            this_Transform.localPosition = new Vector3(character.localPosition.x, character.localPosition.y, -169f);
            fadePanel.localPosition = new Vector3(character.localPosition.x, character.localPosition.y, 0f);
        }
    }
}
