using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTank_percentext : MonoBehaviour {
    public GameObject tank;
    public TextMesh percentext;

	// Use this for initialization
	void Start () {
        
        //percentext = gameObject.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
               
        percentext.text = Mathf.Clamp(tank.GetComponent<WaterTank>().getValueAsPercent(), 0, 100).ToString()+ " %";

    }
}
