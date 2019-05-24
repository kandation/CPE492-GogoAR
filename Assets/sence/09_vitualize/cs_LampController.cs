using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cs_LampController : MonoBehaviour

{
    public bool isPause = false;
    public bool isOn = false;
    public GameObject obj_Lamp;
    public GameObject obj_Light;

    private Renderer lamp_render; 


    // Start is called before the first frame update
    void Start()
    {
        lamp_render = obj_Lamp.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            lamp_render.material.EnableKeyword("_EMISSION");
            obj_Light.SetActive(true);
        }
        else
        {
            lamp_render.material.DisableKeyword("_EMISSION");
            obj_Light.SetActive(false);
        }
        
    

    }
}
