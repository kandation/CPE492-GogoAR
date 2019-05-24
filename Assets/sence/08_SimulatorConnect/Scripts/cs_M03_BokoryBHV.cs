using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cs_M03_BokoryBHV : MonoBehaviour {
    public int timeProcess = 1;
    [Range(0, 1)]
    public float hpReduce = 0.1f;
    public GameObject water;
    cs_Bokory Bokory;
    float time_lastProcess;

	// Use this for initialization
	void Start () {
        Bokory = this.gameObject.GetComponent<cs_Bokory>();
        time_lastProcess = Time.time;
		
	}
	
	// Update is called once per frame
	void Update () {
        
        
        if (!water.active)
        {
            float time_lastProcess_delta = Time.time - time_lastProcess;
            if (time_lastProcess_delta >= timeProcess)
            {
                Bokory.reduceHP(hpReduce);
                time_lastProcess = Time.time;
            }
        }
        else
        {
            float time_lastProcess_delta = Time.time - time_lastProcess;
            if (time_lastProcess_delta >= timeProcess)
            {
                Bokory.incressHP(hpReduce);
                time_lastProcess = Time.time;
            }
        }

    }
}
