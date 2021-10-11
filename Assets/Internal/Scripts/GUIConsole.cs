using System;
using System.IO;
using UnityEngine;

public class GUIConsole : MonoBehaviour
{
    string myLog = "*begin log";
    string filename = "";
    bool doShow = false;
    int kChars = 700;
    float deltaTime = 0.0f;

    void OnEnable() { Application.logMessageReceived += Log; }
    void OnDisable() { Application.logMessageReceived -= Log; }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { doShow = !doShow; }
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    public void Log(string logString, string stackTrace, LogType type)
    {
        // for onscreen...
        myLog = myLog + "\n" + DateTime.Now.ToString("HH:mm:ss ") + logString;
        if (myLog.Length > kChars) { myLog = myLog.Substring(myLog.Length - kChars); }

        // for the file ...
        if (filename == "")
        {
            string d = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/LOGS";
            Directory.CreateDirectory(d);
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd\\--HH:mm:ss");
            filename = d + "/log-" + dateTime + ".txt";
        }
        try { File.AppendAllText(filename, "\n" + DateTime.Now.ToString("HH:mm:ss ") + logString); }
        catch { }
    }

    void OnGUI()
    {
        if (!doShow) { return; }
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity,
           new Vector3(Screen.width / 800.0f, Screen.height / 800.0f, 1.0f));
        GUI.TextArea(new Rect(10, 60, 540, 370), myLog);

        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
}