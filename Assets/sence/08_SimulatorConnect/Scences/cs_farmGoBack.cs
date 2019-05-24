using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cs_farmGoBack : MonoBehaviour {
    sc_SceneLoadManager goback;

	// Use this for initialization
	void Start () {
		
	}

    public void farmGoBack()
    {
        if(goback == null)
        {
            goback = gameObject.AddComponent<sc_SceneLoadManager>();
            goback.LoadScene("sc_menuSelect");
        }
    }
	
}
