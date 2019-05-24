using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTank : MonoBehaviour {
    [Range(0,1)]
    public float Value =0;
    public int maxFrame = 100;
    Animator anim;
    //public Animation anim_clip;
    AnimationState clipState;
    bool isFull = false;

    // Use this for initialization
    void Start()
    {
        //clipState = anim_clip.GetClip("water_fill");'
        anim = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        Value = Mathf.Clamp(Value, 0, (float)maxFrame);
        //print(Value);
        float cleanValue = (Value) *100;
        if (Value <1 && Value >= 0)
        {
            anim.Play("water_fill", 0, (1f / maxFrame) * cleanValue);
            isFull = false;
        }
        else if (Value >= 1)
        {
            Value = 1;
            anim.Play("water_fill", 0, 0.99f);
            isFull = true;
        }
        else
        {
            Value = 0;
            anim.Play("water_fill", 0, 0f);
            isFull = false;
        }
        
        //anim.GetComponent<Animator>()["water_fill"];
        //anim.runtimeAnimatorController.animationClips
        anim.speed = 0.0f;
    }

    public float getValueAsPercent()
    {
        return Value * 100f;
    }

    public void setValue(float val)
    {
        Value = val;
    }
    
    public void setSensorValue(float sensor)
    {
        Value = sensor2percent(sensor);
    }

    float sensor2percent(int ss)
    {
        float ssv = Mathf.Clamp(ss,0,1023);
        float ssv01 = Mathf.Clamp01(ssv / 1023);
        return ssv01;
    }

    float sensor2percent(float ss)
    {
        float ssv = Mathf.Clamp(ss, 0f, 1023f);
        float ssv01 = Mathf.Clamp01(ssv / 1023f);
        return ssv01;
    }
    public void sensorAddValue(int sensor)
    {
        Value += sensor2percent(sensor);
        //Value = Mathf.Clamp(Value, 0, (float)maxFrame);
    }

    public void sensorAddValue(float sensor)
    {
        Value += (sensor/1023);
        Value = Mathf.Clamp(Value, 0, (float)maxFrame);
    }

    public void sensorReduceValue(int sensor)
    {
        Value -= sensor2percent(sensor);
        //Value = Mathf.Clamp(Value, 0, (float)maxFrame);
    }

    public bool isOverFull()
    {
        return isFull;
    }
}
