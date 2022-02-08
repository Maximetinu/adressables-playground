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
        const float padding = 10f;
        GUI.Label(new Rect(padding * 0.5f, padding * 0.5f, Screen.width - padding, Screen.height - padding), log);
    }

    void Log(string logString, string stackTrace, LogType type)
    {
        log = logString + "\n" + log;
    }
}