using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class cs_MissionController : MonoBehaviour
{
    public TrackableBehaviour theTrackable_obj;
    public GameObject ARCamera_obj;
    public GameObject gogoAPI_obj;
    public GameObject Model;
    public GameObject UIController_obj;
    public bool PassMission = false;
    public bool AllowRecomended = true;
    public bool AutoFocus = true;
    public bool ShowBackgroundVideo = true;
    public bool ShowMissionComplete = false;
    public bool b_slamMode = true;
    public string t_prevScence;

    private GogoAPI gogo;


    private CameraFocusController camFocus;
    private bool b_CameraChangeBeforeStep = false;

    private bool b_SlamModeBeforeStep = false;

    private PositionalDeviceTracker pdt_deviceTracker_slamMode;
    private bool b_pdtFirstTime = true;

    private cs_UI c_csui;

    private float f_gogoDelayConnectSec = 2;
    private float f_gogoDelayConnectTime = 0;
    private bool b_gogoDelayConnectFirstTime = true;

    private bool b_gogoFirstTimeStart = true;

    void Start()
    {
        checkGogoAPI();
        checkARCamera();
        checkModel();
        checkCamfocus();

        gogo = gogoAPI_obj.GetComponent<GogoAPI>();
        if (gogo == null)
        {
            Debug.LogError("FFFFF");
            Application.Quit();
        }
        //gogo.autoLoad();
        //gogo.refreshConnection();

        c_csui = UIController_obj.GetComponent<cs_UI>();



    }

    void Update()
    {
        pdt_firstInit();

        switchAutoFocus();
        update_AutofocuFromUIController();
        update_val_SlamMode();
        update_VuforiaConfig_slamMode();
        gogo_delayConnect();
        checkPlay();
        gogo_updateStatus();

    }



    private void gogo_delayConnect()
    {
        if (Time.time - f_gogoDelayConnectTime >= f_gogoDelayConnectSec && b_gogoDelayConnectFirstTime)
        {
            gogo.autoLoad();
            gogo.refreshConnection();
            b_gogoDelayConnectFirstTime = false;
        }
    }

    private void gogo_updateStatus()
    {
        c_csui.setConnectionText(gogo.isConnected, gogo.socketStatus);
    }

    private void gogo_startSimulator()
    {
        if (gogo != null)
        {
            if (gogo.isConnected)
            {
                gogo.sendCommand("cmd::LogoControl::0");
                gogo.sendCommand("cmd::LogoControl::1");
            }
        }
    }

    private void gogo_LoadIP()
    {
        gogo.autoLoad();
    }

    private void checkPlay()
    {
        bool _b_PLay = UIController_obj.GetComponent<cs_UI>().b_Play;
        bool _b_Pause = UIController_obj.GetComponent<cs_UI>().b_Pause;

        if (_b_PLay || _b_Pause)
        {
            Model.SetActive(true);
            if (b_gogoFirstTimeStart)
            {
                gogo_startSimulator();
                b_gogoFirstTimeStart = false;
            }

        }
        else
        {
            Model.SetActive(false);
        }
    }

    private void pdt_firstInit()
    {
        if (VuforiaRuntime.Instance.InitializationState == VuforiaRuntime.InitState.INITIALIZED && b_pdtFirstTime)
        {
            pdt_deviceTracker_slamMode = TrackerManager.Instance.GetTracker<PositionalDeviceTracker>();
            Debug.LogWarning(pdt_deviceTracker_slamMode.IsActive);
            UIController_obj.GetComponent<cs_UI>().b_slamMode = pdt_deviceTracker_slamMode.IsActive;
            b_pdtFirstTime = false;
        }
    }

    private void update_VuforiaConfig_slamMode()
    {
        if (b_slamMode != b_SlamModeBeforeStep && !b_pdtFirstTime)
        {
            pdt_deviceTracker_slamMode = TrackerManager.Instance.GetTracker<PositionalDeviceTracker>();

            if (b_slamMode)
            {
                if (pdt_deviceTracker_slamMode == null)
                {
                    pdt_deviceTracker_slamMode = TrackerManager.Instance.InitTracker<PositionalDeviceTracker>();
                    Debug.LogWarning("PDT ADD POSITION DEVICE");

                }
                pdt_deviceTracker_slamMode.Start();
                Debug.LogWarning("PDT START");
            }
            else
            {
                pdt_deviceTracker_slamMode.Stop();
                Debug.LogWarning("PDT STOP");
            }
        }
        b_SlamModeBeforeStep = b_slamMode;

    }


    private void update_val_SlamMode()
    {
        b_slamMode = UIController_obj.GetComponent<cs_UI>().b_slamMode;
    }

    private void update_AutofocuFromUIController()
    {
        AutoFocus = UIController_obj.GetComponent<cs_UI>().b_autoFocus;
    }

    private void switchAutoFocus()
    {
        if (camFocus.mVuforiaStarted)
        {
            if (AutoFocus)
            {
                camFocus.SetAutofocus();

            }
            else
            {
                camFocus.SetFixedFocus();

            }

            if (AutoFocus != b_CameraChangeBeforeStep)
            {
                camFocus.RestartCamera();
                b_CameraChangeBeforeStep = AutoFocus;
            }


        }


    }

    private void checkCamfocus()
    {
        camFocus = ARCamera_obj.GetComponent<CameraFocusController>();
        if (camFocus == null)
        {
            camFocus = ARCamera_obj.AddComponent<CameraFocusController>();
        }

    }



    private void checkGogoAPI()
    {
        if (gogoAPI_obj == null)
        {
            gogoAPI_obj = GameObject.Find("GogoAPI").gameObject;
            if (gogoAPI_obj == null)
            {
                gogoAPI_obj = new GameObject("GogoAPI");
                Instantiate(gogoAPI_obj, gameObject.transform);
            }

            if (!gogoAPI_obj.GetComponent<GogoAPI>())
            {
                gogoAPI_obj.AddComponent<GogoAPI>();
            }
        }
    }

    private void checkARCamera()
    {
        if (ARCamera_obj == null)
        {
            ARCamera_obj = GameObject.Find("ARCamera").gameObject;
            Debug.LogError("Not Found ARCamera");
            Application.Quit();
        }
    }

    private void checkModel()
    {
        if (Model == null)
        {

            Model = new GameObject("TEMPModel");
            Instantiate(Model, gameObject.transform);

        }
    }

    private void checkTrackAble()
    {
        if (!theTrackable_obj)
        {

        }
    }


}

