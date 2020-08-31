using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogReader : MonoBehaviour
{
    public TextFileManager fileManager = new TextFileManager();

    // Start is called before the first frame update
    void Start()
    {
        fileManager.Start();
    }

    public void SaveKeyValuePair(string key, string value)
    {
        fileManager.AddKeyValuePair(fileManager.logName, key, value);
    }

    public string LoadStringByKey(string key)
    {
        return fileManager.LocateStringByKey(key);
    }

    public float LoadFloatByKey(string key)
    {
        float f = 0;
        float.TryParse(fileManager.LocateStringByKey(key), out f);
        return f;
    }

    public float LoadIntByKey(string key)
    {
        float i = 0;
        float.TryParse(fileManager.LocateStringByKey(key), out i);
        return i;
    }

    public bool LoadBoolByKey(string key)
    {
        string v = fileManager.LocateStringByKey(key);
        if(v == "True" || v == "true")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
