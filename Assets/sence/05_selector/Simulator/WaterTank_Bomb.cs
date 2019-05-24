using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTank_Bomb : MonoBehaviour {
    public GameObject tank;
    private WaterTank tankValue;
    public GameObject particle;
    public GameObject pump01;

    public GameObject gogoapi;
    GogoAPI gogo;

    bool gogosenderFirst = true;
    bool gogosenderFirst2 = true;
    

	// Use this for initialization
	void Start () {
        particle.active = false ;
        tankValue = tank.GetComponent<WaterTank>();
        gogo = gogoapi.GetComponent<GogoAPI>();
		
	}
	
	// Update is called once per frame
	void Update () {

        if (tankValue.isOverFull())
        {
            if (gogosenderFirst)
            {
                gogo.sendCommand("cmd::talkToMotor::4");
                gogo.sendCommand("cmd::motorOn");
                gogosenderFirst = false;
                gogosenderFirst2 = true;
            }
        }
        else
        {
            if (gogosenderFirst2)
            {
                gogo.sendCommand("cmd::talkToMotor::4");
                gogo.sendCommand("cmd::motorOff");
                gogosenderFirst = true;
                gogosenderFirst2 = false;
            }
        }

        if (tankValue.isOverFull() && pump01.active)
        {
            particle.SetActive(true);
            
        }
        else
        {
            particle.SetActive(false);
            
        }
        
        
		
	}
}
