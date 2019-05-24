using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class cs_UIController : MonoBehaviour
{
    GogoAPI gogoAPI;
    public GameObject gogoAPIGameObject;
    public GameObject UIGogoStatusText;
    public GameObject UIGogoDebugText;

    public GameObject tutorial;
    public GameObject renewIP;

    public GameObject TrackObject;

    private GogoAPI gogo;





    // Use this for initialization
    void Start()
    {
        gogo = gogoAPIGameObject.GetComponent<GogoAPI>();
        setAllUI(false);

    }

    // Update is called once per frame
    void Update()
    {
        updateGogoSettingText();
        updateGogoDebugText();
        

    }


    public void UIBack()
    {
        /*using (StreamWriter sw = new StreamWriter("Assets/TestFile.txt"))
        {
            // Add some text to the file.
            sw.Write("This is the ");
            sw.WriteLine("header for the file.");
            sw.WriteLine("-------------------");
            // Arbitrary objects can also be written to the file.
            sw.Write("The date is: ");
            //sw.WriteLine(DateTime.Now);
        }*/
        SceneManager.LoadScene("sc_loadScene");
    }

    public void closeAllUI()
    {
        setAllUI(false);
    }

    void updateGogoSettingText()
    {
        UIGogoStatusText.GetComponent<Text>().supportRichText = true;
        UIGogoStatusText.GetComponent<Text>().text = "Status: " + gogo.gogoWarning;
    }

    void updateGogoDebugText()
    {
        UIGogoDebugText.GetComponent<Text>().supportRichText = true;
        UIGogoDebugText.GetComponent<Text>().text = "Sensor: " + gogo.sensor_status_str + "\nMotor: " + gogo.motor_status_str;
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

    bool isActiveOnlyOnceUI()
    {
        return tutorial.active || renewIP.active;
    }

    public void ZoomIn()
    {
        TrackObject.transform.localScale += new Vector3(0.02f, 0.02f, 0.02f);
    }

    public void ZoomOut()
    {
        TrackObject.transform.localScale += new Vector3(-0.02f, -0.02f, -0.02f);
    }
}

public class cs_SettingUI : MonoBehaviour
{
    string GogoIPAddress = "127.0.0.1";

}
