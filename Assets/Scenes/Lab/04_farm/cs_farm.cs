using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cs_farm : MonoBehaviour {
    sc_SceneLoadManager gotofarm;

	// Use this for initialization
	void Start () {
		
	}

    public void gotoFram()
    {
        gotofarm = gameObject.AddComponent<sc_SceneLoadManager>();
        gotofarm.LoadScene("07_missionPump");
    } 
	

}
