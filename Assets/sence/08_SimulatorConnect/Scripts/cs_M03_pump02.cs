using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cs_M03_pump02 : MonoBehaviour {
    [Range(0,1)]
    public float Heath = 100;
    public GameObject Pump02_smoke;

    public GameObject WaterTank_obj;
    private WaterTank watertank;

	// Use this for initialization
	void Start () {
        watertank = WaterTank_obj.GetComponent<WaterTank>();
	}
	
	// Update is called once per frame
	void Update () {
       
	}
}
