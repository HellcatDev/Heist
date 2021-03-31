using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class TextFileManager
{
    public string logName;
    public string[] logContents;

    /// <summary>
    /// In this CreateFile function, we will specify the directory path of the resources folder and read/write text file. We will check to see if the file exists and if it
    /// doesn't, then we will create one and write the filename to it.
    /// </summary>
    /// <param name="fileName"></param>
    public void CreateFile(string fileName)
    {
        string dirPath = Application.dataPath + "/Resources/" + fileName + ".txt";
        if (!File.Exists(dirPath))
        {
            Debug.LogError("sheit dawg it already exists and its still writing a new lines");
            Directory.CreateDirectory(Application.dataPath + "/Resources");
            File.WriteAllText(dirPath, fileName + "\n");
        }
    }

    /// <summary>
    /// In the ReadFileContents function, we will specify the directory path of the resources folder. We will then create a new local variable array called tContents.
    /// Next check to see if the file exists inside of the directory path. If it does, then it will read all of 
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public string[] ReadFileContents(string fileName)
    {
        string dirPath = Application.dataPath + "/Resources/" + fileName + ".txt";
        string[] tContents = new string[0];
        if(File.Exists(dirPath))
        {
            tContents = File.ReadAllLines(dirPath);
        }
        logContents = tContents;
        return tContents;
    }

    /// <summary>
    /// In the AddFileLine function, we will read the file contents of the fileName specified in the parameters. Then we will get the directory path of the resources folder.
    /// We will then check to see if the folder exists in the directory path specified. If it does, then we will append a line to it of the timestamp with the tContents.
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="fileContents"></param>
    public void AddFileLine(string fileName, string fileContents)
    {
        ReadFileContents(fileName);
        string dirPath = Application.dataPath + "/Resources/" + fileName + ".txt";
        string tContents = fileContents + "\n";
        string timeStamp = "Time Stamp: " + System.DateTime.Now;
        if(File.Exists(dirPath))
        {
            File.AppendAllText(dirPath, timeStamp + " - " + tContents);
        }
    }

    /// <summary>
    /// This function is there the add a key value to the file. It will check if the file exists and then run a for loop to check for the logcontents length. Next it will check using an
    /// if statement, if the logContents contains the key parameter specified.
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void AddKeyValuePair(string fileName, string key, string value)
    {
        ReadFileContents(fileName);
        string dirPath = Application.dataPath + "/Resources/" + fileName + ".txt";
        string tContents = key + "," + value;
        string timeStamp = "Time Stamp: " + System.DateTime.Now;
        if(File.Exists(dirPath))
        {
            bool contentsFound = false;
            for(int i = 0; i < logContents.Length; i++)
            {
                if(logContents[i].Contains(key))
                {
                    logContents[i] = timeStamp + " - " + tContents;
                    contentsFound = true;
                }
            }
            if(contentsFound)
            {
                File.WriteAllLines(dirPath, logContents);
            }
            else
            {
                File.AppendAllText(dirPath, timeStamp + " - " + tContents);
            }
        }
    }

    /// <summary>
    /// In this function, we will read the contents of the file and run a foreach loop that checks every character. The foreach loop will then check for a key using an if statement and grab
    /// that key using a string split at "," and add it to the character array. It will then return the string.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string LocateStringByKey(string key)
    {
        ReadFileContents(logName);
        string t = "";
        foreach(string s in logContents)
        {
            if(s.Contains(key))
            {
                string[] splitString = s.Split(",".ToCharArray());
                t = splitString[splitString.Length - 1];
            }
        }
        return t;
    }

    public void Start()
    {
        CreateFile(logName);
        ReadFileContents(logName);
    }
}
