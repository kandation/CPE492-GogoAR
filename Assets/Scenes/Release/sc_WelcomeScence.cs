using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_WelcomeScence : MonoBehaviour {
    sc_SceneLoadManager loadManager;
    sc_SaveIPAddress saveIP;
    

    void Start()
    {
        if(loadManager == null)
        {
            loadManager = GameObject.Find("EventSystem").AddComponent<sc_SceneLoadManager>();
        }
    }

	public void gotoConnect()
    {
        loadManager.LoadScene("02_test_qr");
    }
	
}
