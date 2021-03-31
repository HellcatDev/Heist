using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogReader : MonoBehaviour
{
    public static TextFileManager fileManager = new TextFileManager();
    public string fileManagerName;
    public string[] fileManagerContents = new string[0];

    // Start is called before the first frame update
    void Start()
    {
        fileManager.logName = fileManagerName;
        DontDestroyOnLoad(gameObject);
        fileManager.Start();
    }

    private void Update()
    {
        fileManagerContents = fileManager.logContents;
        fileManager.logName = fileManagerName;
    }

    public static string[] ReturnFileContents()
    {
        return fileManager.ReadFileContents(fileManager.logName);
    }

    public static void SaveKeyValuePair(string key, string value)
    {
        fileManager.AddKeyValuePair(fileManager.logName, key, value);
    }

    public static string LoadStringByKey(string key)
    {
        return fileManager.LocateStringByKey(key);
    }

    public static float LoadFloatByKey(string key)
    {
        float f = 0;
        float.TryParse(fileManager.LocateStringByKey(key), out f);
        return f;
    }

    public static int LoadIntByKey(string key)
    {
        int i = 0;
        int.TryParse(fileManager.LocateStringByKey(key), out i);
        return i;
    }

    public static bool LoadBoolByKey(string key)
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
