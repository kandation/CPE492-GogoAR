using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using SimpleJSON;

public class GogoAPIStream : MonoBehaviour {
    private WebSocket ws;
    private JSONNode json;
    public int[] Sensor = { 0, 0, 0, 0, 0, 0, 0, 0 };

    // Use this for initialization
    void Start () {
        Debug.Log("Dd");
        ws = new WebSocket("ws://localhost:8317");
        ws.OnMessage += OnMessage;
        ws.Connect();
    }
	
	// Update is called once per frame
	void Update () {
        
	}
    public void sendLed()
    {
        ws.Send("cmd::ledOn");
        Debug.Log("OKSEND");
    }

    void OnMessage(object sender, MessageEventArgs e)
    {
        Debug.Log("++++++");
    
        json = JSON.Parse(e.Data.ToString());
        JSONArray ls = json["stream"].AsArray;
        

        if (ls[0] == 0)
        {
            Sensor[0] = Merge2Address(ls[1], ls[2]);
            Sensor[1] = Merge2Address(ls[3], ls[4]);
            Sensor[2] = Merge2Address(ls[5], ls[6]);
            Sensor[3] = Merge2Address(ls[7], ls[8]);
            Sensor[4] = Merge2Address(ls[9], ls[10]);
            Sensor[5] = Merge2Address(ls[11], ls[12]);
            Sensor[6] = Merge2Address(ls[13], ls[14]);
            Sensor[7] = Merge2Address(ls[15], ls[16]);

            Debug.Log(ls);
            Debug.Log(Sensor);
        }



    }

    int Merge2Address(int a, int b)
    {
        return (a << 8)+b;
    }
}
