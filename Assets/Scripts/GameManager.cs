using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static Dictionary<int, string> scenes;

    public static GameManager Instance;
    
    void Start()
    {
        if (!LazyInitializeSingleton()) return;
        PrintUserInfo(gameObject.scene.name);
        SceneManager.sceneLoaded += OnSceneLoaded;
        CacheAvailableScenes();
    }

    bool LazyInitializeSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            return true;
        }
        else
        {
            Destroy(gameObject);
            return false;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == "DontDestroyOnLoad") return;
        PrintUserInfo(scene.name);
    }

    // Load all possible scenes
    // at this point we'll need to check for adressable scenes
    void CacheAvailableScenes()
    {
        scenes = new Dictionary<int, string>();
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            scenes.Add(i, SceneUtility.GetScenePathByBuildIndex(i));
        }
    }

    void PrintSceneNavigation()
    {
        Debug.Log("Press a number key to load a scene:");
        foreach (var scn in scenes)
        {
            Debug.Log($"[{scn.Key}] --> {Path.GetFileNameWithoutExtension(scn.Value)}");
        }
    }
    
    void PrintUserInfo(string sceneName)
    {
        Debug.Log($"Scene {sceneName} loaded");
        Debug.Log($"Press [SPACE BAR] to see a list of available scenes.");
    }

    int GetNumberPressed()
    {
        for (int i = 0; i < 10; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
                return i;
        }
        
        return -1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PrintSceneNavigation();
        }

        if (scenes.ContainsKey(GetNumberPressed()))
        {
            int selectedSceneIndex = GetNumberPressed();
            var selectedScenePath = scenes[selectedSceneIndex];
            Debug.Log($"Loading scene {selectedScenePath}...");
            SceneManager.LoadScene(selectedSceneIndex);
        }
    }
}
