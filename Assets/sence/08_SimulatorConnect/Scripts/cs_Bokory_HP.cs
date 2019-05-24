using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cs_Bokory_HP : MonoBehaviour {
    public GameObject BokoryHP;
    public Image bar;

    public float hp = 0;
    public GameObject iconWet;
    public GameObject iconHealthy;
    public GameObject iconDry;
    private bool healthyToggle = false;
    private bool healthyToggleSet = false;
    public float healthyTime = 0;
    public bool thinkPassMission = false;

    public GameObject gogoApi;
    private GogoAPI gogo;

    private int levelStatus = 0;
    

	// Use this for initialization
	void Start () {
        hp = BokoryHP.GetComponent<cs_Bokory>().hp;
        iconWet.SetActive(false);
        iconHealthy.SetActive(false);
        iconDry.SetActive(false);

        gogo = gogoApi.GetComponent<GogoAPI>();

    }
	
	// Update is called once per frame
	void Update () {
        hp = BokoryHP.GetComponent<cs_Bokory>().hp;
        bar.fillAmount = hp;
        if (hp > 0.8)
        {
            iconWet.SetActive(true);
            iconHealthy.SetActive(false);
            iconDry.SetActive(false);
            levelStatus = 2;
        }
        else if(hp > 0.3 && hp < 0.6) {
            iconWet.SetActive(false);
            iconHealthy.SetActive(true);
            iconDry.SetActive(false);
            healthyToggle = true;

            levelStatus = 1;
        }
        else if (hp <= 0.3)
        {
            // dry
            iconWet.SetActive(false);
            iconHealthy.SetActive(false);
            iconDry.SetActive(true);

            levelStatus = 0;
        }
        else
        {
            iconWet.SetActive(false);
            iconHealthy.SetActive(false);
            iconDry.SetActive(false);

           
        }


        gogoSender(levelStatus);
        healthyTimeCheck();
		
	}

    private bool gogoSenderLevel0 = true;
    private bool gogoSenderLevel1 = true;
    private bool gogoSenderLevel2 = true;
    private bool gogoSenderLevel3 = true;

    void gogoSender(int lv)
    {
        if (lv == 0)
        {
            if (gogoSenderLevel0)
            {
                gogo.sendCommand("cmd::talkToMotor::8");
                gogo.sendCommand("cmd::motorCW");
                gogo.sendCommand("cmd::motorOn");
                gogoSenderLevel0 = false;
                gogoSenderLevel1 = true;
                gogoSenderLevel2 = true;
                gogoSenderLevel3 = true;
            }
            //if (!gogo.motors_isOn[3])
            //{
            //    gogo.sendCommand("cmd::motorOn");
            //}
        }
        else if(lv ==1)
        {
            if (gogoSenderLevel1)
            {
                gogo.sendCommand("cmd::talkToMotor::8");
                gogo.sendCommand("cmd::motorCCW");
                gogo.sendCommand("cmd::motorOn");
                gogoSenderLevel0 = true;
                gogoSenderLevel1 = false;
                gogoSenderLevel2 = true;
                gogoSenderLevel3 = true;
            }
            //if (!gogo.motors_isOn[3])
            //{
            //    gogo.sendCommand("cmd::motorOn");
            //}
        }
        else if(lv ==2)
        {
            if (gogoSenderLevel2)
            {
                gogo.sendCommand("cmd::talkToMotor::8");
                gogo.sendCommand("cmd::motorCCW");
                gogo.sendCommand("cmd::motorOff");
                gogoSenderLevel0 = true;
                gogoSenderLevel1 = true;
                gogoSenderLevel2 = false;
                gogoSenderLevel3 = true;
            }
            //if (gogo.motors_isOn[3])
            //{
            //    gogo.sendCommand("cmd::motorOff");
            //}

        }
        else
        {
            if (gogoSenderLevel3)
            {
                gogo.sendCommand("cmd::talkToMotor::8");
                gogo.sendCommand("cmd::motorCW");
                gogo.sendCommand("cmd::motorOff");
                gogoSenderLevel0 = true;
                gogoSenderLevel1 = true;
                gogoSenderLevel2 = true;
                gogoSenderLevel3 = false;
            }
            //if (gogo.motors_isOn[3])
            //{
            //    gogo.sendCommand("cmd::motorOff");
            //}
        }
    }

    void healthyTimeCheck()
    {
        if (healthyToggle)
        {
            if (!healthyToggleSet)
            {
                healthyTime = Time.time;
                healthyToggleSet = true;
            }
            else
            {
                if (Time.time - healthyTime >= 60)
                {
                    thinkPassMission = true;
                }
                else
                {
                    thinkPassMission = false;
                }
            }

        }
        else
        {
            healthyToggleSet = false;
            thinkPassMission = false;
            healthyTime = Time.time;

        }
    }
}
