using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class sc_SaveIPAddress : MonoBehaviour {
    public bool DebugMode = false;
    string data = "127.0.0.1";
    string debugString = "";

    public string getLoadFIle()
    {
        LoadFileIP();
        return data;
    }


    public void SaveFileIP()
    {
        string destination = Application.persistentDataPath + "/gogoIpAddress.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("SaveFile Success");
    }

    public void LoadFileIP()
    {
        string destination = Application.persistentDataPath + "/gogoIpAddress.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("File not found");
            debugString = "<color=red>File not found</color>";
            data = "127.0.0.1";
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        data = (string)bf.Deserialize(file);
        file.Close();

    }
    public void setData(string ip)
    {
        data = ip;
    }
    public string getData()
    {
        return data;
    }

    void OnGUI()
    {
        if (DebugMode)
        {
            GUI.Label(new Rect(10, 10, 500, 20), data);

        }
        if (!debugString.Equals(""))
        {
            GUI.Label(new Rect(Screen.width / 2, 50, 500, 20), debugString);
        }

    }
}
