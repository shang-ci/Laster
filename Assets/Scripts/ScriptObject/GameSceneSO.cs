using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "GameScene/GameSceneSO")]
public class GameSceneSO : ScriptableObject
{
    //��Ϸ��Դ������

    public ScenesType scenesType;
    public AssetReference sceneRefarence;
}
