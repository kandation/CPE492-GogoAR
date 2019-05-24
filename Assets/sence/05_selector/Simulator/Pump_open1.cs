using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pump_open1 : MonoBehaviour {
    public GameObject Pump;
    public GameObject PumpSmoke;
    public GameObject ObjectGogoAPI;
    public int SensorButton = 0;
    public int MotorPort = 0;

    private float buttomToggle = 0;
    private WaterTank tank;
    public bool isBroken = false;

    public float smokeHP = 100;
    GogoAPI gogo;

    bool isFirstOn = false;

    // Use this for initialization
    void Start() {
       
        //ModifyButton.GetComponent<Button>().onClick.AddListener(togglePump);
        gogo = ObjectGogoAPI.GetComponent<GogoAPI>();
        buttomToggle = Time.time;
        tank = gameObject.GetComponent<WaterTank>();
        Pump.SetActive(false);
        if(PumpSmoke != null)
        {
            PumpSmoke.SetActive(false);
        }



    }

    void Update()
    {
        checkBroken();
        if (!isBroken)
        {
            checkSensor();
        }
        

    }
    void checkSensor()
    {
        /*if (gogo.sensor[SensorButton] > 500 && !Pump.active)
        {
            //Debug.Log("Pump is On");

            float lk = Time.time - buttomToggle;
            if (lk >= 1)
            {
                buttomToggle = Time.time;
                Pump.SetActive(true);
            }

        }
        if (gogo.sensor[SensorButton] > 500 && Pump.active)
        {
            float lk = Time.time - buttomToggle;
            Debug.Log("Time" + lk);
            if (lk >= 1)
            {
                Pump.SetActive(false);
                buttomToggle = Time.time;
            }

        }*/
        if (!gogo.motors_isOn[MotorPort] && !isBroken)
        {
            Pump.SetActive(false);
            Debug.Log("gffffffffffff");

        }
        else if (gogo.motors_isOn[MotorPort] && !isBroken && Time.time >1)
        {
            Debug.Log("PPPP");
            Pump.SetActive(true);
            smokeHP = 100f;
        }
        else
        {
            Pump.SetActive(false);
        }
        
    }

  

    void checkBroken()
    {
        if (PumpSmoke != null)
        {
            if (isBroken && gogo.motors_isOn[MotorPort])
            {
                if (smokeHP <= 0)
                {
                    PumpSmoke.SetActive(true);

                }
                else
                {
                    smokeHP -= 0.5f;
                }
            }
            if (tank.getValueAsPercent() <= 0 && Pump.active)
            {
                
                Pump.SetActive(false);
                isBroken = true;
               
                
                
            }
            if(tank.getValueAsPercent() > 1 && !Pump.active && Time.time  > 1f)
            {
                Debug.Log("yyyyyyyyyy");
                
                PumpSmoke.SetActive(false);
                isBroken = false;

            }
        }
        

    }


    void togglePump()
    {
        Pump.SetActive(!Pump.active);
    }
}
