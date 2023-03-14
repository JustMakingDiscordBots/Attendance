using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class logsController : MonoBehaviour
{
    //time ints
    public string time;
    public string time2;
    //date ints
    public string date;
    public string date2;

    //color stuff
    private string dcolor;
    private string tcolor;
    private string[] colors = new string[] {"White", "Dark Red", "Red", "Blue", "Light Blue", "Green", "Light Green"};

    //TMP Stuff
    public TMP_Dropdown dColorDrop;
    public TMP_Dropdown tColorDrop;
    public TextMeshProUGUI clock;
    public TextMeshProUGUI dayte;

    //Settings Menu
    public GameObject settingsMenu;
    public GameObject mainScreen;


    void Start()
    {
        //DATE DROPDOWN
        dColorDrop.ClearOptions();
        dWhite();

        List<string> options = new List<string>();

        int currentDColor = 0;
        for(int i = 0; i < colors.Length; i++)
        {
            Debug.Log(colors[i]);
            options.Add(colors[i]);
        }

        dColorDrop.AddOptions(options);
        dColorDrop.value = currentDColor;
        dColorDrop.RefreshShownValue();

        ///////////////////////////////////////////////////

        //TIME DROPDOWN
        tColorDrop.ClearOptions();
        tWhite();

        List<string> option = new List<string>();

        int currentTColor = 0;
        for (int i = 0; i < colors.Length; i++)
        {
            option.Add(colors[i]);
        }

        tColorDrop.AddOptions(option);
        tColorDrop.value = currentTColor;
        tColorDrop.RefreshShownValue();
    }



    //Called every frame
    void Update()
    {
        //usage setup
        System.DateTime now = System.DateTime.Now;
        System.DateTime today = System.DateTime.Now;

        //implementation of date
        date = today.ToString("dddd\n\n MMMM dd, yyyy");
        date2 = "<color=" + dcolor + ">" + date + "</color>";
        Debug.Log(date2);
        dayte.text = date2;

        //implementation of time
        time = now.ToString("h:mm:ss tt");
        time2 = "<color=" + tcolor + ">" + time + "</color>";
        clock.text = time2;
    }

    //dropdown
    public void OnDropDownChanged(TMP_Dropdown dropDown)
    {
        Debug.Log("DROP DOWN CHANGED -> " + dropDown.value);
    }

    public void backButton()
    {
        SceneManager.LoadScene("Main");
    }
    public void x()
    {
        settingsMenu.SetActive(false);
        mainScreen.SetActive(true);
    }

    public void settingsButton()
    {
        settingsMenu.SetActive(true);
        mainScreen.SetActive(false);
    }

    public void dSetColor (TMP_Dropdown colorIndex)
    {
        Debug.Log("at SetColor()");
        if(colorIndex.value == 0)
        {
            dWhite();
        }
        else if (colorIndex.value == 1)
        {
            dDarkRed();
            Debug.Log("got here!");
        }
        else if (colorIndex.value == 2)
        {
            dRed();
        }
        else if (colorIndex.value == 3)
        {
            dBlue();
        }
        else if (colorIndex.value == 4)
        {
            dLightBlue();
        }
        else if (colorIndex.value == 5)
        {
            dGreen();
        }
        else if (colorIndex.value == 6)
        {
            dLightGreen();
        }
        else
        {
            Debug.Log("brokie lmao you retard");
        }
    }

    public void tSetColor(TMP_Dropdown colorI)
    {
        Debug.Log("at SetColor()");
        if (colorI.value == 0)
        {
            tWhite();
        }
        else if (colorI.value == 1)
        {
            tDarkRed();
        }
        else if (colorI.value == 2)
        {
            tRed();
        }
        else if (colorI.value == 3)
        {
            tBlue();
        }
        else if (colorI.value == 4)
        {
            tLightBlue();
        }
        else if (colorI.value == 5)
        {
            tGreen();
        }
        else if (colorI.value == 6)
        {
            tLightGreen();
        }
        else
        {
            Debug.Log("brokie lmao you retard");
        }
    }


    //CoLoRS   REQUIRES ONE  **tColor()**  AND ONE  **dColor()**  
    public void tWhite()
    {
        tcolor = "#ffffff";
    }
    public void dWhite()
    {
        dcolor = "#ffffff";
    }


    public void tDarkRed()
    {
        tcolor = "#630f0f";
    }
    public void dDarkRed()
    {
        dcolor = "#630f0f";
    }


    public void tRed()
    {
        tcolor = "#ff0000";
    }
    public void dRed()
    {
        dcolor = "#ff0000";
    }


    public void tBlue()
    {
        tcolor = "#0000ff";
    }
    public void dBlue()
    {
        dcolor = "#0000ff";
    }


    public void tLightBlue()
    {
        tcolor = "#00a6ff";
    }
    public void dLightBlue()
    {
        dcolor = "#00a6ff";
    }


    public void tGreen()
    {
        tcolor = "#095c05";
    }
    public void dGreen()
    {
        dcolor = "#095c05";
    }


    public void tLightGreen()
    {
        tcolor = "#0cff00";
    }
    public void dLightGreen()
    {
       dcolor = "#0cff00";
    }
}
