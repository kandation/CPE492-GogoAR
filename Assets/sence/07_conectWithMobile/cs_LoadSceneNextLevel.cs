using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cs_LoadSceneNextLevel : MonoBehaviour {
    public GameObject exce;
    public GameObject uiMenu;

	// Use this for initialization
	void Start () {
        setActiveAll(false);
        uiMenu.SetActive(true);

    }
    void setActiveAll(bool status)
    {
        exce.SetActive(status);
        uiMenu.SetActive(status);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    

    public void nextExercise()
    {
        setActiveAll(false);
        exce.SetActive(true);
    }

    public void gotoWaterfill()
    {
        SceneManager.LoadScene("sc_TestGogoAPIMoblieWithContainer");
    }
}
