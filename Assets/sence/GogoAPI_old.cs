using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using WebSocketSharp;
using UnityEngine.UI;

public class GogoAPI_old : MonoBehaviour {
    private WebSocket ws;
    private JSONNode json;
    public bool isConnected = false;
    public int[] sensor = { 0, 0, 0, 0, 0, 0, 0, 0 };
    public bool[] motors_isOn = { false, false, false, false };
    public bool[] motors_isActive = { false, false, false, false };
    public int[] motors_power = {0,0,0,0};
    public int[] motors_direction = { 0, 0, 0, 0 };

    public int boardType = 0;
    public int boardVersion = 0;
    public int firmwareVersion = 0;

    public string ComputerIP = "127.0.0.1";

    public bool showDebug = false;
    public string gogoWarning = "";
    public string motor_status_str = "";
    public string sensor_status_str = "";

    public string tempTextIP;

    struct Motor
    {
        public int direction;
        public bool isOn;
        public bool isActive;
        public int power;
    }

    struct GogoInfo
    {
        public int boardType;
        public int boardVersion;
        public int firmwareVersion;
    }

    Motor[] motors;
    GogoInfo info;

    public bool socketStatus = false;
    



    // Use this for initialization
    void Start () {
        Debug.Log("INING");
        motors = new Motor[4];
        info = new GogoInfo();
        initMotors();
        initInfo();
        connect();
	}
    
	
	// Update is called once per frame
	void Update () {
        motor_status_str = "";
        sensor_status_str = "";

        for (int i = 0; i < sensor.Length; i++)
        {
            sensor_status_str += +i+"-->"+sensor[i] + " ";
        }
        
        for (int i=0; i< motors.Length; i++)
        {
            motors_isOn[i] = motors[i].isOn;
            motors_isActive[i] = motors[i].isActive;
            motors_power[i] = motors[i].power;
            motors_direction[i] = motors[i].direction;
            motor_status_str += motors[i].isOn.ToString();
        }

        boardType = info.boardType;
        boardVersion = info.boardVersion;
        firmwareVersion = info.firmwareVersion;

        if (CheckConnected())
        {
            gogoWarning = "GogoBoard <color=lime>Connected</color> Ver.<color=yello>" + info.firmwareVersion+"</color>";
            isConnected = true;
        }
        else
        {
            gogoWarning = "GogoBoard <color=red>DIsconnected</color>";
            if (!socketStatus)
            {
                gogoWarning += "\nand Websocket <color=red>Disconnected</color>";
            }
            isConnected = false;
            
        }

    }

    void OnGUI()
    {
        if (showDebug)
        {
            GUI.Label(new Rect(10, 10, 500, 500), "Motor: "+motor_status_str);
            GUI.Label(new Rect(10, 30, 500, 500), "Sensor: " + sensor_status_str);
            GUI.Label(new Rect(10,50, 500, 500), gogoWarning);
        }
        

    }

    void initInfo()
    {
        info.boardType = 0;
        info.boardVersion = 0;
        info.firmwareVersion = 0;
    }

    void initMotors()
    {
        for(int i = 0; i < 4; i++)
        {
            motors[i].direction = 1;
            motors[i].isOn = false;
            motors[i].isActive = false;
            motors[i].power = 0;
        }
    }

    public void connect()
    {
        
        try
        {
            gogoWarning = "<color=yellow>Try Connect Websocket</color>";
            string uri = "ws://"+ComputerIP+":8317/json";
            ws = new WebSocket(uri);
            ws.OnMessage += OnMessage;
            ws.Connect();
            gogoWarning = "<color=lime>Websocket is Connected</color>";
            Debug.Log("Gogo Api COnnected Working");


        }
        catch(System.Exception e)
        {
            gogoWarning = "<size=20><color=red>Error!!</color></size><size=5>"+e+"</size>";
        }
        
        
    }

    public void CloseConnection()
    {
        Debug.Log("UnityGogo Socket is try to Closed!");
        gogoWarning = "<size=14><color=yellow>Websocket workiing hard to close</color></size>";
 
        ws.CloseAsync();
        ws.Close();
        
        gogoWarning = "<color=silver>Socket Close</color>";
        Debug.Log("UnityGogo Socket is Closed!");
    }
    public void sendCommand(string cmd)
    {
        if(ws != null)
        {
            ws.Send(cmd);
        }
        
    }

    public void ReConnect()
    {
        initMotors();
        initInfo();
        CloseConnection();
        socketStatus = false;
        setReconnectText();
        gogoWarning = "<color=orange>Reconnect on IP</color><color=white>" + ComputerIP+"</color>";        
        connect();
        gogoWarning = "<color=lime>Reconnected OK</color>";
    }

    public void setGogoMainIP(InputField input)
    {
        if (input.text.Length > 0)
        {
            //gogoWarning = "Get IP is "+ input.text;
            tempTextIP = input.text;
        }
    }

    private void setReconnectText()
    {
        gogoWarning = "<size=14><color=yellow>Websocket Set new IP</color></size>";
        ComputerIP = tempTextIP;
    }

    void OnMessage(object sender, MessageEventArgs e)
    {
        json = JSON.Parse(e.Data.ToString());
        int type = json["type"].AsInt;
        if(type == 0)
        {
            socketStatus = true;
            UpdateSensor(json.ToString());
        }
        
    }

    private void UpdateSensor(string _json)
    {
        json = JSON.Parse(_json);
        JSONNode data = json["data"];

        JSONNode _info = data["info"];
        info.boardType = _info["boardType"];
        info.boardVersion = _info["boardVersion"];
        info.firmwareVersion = _info["firmwareVersion"];


        JSONNode _sensor = data["sensors"];
        for(int i=0; i<_sensor.Count;i++)
        {            
            sensor[i] =  int.Parse(_sensor[i].ToString());
        }
        JSONNode _jmotors = data["motors"];

        for(int i = 0; i < _jmotors.Count; i++)
        {
            JSONNode m = _jmotors[i];
            motors[i].direction = getDirection(m["direction"].ToString());
            motors[i].isOn = getMotorIsOn(m["isOn"].ToString());            
            motors[i].isActive = getMotorIsOn(m["isActive"].ToString());
            motors[i].power = int.Parse(m["power"].ToString());
        }


    }

    bool getMotorIsOn(string s)
    {
        return (s.Equals("0")) ? false : true;
    }

    int getDirection(string d)
    {
        int dir = 1;
        if (d.Equals("\"ccw\""))
        {
            dir = -1;
        }
        else if(d.Equals("\"cw\""))
        {
            dir = 1;
        }
        else
        {
            dir = 0;
        }
        return dir;
    }

   private bool CheckConnected()
    {
        return (info.boardVersion != 0);
    }
}
