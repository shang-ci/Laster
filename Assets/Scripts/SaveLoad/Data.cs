using System.Collections.Generic;
using UnityEngine;


public class Data 
{
    public string sceneToSave;

    //数据类型,vector3无法被序列化保存，坐标需要另外的特殊类型来保存
    public Dictionary<string, Vector3> characterPosdict = new Dictionary<string, Vector3>();
   
    //所有float类型的数据都得用它来保存
    public Dictionary<string, float> floatSaveData = new Dictionary<string, float>();

    //同理类似宝箱，拾取物品可以用bool来代表


    //我们想要保存场景的话 正常情况下是无法进行序列化的 也就是只有在游戏运行的时候 才能实现所以呢我们要换一个方法比如将场景的名字 也就是将现在这个场景 记录下来然后创建一个新的 场景object然后呢让我们的scene loader来重新加载我们创建的这个新场景 
    
    public void SaveGameScene(GameSceneSO savedScene)
    {
        //帮我们把class和object的转化成string
        sceneToSave = JsonUtility.ToJson(savedScene);
    }

    public GameSceneSO GetSavedScene()
    {
        var newScene = ScriptableObject.CreateInstance<GameSceneSO>();
        //将序列化的内容翻转回原内容
        JsonUtility.FromJsonOverwrite(sceneToSave, newScene);

        return newScene;
    }
}
