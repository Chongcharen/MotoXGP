using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIDebug : MonoSingleton<GUIDebug>
{
    static GUIDebug instance;
    public Vector2 scrollPosition = Vector2.zero;
    static string longString = "This is a long-ish string";
    GUIStyle style;
    GUIStyle messageStyle;

    bool showGUI = false;
    public void Init(){
        style = new GUIStyle();
        style.fontSize = 15;

        messageStyle = new GUIStyle();
        messageStyle.normal.textColor = Color.green;
        messageStyle.fontStyle = FontStyle.Bold;
        messageStyle.fontSize = 15;
    }
    void Update(){
        // if(Input.GetKeyDown(KeyCode.Alpha1)){
        //     showGUI = !showGUI;
        // }
    }
    void OnGUI(){
        if(!showGUI)return;
        // Begin a scroll view. All rects are calculated automatically -
        // it will use up any available screen space and make sure contents flow correctly.
        // This is kept small with the last two parameters to force scrollbars to appear.
        scrollPosition = GUILayout.BeginScrollView(
            new Vector2(100,100), GUILayout.Width(Screen.width*.8f), GUILayout.Height(Screen.height*.5f));
         if (GUILayout.Button("Clear"))
            longString = "";

        // We just add a single label to go inside the scroll view. Note how the
        // scrollbars will work correctly with wordwrap.
        GUILayout.Label(longString,messageStyle);

        // Add a button to clear the string. This is inside the scroll area, so it
        // will be scrolled as well. Note how the button becomes narrower to make room
        // for the vertical scrollbar
       

        // End the scrollview we began above.
        GUILayout.EndScrollView();

        // Now we add a button outside the scrollview - this will be shown below
        // the scrolling area.
        if (GUILayout.Button("Add More Text"))
            longString += "\nHere is another line";
    }
    public static void Log(string message){
        longString += "\n"+message;
    }
}