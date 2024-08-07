using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "GameScene/GameSceneSO")]
public class GameSceneSO : ScriptableObject
{
    //游戏资源的引用

    public ScenesType scenesType;
    public AssetReference sceneRefarence;
}
