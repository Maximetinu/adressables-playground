using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu()]
public class UserLevelsSet : ScriptableObject
{
    public AssetReference[] userLevelSceneReferences;
}
