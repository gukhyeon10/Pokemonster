using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushScript : MonoBehaviour {

    public UISprite m_Bush;

    public int tileNumber;
    public int bushAngle;

    public void ObjectMouseRightButtonDown()
    {
        if(Input.GetMouseButton(1))
        {
            Debug.Log(tileNumber + "number this Bush Press!");
            ToolCursor.Instance.MapObjectSelect(this.gameObject);
        }
    }

    public void SetBushObject()
    {
        float objectSizeX = 1, objectSizeY = 1;
        float objectPositionX = 0, objectPositionY = 0;


        switch(m_Bush.spriteName)
        {
            case "Bush_03":
                {
                    objectSizeX = 1;
                    objectSizeY = 1.6f;
                    objectPositionX = 0;
                    objectPositionY = 40;
                    break;
                }
            default:
                {
                    break;
                }
        }

        this.transform.localScale = new Vector3(1.025f, 1.025f);
        m_Bush.transform.localScale = new Vector3(objectSizeX, objectSizeY);
        m_Bush.transform.localPosition += new Vector3(objectPositionX, objectPositionY);
        m_Bush.transform.localEulerAngles = new Vector3(0, 0, ToolCursor.Instance.GetCursorRotation);
        bushAngle = ToolCursor.Instance.GetCursorRotation;
    }
}
