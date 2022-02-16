using UnityEngine;

public class RuntimeLogGUI : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod]
    static void Initialize()
    {
        GameObject logGuiObj = new GameObject("Log GUI");
        logGuiObj.AddComponent<RuntimeLogGUI>();
        logGuiObj.hideFlags = HideFlags.HideInHierarchy;
        GameObject.DontDestroyOnLoad(logGuiObj);
    }
    
    string log;

    void OnEnable()
    {
        Application.logMessageReceived += Log;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= Log;
    }

    void OnGUI()
    {
        const float padding = 10f;
        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.LowerLeft;
        style.normal.textColor = Color.white;
        GUI.Label(new Rect(padding * 0.5f, padding * 0.5f, Screen.width - padding, Screen.height - padding), log, style);
    }

    void Log(string logString, string stackTrace, LogType type)
    {
        log =  log + "\n" + logString;
    }
}