using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cs_05OpenLamp : MonoBehaviour
{

    public GameObject obj_gogoAPI;
    public GameObject Room;

    private GogoAPI gogo;
    private cs_LampController lamp;

    // Start is called before the first frame update
    void Start()
    {
        checkLamp();

        gogo = obj_gogoAPI.GetComponent<GogoAPI>();
    }

    // Update is called once per frame
    void Update()
    {
        
        lamp.isOn = gogo.motors_isOn[0];

        
        
    }

    private void checkLamp()
    {
        if(lamp == null)
        {
            lamp = Room.GetComponent<cs_LampController>();
        }
    }
}
