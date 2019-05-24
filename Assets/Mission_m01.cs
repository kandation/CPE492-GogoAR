using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_m01 : MonoBehaviour {
    public GameObject tank;
    public GameObject OK;
    WaterTank wtank;
    public GameObject pump;
    public float fakeValue = 100;
    public float maxValue = -99;

	// Use this for initialization
	void Start () {
        wtank = tank.GetComponent<WaterTank>();
        OK.SetActive(false);
		
	}
	
	// Update is called once per frame
	void Update () {
        
        Debug.Log("VV" + fakeValue );
        
        fakeValue = wtank.getValueAsPercent();
        updateMax(fakeValue);


        bool isYetMax = (maxValue > 0);
        bool isOkk = (fakeValue <= 0f) && pump.active && isYetMax;
        Debug.Log("ISTA" + isOkk);
        if (isOkk)
        {
            OK.SetActive(true);
          
        }
	}

    void updateMax(float value)
    {
        if(value > maxValue)
        { maxValue = value; }
    }
}
