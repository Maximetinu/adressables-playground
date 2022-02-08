using UnityEngine;

public class RuntimeLogGUI : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod]
    static void Initialize()
    {
        GameObject.DontDestroyOnLoad(new GameObject("Log GUI").AddComponent<RuntimeLogGUI>());
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
        GUI.Label(new Rect(5, 5, Screen.width - 10, Screen.height - 10), log);
    }

    void Log(string logString, string stackTrace, LogType type)
    {
        log = logString + "\n" + log;
    }
}