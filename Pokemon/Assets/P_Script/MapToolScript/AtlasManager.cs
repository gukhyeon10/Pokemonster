using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtlasManager : MonoBehaviour {

    private static AtlasManager instance = null;

    public UIAtlas atlas_Tile;
    public UIAtlas atlas_Build;
    public UIAtlas atlas_Npc;
    public UIAtlas atlas_Bush;

    public static AtlasManager Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        instance = this;    
    }

}
