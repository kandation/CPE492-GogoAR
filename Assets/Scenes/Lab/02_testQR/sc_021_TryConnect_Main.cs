using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sc_021_TryConnect_Main : MonoBehaviour {

    public GameObject obj_label_IPAddress;
    private Text label_IPAdress;
    private sc_SaveIPAddress saveIP;

    private GogoAPI gogo;

    private float lastUpdateTime = 0;
    public float lastUpdateTimePeriodSec = 2;
    private bool lastUpdateTime_isFirst = true;

    public GameObject obj_Loadding;
    private float loadding_angel = 0;

    private float canConnectedTime = 0;

    public GameObject obj_panel_failed;

    private string TempIP = "127.0.0.1";

    public GameObject obj_panel_refillIPaddress;

	// Use this for initialization
	void Start () {
        label_IPAdress = obj_label_IPAddress.GetComponent<Text>();

        saveIP = GameObject.Find("EventSystem").AddComponent<sc_SaveIPAddress>();
        saveIP.LoadFileIP();

        label_IPAdress.text = saveIP.getData();

        gogo = GameObject.Find("GogoAPI").GetComponent<GogoAPI>();

        gogo.showDebug = true;

        lastUpdateTime = Time.time;

        canConnectedTime = Time.time;

        obj_panel_failed.SetActive(false);
        obj_panel_refillIPaddress.SetActive(false);


    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time - lastUpdateTime >= lastUpdateTimePeriodSec)
        {
            // Waiting Period Sec to Auto Connect
            gogo.autoLoad();
            gogo.refreshConnection();
            if (gogo.isConnected)
            {
                sc_SceneLoadManager sc_temp = gameObject.AddComponent<sc_SceneLoadManager>();
                sc_temp.LoadScene("sc_menuSelect");
            }
            lastUpdateTime = Time.time;
        }

        if(Time.time - canConnectedTime >= 25)
        {
            obj_Loadding.SetActive(false);
            obj_panel_failed.SetActive(true);
        }

        loadding_angel = -5f ;
        obj_Loadding.transform.Rotate(0, 0, loadding_angel, Space.Self);
	}

    public void Event_TryAgain()
    {
        sc_SceneLoadManager sc_temp = gameObject.AddComponent<sc_SceneLoadManager>();
        sc_temp.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void setTempIP(InputField input)
    {
        TempIP = input.text; 
    }

    public void refillIPAddress()
    {
        saveIP.setData(TempIP);
        saveIP.SaveFileIP();

        sc_SceneLoadManager sc_temp = gameObject.AddComponent<sc_SceneLoadManager>();
        sc_temp.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void showRefillIPAddressPanel()
    {
        obj_panel_refillIPaddress.SetActive(true);
    }
}
