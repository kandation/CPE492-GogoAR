using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cs_M02_waterFill : MonoBehaviour {
    [Range(0.1f, 10f)]
    public float TimeToProcess = 1;
    public float FillSpeed = 1;
    public GameObject tank;
    public GameObject pump01;
    public GameObject pump02;
    public int fillSpeedPump01 = 200;
    public int fillSpeedPump02 = -150;
    private float lastAddWater = 0;
    private float lastRemPump = 0;

    float total_water = 0;
    float total_water_element = 0;
    float last = 0;

    float kkkk = -1;


    float deltaTime = 0.0f;
    float rate = 0;

    public bool showDebug = false;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        
        total_water_element = 0;
        float timeInterupt = Time.time - lastAddWater;
        if (timeInterupt >= TimeToProcess)
        {
            total_water = 0;

            if (pump01.active)
            {
                total_water_element -= fillSpeedPump01;
                Debug.Log("PUMP01");
            }
            
            if (pump02.active)
            {
                total_water_element += fillSpeedPump02;
            }
            last = tank.GetComponent<WaterTank>().getValueAsPercent();
            total_water = last - total_water_element;
            lastAddWater = Time.time;
            Debug.Log("GG");


        }
        else
        {
            //kkkk = Mathf.LerpUnclamped(1f, 10000f, 10f);

            rate = total_water / 60f;
            if (!pump01.active && !pump02.active)
            {
                rate = 0;
            }

                tank.GetComponent<WaterTank>().sensorAddValue(rate);
            //Debug.Log("WORK"+rate.ToString());

        }


    }

    void OnGUI()
    {
        if (showDebug)
        {
            float msec = deltaTime * 1000.0f;
            float fps = 1.0f / deltaTime;
            string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
            string ggez = "Total " + total_water.ToString() + " /Last " + last.ToString() + " /TotalElm " + total_water_element.ToString() + "/kkk: " + kkkk.ToString() + " / Rate " + rate.ToString() + " <EOF>";
            ggez += "\n" + text;
            GUI.contentColor = Color.black;
            GUI.Label(new Rect(10, 10, 5000, 500), ggez);
        }

    }
}
