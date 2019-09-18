using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    private static TimeManager instance = null;
    public float deltaTime = 0.0f;

    float lastFrame = 0;
    float currentFrame = 0;
    float myDelta = 0;

    public static TimeManager Instance
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

    void OnDestroy()
    {
        instance = null;    
    }

    void Reset()
    {
        lastFrame = currentFrame = Time.realtimeSinceStartup;
        myDelta = Time.smoothDeltaTime;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        currentFrame = Time.realtimeSinceStartup;
        myDelta = currentFrame - lastFrame;
        myDelta *= Time.timeScale;
        lastFrame = currentFrame;
        

	}

     void LateUpdate()
    {
        deltaTime = (Time.deltaTime + Time.smoothDeltaTime + myDelta) * 0.33333f;
    }
}
