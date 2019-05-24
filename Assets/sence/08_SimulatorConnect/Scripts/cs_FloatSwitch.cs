using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cs_FloatSwitch : MonoBehaviour {
    [Range(0, 1)]
    public float Value = 0;
    public int maxFrame = 100;
    Animator anim;
    public GameObject waterTankObj;
    public GameObject gogoAPIObj;
    WaterTank tank;
    GogoAPI gogoapi;

    bool nerverSenderFull = true;
    bool nerverSenderEmpty = true;
    
    

    // Use this for initialization
    void Start () {
        anim = gameObject.GetComponent<Animator>();
        tank = waterTankObj.GetComponent<WaterTank>();
        gogoapi = gogoAPIObj.GetComponent<GogoAPI>();
        TalkToGoGo_tankIsFullOut();
    }
	
	// Update is called once per frame
	void Update () {
        Value = tank.Value;
        Value = Mathf.Clamp(Value, 0, (float)maxFrame);
        float cleanValue = (Value) * 100;
        anim.Play("anim_FloatSwitch", 0, (1f / maxFrame) * cleanValue);

        if (Value >= 1)
        {
            if (nerverSenderFull)
            {
                TalkToGoGo_tankIsFull();
                nerverSenderFull = false;
                nerverSenderEmpty = true;
            }
            
        }

        if(Value > 0.9 && Value < 1)
        {
            if (nerverSenderEmpty)
            {
                TalkToGoGo_tankIsFullOut();
                nerverSenderEmpty = false;
                nerverSenderFull = true;
            }
        }
        

    }

    void TalkToGoGo_tankIsFull()
    {
        gogoapi.sendCommand("cmd::talkToMotor::4");
        gogoapi.sendCommand("cmd::motorOn");

    }
    void TalkToGoGo_tankIsFullOut()
    {
        gogoapi.sendCommand("cmd::talkToMotor::4");
        gogoapi.sendCommand("cmd::motorOff");
    }

}
