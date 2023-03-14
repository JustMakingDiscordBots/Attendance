/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class logsDisp : MonoBehaviour
{

    //seraializes textmeshpro object
    [SerializeField] TextMeshProUGUI textElement;

    //time ints
    public string time;
    public string time2;

    //basically vars
    public string textDisplay;
    public string logs;
    public string alertDisplay;
    
    // Start is called before the first frame update
    /*void Start()
    {
        //Check for Pi Connection
        Console.WriteLine("[" + time2 + "]  " + "Initialized Connection to Pi.");
        Console.WriteLine("test");
    }

    //updated every frame
    void Update()
    {
        System.DateTime now = System.DateTime.Now;
        time = now.ToString("HH:mm:ss");
        time2 = "[" + time + "]  ";
        

        //for alerts
        if (logs.Contains("[ALERT]") == true)
        {
            textElement.text = logs;
        }
        else
        {

        }
    }
    public void test()
    {
        textDisplay = "ooga booga";
        logs += time2 + textDisplay;
        SendMessage(logs);
     }

    public void Test2()
    {
        textDisplay = "another something idk.";
        logs += time2 + textDisplay;
        SendMessage("text", logs);
    }

   
}*/
