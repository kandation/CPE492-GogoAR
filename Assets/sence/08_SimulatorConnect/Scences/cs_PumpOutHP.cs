using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cs_PumpOutHP : MonoBehaviour {
    public Image HpBar;
    float hp = 0;


    // Use this for initialization
    void Start()
    {

        hp = 1f - (gameObject.GetComponent<Pump_open1>().smokeHP/100);
    }

    // Update is called once per frame
    void Update()
    {
        hp = 1f - (gameObject.GetComponent<Pump_open1>().smokeHP/100);
        HpBar.fillAmount = hp;

    }
}
