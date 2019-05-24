using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using UnityEngine.UI;
using System;
using System.Net;
using System.Net.Sockets;

public class testWS : MonoBehaviour {
    public Text text;
    //public StateMachinestateMachine;
    public WebSocket ws;
    private string ss = "rr";

    public Button buttOk;
    public InputField textfield;
    private string ipbox = "127.0.0.1";

    public GameObject led;
    Renderer rend;
    int lecint = 0;


    // Use this for initialization
    void Start () {

        ws = new WebSocket("ws://127.0.0.1:8316/ws");
        ws.OnMessage += OnMessage;
        ws.Connect();
        text.text = "Not Connect";
        rend  = led.GetComponent<Renderer>();


    }
	
	// Update is called once per frame
	void Update () {
        
        text.text = ipbox  + "##eee" + ss;
        buttOk.onClick.AddListener(clickIP);

        if (lecint > 500)
        {
            //    rend.material.shader = Shader.Find("_Color");
            rend.material.SetColor("_Color", Color.green);
        }
        else
        {
            rend.material.SetColor("_Color", Color.red);
        }





    }

    void clickIP()
    {
        ipbox = textfield.text;
        ws.Close();
        ws = new WebSocket("ws://"+ ipbox + ":8316/ws");
        ws.OnMessage += OnMessage;
        ws.Connect();


    }

   

    void OnMessage(object sender, MessageEventArgs e)

    {
      
        string[] stringSeparators = new string[] { "::" };
        
        string[] wsText = e.Data.ToString().Split('-');
        ss = wsText[1] + "###"+ LocalIPAddress();
        string sensor = wsText[1];
        Debug.Log(ss+ "WWWWC" + e.Data);
        changeLED(sensor);



    }
    public string LocalIPAddress()
    {
        IPHostEntry host;
        string localIP = "";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
                break;
            }
        }
        return localIP;
    }

    private void changeLED(string sensor)
    {
        int x = 0;

        if (!Int32.TryParse(sensor, out x))
        {
            Debug.Log("Errorrrrrr PaseInt");
        }
        lecint = x;





    }

    void webSocketClient_OnOpen(object sender, EventArgs e)
    {
        Debug.Log("OnOpen!");
     
    }
}
