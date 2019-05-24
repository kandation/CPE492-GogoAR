using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cs_M00_CanvasController : MonoBehaviour {
    public GameObject tutorial;
    public GameObject renewIP;


	// Use this for initialization
	void Start () {
        setAllUI(false);


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void setAllUI(bool status)
    {
        tutorial.SetActive(status);
        renewIP.SetActive(status);
    }
    public void clickShowTutorial()
    {
        bool nextStage = !tutorial.active;
        setAllUI(false);
        tutorial.SetActive(nextStage);
    }

    public void clickShowRenewIP()
    {
        bool nextStage = !renewIP.active;
        setAllUI(false);
        renewIP.SetActive(nextStage);
    }
}
