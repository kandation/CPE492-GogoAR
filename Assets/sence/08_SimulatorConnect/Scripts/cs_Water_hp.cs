using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cs_Water_hp : MonoBehaviour {
    public Image HpBar;
    float hp = 0;


	// Use this for initialization
	void Start () {

        hp = gameObject.GetComponent<WaterTank>().Value;
	}
	
	// Update is called once per frame
	void Update () {
        hp = gameObject.GetComponent<WaterTank>().Value;
        HpBar.fillAmount = hp;

    }
}
