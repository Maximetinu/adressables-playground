using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;

public class GameManager : MonoBehaviour
{
    static Dictionary<int, (string sceneName, string sceneArg, Action<string> loadSceneFunc)> scenes = new Dictionary<int, (string, string, Action<string>)>();

    public static GameManager Instance;

    public SceneReference[] baseGameLevels;
    public AssetReferenceT<UserLevelsSet> userLevelsSetAssetReference;

    void Start()
    {
        if (!LazyInitializeSingleton()) return;
        Debug.Log($"Press [SPACE BAR] to see a list of available scenes.");
        PrintSceneLoaded(gameObject.scene.name);
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
        PrintSceneLoaded(scene.name);
    }

    // Load all possible scenes
    // at this point we'll need to check for adressable scenes
    void CacheAvailableScenes()
    {
        for (int i = 0; i < baseGameLevels.Length; i++)
        {
            scenes.Add(i, (Path.GetFileNameWithoutExtension(baseGameLevels[i].ScenePath), baseGameLevels[i].ScenePath, SceneManager.LoadScene));
        }

        UserLevelsSet userLevelsSet = userLevelsSetAssetReference.LoadAssetAsync<UserLevelsSet>().WaitForCompletion();
        for (int i = 0; i < userLevelsSet.userLevelSceneReferences.Length; i++)
        {
            scenes.Add(i + baseGameLevels.Length, (userLevelsSet.userLevelSceneReferences[i].editorAsset.name, (string)userLevelsSet.userLevelSceneReferences[i].RuntimeKey, (string n) => Addressables.LoadSceneAsync(n)));
        }
    }

    void PrintSceneNavigation()
    {
        Debug.Log("Press a number key to load a scene:");
        foreach (var scn in scenes)
        {
            Debug.Log($"[{scn.Key}] --> {Path.GetFileNameWithoutExtension(scn.Value.sceneName)}");
        }
    }
    
    void PrintSceneLoaded(string sceneName)
    {
        Debug.Log($"Scene {sceneName} loaded");
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
            var selectedScene = scenes[selectedSceneIndex];
            Debug.Log($"Loading scene {selectedScene.sceneName}...\n");
            selectedScene.loadSceneFunc(selectedScene.sceneArg);
        }
    }
}
