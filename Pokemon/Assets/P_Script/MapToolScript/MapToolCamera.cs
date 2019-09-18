using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapToolCamera : MonoBehaviour {

	
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(Vector3.right * Time.deltaTime * 3f);
        }
        if(Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(Vector3.down * Time.deltaTime * 3f);
        }
        if(Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(Vector3.left * Time.deltaTime * 3f);
        }
        if(Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector3.up * Time.deltaTime * 3f);
        }
        
	}
}
