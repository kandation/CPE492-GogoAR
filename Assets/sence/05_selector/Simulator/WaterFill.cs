using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFill : MonoBehaviour {
    [Range(0.1f, 10f)]
    public float TimeToProcess = 0;
    public float FillSpeed = 1;
    public GameObject tank;
    public GameObject motor;
    private float lastAddWater = 0;
    private float lastRemPump = 0;

    float total_water = 0;
    float total_water_element = 0;
    float last = 0;

    float kkkk=-1;


    float deltaTime = 0.0f;
    float rate = 0;

    public bool showDebug = false;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        float timeInterupt = Time.time - lastAddWater;
        if (timeInterupt >= TimeToProcess)
        {
            total_water_element = 0;
            if (tank.active)
            {
                total_water_element -= 50f;
            }
            if (motor.active)
            {
                total_water_element += 200f;
            }
            last = tank.GetComponent<WaterTank>().getValueAsPercent();
            total_water = last - total_water_element;
            lastAddWater = Time.time;
            //Debug.Log("GG");


        }
        else
        {
            //kkkk = Mathf.LerpUnclamped(1f, 10000f, 10f);
            rate = total_water / 60f;
            kkkk += 1;
            tank.GetComponent<WaterTank>().sensorAddValue(rate);
            //Debug.Log("WORK");

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
