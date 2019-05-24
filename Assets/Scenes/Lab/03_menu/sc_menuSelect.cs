using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sc_menuSelect : MonoBehaviour
{



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void gotoSmartFram()
    {
        sc_SceneLoadManager sc_temp = gameObject.AddComponent<sc_SceneLoadManager>();
        sc_temp.LoadScene("sc_farm");
    }

    public void gotoVistualize()
    {
        sc_SceneLoadManager sc_temp = gameObject.AddComponent<sc_SceneLoadManager>();
        sc_temp.LoadScene("06_missionLampRoom");
    }

}
