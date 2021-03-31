using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public string[] contents = new string[0];

    // Start is called before the first frame update
    void Start()
    {
        LogReader.SaveKeyValuePair("Hellcat", "69420");
        LogReader.SaveKeyValuePair("test", "number");
        LogReader.SaveKeyValuePair("FEELING HIGH", "FEELIINGGG HEIGH!");
        LogReader.SaveKeyValuePair("mega mind", "is this somehow working?");
        contents = LogReader.ReturnFileContents();
        contents[0].Remove(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
