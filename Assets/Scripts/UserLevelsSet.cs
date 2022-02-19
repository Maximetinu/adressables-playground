using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu()]
public class UserLevelsSet : ScriptableObject
{
    [SerializeField]
    AssetReference[] userLevelSceneReferences;

    public int Count => userLevelSceneReferences.Length;

    public void LoadScene(int i)
    {
        Addressables.LoadSceneAsync(GetSceneAddressableArg(i));
    }

    // regex to extract "UserLevel_00" from "[f895eda0324951e43b3b2b97e08cfdfd]UserLevel_00 (UnityEngine.SceneAsset)"
    // this has to be possible with Addressables API, but I didn't find how
    public string GetSceneName(int i)
    {
        AssetReference sceneRef = userLevelSceneReferences[i];
        string guid_sceneName_type = sceneRef.ToString();
        return Regex.Match(guid_sceneName_type, @"(?<=\]).*?(?= \()").Value;
    }

    private string GetSceneAddressableArg(int i)
    {
        return (string)userLevelSceneReferences[i].RuntimeKey;
    }
}
