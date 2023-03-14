using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PubNubAPI;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Text;

public class JSONInformation
{
    public string username;
    public string text;
}

public class SendMessage : MonoBehaviour
{
    //seraializes textmeshpro object
    [SerializeField] TextMeshProUGUI textElement;

    public string path = @"C:\Users\Chris R\Documents\schedule.txt";

    //time stuff
    public string time;
    public string time2;

    //basically vars
    public string textDisplay;
    public string logs;
    public string alertDisplay;
    public string randLogs;
    public int random;
    public Text alerter;
    public string alertStore;
    public TMP_Text ooga;
    public string oogaStore;
    private int idRandom;
    public string stuName;
    private int locRan;
    private string loc;
    string[] idList =
    {
        "7415492", "7415503", "7418944"
    };


    //PubNUb stuff
    public static PubNub pubnub;
    public Font customFont;
    public Button SubmitButton;
    public Canvas canvasObject;
    public InputField UsernameInput;
    public InputField TextInput;
    public int counter = 0;
    public int indexcounter = 0;
    public Text deleteText;
    public Text moveTextUpwards;
    private Text text;

    Queue<GameObject> chatMessageQueue = new Queue<GameObject>();

    void Start()
    {


        if (randLogs.Contains("[ALERT]"))
        {
            textDisplay = randLogs;
            logs = textDisplay;
            alerter.text += "\n" + logs;
            ooga.text += "\n" + logs;
        }
        else
        {
            textDisplay = randLogs;
            logs = textDisplay;
            ooga.text += "\n" + logs;
        }

        UsernameInput.text = SystemInfo.deviceName;

        PNConfiguration pnConfiguration = new PNConfiguration();
        pnConfiguration.PublishKey = "pub-c-58ebe2bc-049d-4c3e-b457-427c56a9c60c";
        pnConfiguration.SubscribeKey = "sub-c-549d4fe7-e678-4548-a460-a2fdcdc03857";
        pnConfiguration.LogVerbosity = PNLogVerbosity.BODY;
        pnConfiguration.UserId = System.Guid.NewGuid().ToString();
        pubnub = new PubNub(pnConfiguration);


        // Add Listener to Submit button to send messages
        Button btn = SubmitButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);


        //fetch last X number of messages
        pubnub.FetchMessages()
           .Channels(new List<string> { "chatchannel" })
           .Count(12)
           .Async((result, status) =>
           {
               if (status.Error)
               {
                   Debug.Log(string.Format(
                        " FetchMessages Error: {0} {1} {2}",
                        status.StatusCode, status.ErrorData, status.Category
                    ));
               }
               else
               {
                   foreach (KeyValuePair<string, List<PNMessageResult>> kvp in result.Channels)
                   {
                       foreach (PNMessageResult pnMessageResult in kvp.Value)
                       {
                           // Format data into readable format
                           JSONInformation chatmessage = JsonUtility.FromJson<JSONInformation>(pnMessageResult.Payload.ToString());

                           // Call the function to display the message in plain text
                           CreateChat(chatmessage);

                           // Counter used for positioning the text UI 
                           if (counter != 650)
                           {
                               counter += 50;
                           }
                       }
                   }
               }
           });
        //subcribe to a channel to recieve messages when set
        pubnub.Subscribe()
            .Channels(new List<string>() {
                "chatchannel"
            })
            .WithPresence()
            .Execute();

        // This is the subscribe callback function where data is recieved that is sent on the channel
        pubnub.SubscribeCallback += (sender, e) =>
        {
            SubscribeEventEventArgs message = e as SubscribeEventEventArgs;
            if (message.MessageResult != null)
            {
                // Format data into a readable format
                JSONInformation chatmessage = JsonUtility.FromJson<JSONInformation>(message.MessageResult.Payload.ToString());

                // Call the function to display the message in plain text
                CreateChat(chatmessage);

                // When a new chat is created, remove the first chat and transform all the messages on the page up
                SyncChat();

                // Counter used for position the text UI
                if (counter != 650)
                {
                    counter += 50;
                }
            }
        };
    }

    void Update()
    {


        System.DateTime now = System.DateTime.Now;
        time = now.ToString("HH:mm:ss");
        time2 = "[<color=red>" + time + "</color>]  ";

        TextInput.text = logs;


        //random ID# / name
        idRandom = Random.Range(0, 3);

        if (idRandom == 0) //7415492
        {
            stuName = "Logan Ruby";
            scheduleWrite(0);
        }
        else if (idRandom == 1) //7415503
        {
            stuName = "Zachary Webb";
            scheduleWrite(1);
        }
        else if (idRandom == 2) //7418944
        {
            stuName = "Christopher Racine";
            scheduleWrite(2);
        }
        else
        {
            stuName = "Other";
        }


        locRan = Random.Range(0, 3);

        if (locRan == 0)
        {
            loc = "CLZ Tech HQ.";
        }
        else if (locRan == 1)
        {
            loc = "Room 202 (ANDRE).";
        }
        else if (locRan == 2)
        {
            loc = "Staff Bathroom. [ALERT]";
        }
        else if (locRan == 3)
        {
            loc = "Staff Lounge. [ALERT]";
        }


        //random logs lol\
        randLogs = stuName + " entered " + loc;

    }



    //Student Info Search Stuff
    public void scheduleWrite(int number)
    {
        if (number == 0)
        {
            string lines = "Logan's Schedule lmao";
            File.WriteAllText(path, lines, Encoding.UTF8);
            string readText = File.ReadAllText(path, Encoding.UTF8);
            Debug.Log(readText);
        }
        else if (number == 1)
        {
            string lines = "Zach's Schedule lmao";
            File.WriteAllText(path, lines, Encoding.UTF8);
            string readText = File.ReadAllText(path, Encoding.UTF8);
            Debug.Log(readText);
        }
        else if (number == 2)
        {
            string lines = "1: PercussionEnsem\n" + "2: AP Econ/Gov\n" + "3: AP Statistics\n" + "4: Engineering DesDev\n" + "6: AP Literature";
            File.WriteAllText(path, lines, Encoding.UTF8);
            string readText = File.ReadAllText(path, Encoding.UTF8);
            Debug.Log(readText);

        }
        
    }




        // Function used to create new chat objects based of the data received from PubNub
        public void CreateChat(JSONInformation payLoad)
        {
            // Create a string with the username and text
            string currentObject = string.Concat(payLoad.username, payLoad.text);

            // Create a new gameobject that will display text of the data sent via PubNub
            GameObject chatMessage = new GameObject(currentObject);
            chatMessage.transform.SetParent(canvasObject.GetComponent<Canvas>().transform);
            chatMessage.AddComponent<Text>().text = currentObject;

            // Assign text to the gameobject. Add visual properties to text
            var chatText = chatMessage.GetComponent<Text>();
            chatText.font = customFont;
            chatText.color = UnityEngine.Color.white;
            chatText.fontSize = 15;

            // Assign a RectTransform to gameobject to maniuplate positioning of chat.
            RectTransform rectTransform;
            rectTransform = chatText.GetComponent<RectTransform>();
            rectTransform.localPosition = new Vector2(-10, 430 - counter);
            rectTransform.sizeDelta = new Vector2(650, 50);
            rectTransform.localScale = new Vector3(1F, 1F, 1F);

            // Assign the gameobject to the queue of chatmessages
            chatMessageQueue.Enqueue(chatMessage);

            // Keep track of how many objects we have displayed on the screen
            indexcounter++;
        }

        void SyncChat()
        {
            // If more 12 objects are on the screen, we need to start removing them
            if (indexcounter > 12)
            {
                // Move all existing text gameobjects up the Y axis 50 pixels
                foreach (GameObject moveChat in chatMessageQueue)
                {
                    RectTransform moveText = moveChat.GetComponent<RectTransform>();
                    moveText.offsetMax = new Vector2(moveText.offsetMax.x, moveText.offsetMax.y + 50);
                }
            }
        }

        void TaskOnClick()
        {
            // When the user clicks the Submit button,
            // create a JSON object from input field input
            JSONInformation publishMessage = new JSONInformation();
            publishMessage.username = string.Concat(UsernameInput.text, ": ");
            publishMessage.text = TextInput.text;
            string publishMessageToJSON = JsonUtility.ToJson(publishMessage);

            // Publish the JSON object to the assigned PubNub Channel
            pubnub.Publish()
                .Channel("chatchannel")
                .Message(publishMessageToJSON)
                .Async((result, status) =>
                {
                    if (status.Error)
                    {
                        Debug.Log(status.Error);
                        Debug.Log(status.ErrorData.Info);
                    }
                    else
                    {
                        Debug.Log(string.Format("Publish Timetoken: {0}", result.Timetoken));
                    }
                });

            TextInput.text = "";
        }
    public void test()
    {
        if (randLogs.Contains("[ALERT]"))
        {
            textDisplay = randLogs;
            logs = textDisplay;
            alerter.text += "\n" + time2 + logs;
            ooga.text += "\n" + time2 + logs;
        }
        else
        {
            textDisplay = randLogs;
            logs = textDisplay;
            ooga.text += "\n" + time2 + logs;
        }
        
    }
}
