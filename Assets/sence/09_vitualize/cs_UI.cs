using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class cs_UI : MonoBehaviour
{
    GameObject MissionCore;
    GameObject UIController;
    GameObject UIInterface;

    cs_MissionController c_missionController;

    public bool b_Play = false;
    public bool b_Pause = false;
    public bool b_Renew = false;
    public bool b_autoFocus = false;

    public bool b_slamMode = false;

    public bool b_isConnectGogo = false;
    public bool b_isConnectWebsocket = false;

    public GameObject page_main;

    public GameObject page_setting;
    public GameObject page_info;
    public GameObject page_title;

    public GameObject btn_Play;
    public GameObject btn_Pause;
    public GameObject btn_Renew;
    public GameObject btn_back;

    public GameObject btn_setting;
    public GameObject btn_info;


    private List<GameObject> gameObjs = new List<GameObject>(0);

    public GameObject obj_SimulatorTime;
    private Text text_SimulatorTIme;

    public GameObject obj_TextGogoStatus;
    public GameObject obj_TextWebsocketStatus;
    private Text text_gogoStatus;
    private Text text_websocketStatus;

    public GameObject obj_textTitle;
    public string t_title = "Smart Framer";
    private Text text_title;

    private float title_time_sec = 0;

    private sc_SceneLoadManager loadSceneManger;

    private float timer = 0;

    public GameObject btn_Autofocus;
    public GameObject txt_Autofocus;
    private Text text_Autofocus;
    private Image img_btnAutoFocus;

    public GameObject btn_slamMode;
    private Image img_btnSlamMode;



    // Use this for initialization
    void Start()
    {
        page_main.SetActive(true);

        checkMissionCOre();
        checkUIController();
        checkInterfaceUI();

        text_SimulatorTIme = obj_SimulatorTime.GetComponent<Text>();
        text_title = obj_textTitle.GetComponent<Text>();
        text_title.text = t_title;

        checkMissionCore();
        checkLoadSceneManger();

        timer = Time.time;


        initGameObjList();
        showMain();

        text_gogoStatus = obj_TextGogoStatus.GetComponent<Text>();
        text_websocketStatus = obj_TextWebsocketStatus.GetComponent<Text>();
        
        img_btnAutoFocus = btn_Autofocus.GetComponent<Image>();

        text_Autofocus = txt_Autofocus.GetComponent<Text>();

        img_btnSlamMode = btn_slamMode.GetComponent<Image>();
    }

    void Update()
    {
        if (Time.time - title_time_sec > 5)
        {
            hideListIndex(7);
        }

        if (b_Play || b_Pause)
        {
            text_SimulatorTIme.text = (Time.time - timer).ToString();
        }
        else
        {
            timer = Time.time;
        }

        update_btnAutoFocusColor();
        update_btnAutofocusTxt();
        update_btnSlamColor();
        updateConnectStatus();


    }

    public void toggleSlamMode()
    {
        b_slamMode = !b_slamMode;
    }

    private void update_btnSlamColor()
    {
        if (b_slamMode)
        {
            img_btnSlamMode.color = Color.green;
        }
        else
        {
            img_btnSlamMode.color = Color.white;
        }
    }

    private void update_btnAutofocusTxt()
    {
        if (b_autoFocus)
        {
            text_Autofocus.text = "Auto Focus";
        }
        else
        {
            text_Autofocus.text = "Infinity";
        }
    }

    private void update_btnAutoFocusColor()
    {
        if (b_autoFocus)
        {
            img_btnAutoFocus.color = Color.green;
        }
        else
        {
            img_btnAutoFocus.color = Color.white;
        }
    }

    public void setConnectionText(bool gg, bool ws)
    {
        b_isConnectGogo = gg;
        b_isConnectWebsocket = ws;
    }

    private void updateConnectStatus()
    {
        text_gogoStatus.text = updateConnectionText_string(b_isConnectGogo);
        text_websocketStatus.text = updateConnectionText_string(b_isConnectWebsocket);
    }

    private string updateConnectionText_string(bool connect)
    {
        return (connect) ? "<color=green>Connected</color>" : "<color=red>Not Connected</color>";
    }

    private void checkLoadSceneManger()
    {
        if (loadSceneManger == null)
        {
            loadSceneManger = gameObject.AddComponent<sc_SceneLoadManager>();
        }
    }

    private void checkMissionCore()
    {
        if (MissionCore == null)
        {
            MissionCore = GameObject.Find("MissionCOre").gameObject;
            if (MissionCore == null)
            {
                Debug.LogError("UIController Connot find mission core");
                Application.Quit();
            }
            c_missionController = MissionCore.GetComponent<cs_MissionController>();
        }
    }

    private void initGameObjList()
    {
        gameObjs.Add(page_setting);     //0
        gameObjs.Add(page_info);        //1
        gameObjs.Add(btn_Play);         //2
        gameObjs.Add(btn_Pause);        //3
        gameObjs.Add(btn_Renew);        //4
        gameObjs.Add(btn_setting);      //5
        gameObjs.Add(btn_info);         //6
        gameObjs.Add(page_title);        //7
        gameObjs.Add(btn_back);       //8
    }

    private void hideListIndex(int index)
    {
        if (index == -1)
        {
            foreach (GameObject gm in gameObjs)
            {
                gm.SetActive(false);
            }
        }
        else
        {
            gameObjs[index].SetActive(false);
        }
    }

    private void showListIndex(int index)
    {
        gameObjs[index].SetActive(true);
    }

    private bool checkListIndex(int index)
    {
        return gameObjs[index].active;
    }

    private void showheader()
    {
        showListIndex(5);
        showListIndex(6);
        showListIndex(8);

    }

    public void showMain()
    {
        hideListIndex(-1);
        showListIndex(2);
        showheader();
        showListIndex(7);

        title_time_sec = Time.time ;
    }

    public void showSetting()
    {
        if (!checkListIndex(0))
        {
            if (checkListIndex(1))
            {
                hideListIndex(1);
            }
            showListIndex(0);
        }
        else
        {

            hideListIndex(0);
        }

    }

    public void showInfo()
    {
        if (!checkListIndex(1))
        {
            if (checkListIndex(0))
            {
                hideListIndex(0);
            }
            showListIndex(1);
        }
        else
        {

            hideListIndex(1);
        }

    }

    public void runPlay()
    {
        hideListIndex(-1);
        showheader();
        showListIndex(3);
        showListIndex(4);
        b_Play = true;
        b_Pause = false;
        b_Renew = false;

        Time.timeScale = 1;

    }

    public void runStop()
    {
        hideListIndex(-1);
        showheader();
        showListIndex(2);
        hideListIndex(4);
        b_Play = false;
        b_Pause = true;
        b_Renew = false;

        Time.timeScale = 0;

    }

    public void runRenew()
    {
        hideListIndex(-1);
        showheader();

        b_Play = false;
        b_Pause = false;
        b_Renew = true;

        loadSceneManger.LoadScene(SceneManager.GetActiveScene().name); 

    }

    public void gotoReConnect()
    {
        loadSceneManger.LoadScene("02_test_qr");
    }

    public void gotoMainMenu()
    {
        loadSceneManger.LoadScene("sc_menuSelect");
    }



    private void checkInterfaceUI()
    {
        if (UIInterface == null)
        {
            UIInterface = GameObject.Find("UI_Interface");
            if (UIInterface == null)
            {
                Debug.LogWarning("UI_NOTFOUND");

            }
        }
    }

    private void checkMissionCOre()
    {
        MissionCore = GameObject.Find("MissionCore");
        if (MissionCore == null)
        {
            Debug.LogError("Cannot FInd Mission Core");
            Application.Quit();
        }

    }

    private void checkUIController()
    {
        if (UIController == null)
        {
            UIController = GameObject.Find("UIController");
            if (UIController == null)
            {
                UIController = new GameObject("UIController");

            }
        }
    }

    public void toggleAutoFocus()
    {
        b_autoFocus = !b_autoFocus;
    }

    public void pause()
    {
        Time.timeScale = 0;
    }
}
