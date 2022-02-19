using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

[CreateAssetMenu()]
public class UserLevelsSet : ScriptableObject
{
    [SerializeField]
    AssetReference[] userLevelSceneReferences;

    public int Count => userLevelSceneReferences.Length;

    string[] sceneNames;

    // Initialize Adressables and cache adressable scene names
    void OnEnable()
    {
        var resourceLocator = Addressables.InitializeAsync().WaitForCompletion();
        sceneNames = new string[userLevelSceneReferences.Length];
        for (int i = 0; i < userLevelSceneReferences.Length; i++)
        {
            IList<IResourceLocation> locs;
            resourceLocator.Locate(userLevelSceneReferences[i].AssetGUID, typeof(UnityEngine.ResourceManagement.ResourceProviders.SceneInstance), out locs);
            sceneNames[i] = Path.GetFileNameWithoutExtension(locs[0].PrimaryKey);
        }
    }

    public void LoadScene(int i)
    {
        Addressables.LoadSceneAsync(GetSceneAddressableArg(i));
    }

    public string GetSceneName(int i)
    {
        return sceneNames[i];
    }

    private string GetSceneAddressableArg(int i)
    {
        return (string)userLevelSceneReferences[i].RuntimeKey;
    }
}
