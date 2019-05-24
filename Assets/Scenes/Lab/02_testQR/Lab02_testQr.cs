using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;



public class Lab02_testQr : MonoBehaviour
{
    private bool camAvalible;
    private WebCamTexture backCam;
    private Texture defualtBakground;

    private float lastUpdate = 0;

    public float lastUpdatePeriodSec = 2;

    public RawImage background;
    public AspectRatioFitter fit;
    public Text lable;

    private sc_SaveIPAddress saveIP;

    private void Start()
    {
        if(saveIP == null)
        {
            saveIP = GameObject.Find("EventSystem").AddComponent<sc_SaveIPAddress>();
        }

        defualtBakground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            Debug.Log("No cameara detected");
            camAvalible = false;
            return;
        }

        for (int i=0; i< devices.Length; i++)
        {
            bool checkFrontCam = false;
            checkFrontCam = (Application.isEditor) ? devices[i].isFrontFacing : !devices[i].isFrontFacing;
            if (checkFrontCam)
            {
                backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
        }

        if (backCam == null)
        {
            Debug.Log("Unable to find back Camemra");
                return;
        }

        backCam.Play();
        background.texture = backCam;
        camAvalible = true;
    }

    private void Update()
    {
        if (!camAvalible)
        {
            return;
        }

        float ratio = (float)backCam.width / (float)backCam.height;
        fit.aspectRatio = ratio;

        float scaleY = backCam.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient = -backCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
        scanQR();


    }

    private void gogoIP(string tx)
    {
        if (checkGogoIP(tx))
        {
            lable.text = getGogoIP(tx);
            backCam.Stop();
            saveIP.setData(lable.text);
            saveIP.SaveFileIP();
            LoadScene("021_TryConnect");
        }
    }

    private bool checkGogoIP(String tx)
    {
        string[] words = tx.Split(',');
        if (words[0].Equals("GOGOIPADDRESS"))
        {
            return true;
        }return false;
    }

    private string getGogoIP(string tx)
    {
        string[] words = tx.Split(',');
        return words[1];
    }

    private void scanQR()
    {
        if(Time.time - lastUpdate >= lastUpdatePeriodSec)
        {
            try
            {
                IBarcodeReader barcodeReader = new BarcodeReader();
                // decode the current frame
                var result = barcodeReader.Decode(backCam.GetPixels32(),
                  backCam.width, backCam.height);
                if (result != null)
                {
                    //lable.text = result.Text;
                    gogoIP(result.Text);
                }
            }
            catch (Exception ex) { Debug.LogWarning(ex.Message); }
            lastUpdate = Time.time;
        }
        
       
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneObject(sceneName));
    }

    public IEnumerator LoadSceneObject(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        async.allowSceneActivation = false;

        // Loop เพื่อตรวจสอบว่าโหลด Object เสร็จหรือยัง
        while (!async.isDone)
        {
            // ทำการคำนวณ progress
            float progress = Mathf.Clamp01(async.progress / 0.9f);
            Debug.Log("Loading progress: " + (progress * 100).ToString("n0") + "%");

            // Loading completed
            if (progress == 1f)
            {
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }


    public void skip()
    {
        backCam.Stop();
        LoadScene("021_TryConnect");
    }
}
