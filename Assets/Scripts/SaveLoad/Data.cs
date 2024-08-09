using System.Collections.Generic;
using UnityEngine;


public class Data 
{
    public string sceneToSave;

    //��������,vector3�޷������л����棬������Ҫ�������������������
    public Dictionary<string, Vector3> characterPosdict = new Dictionary<string, Vector3>();
   
    //����float���͵����ݶ�������������
    public Dictionary<string, float> floatSaveData = new Dictionary<string, float>();

    //ͬ�����Ʊ��䣬ʰȡ��Ʒ������bool������


    //������Ҫ���泡���Ļ� ������������޷��������л��� Ҳ����ֻ������Ϸ���е�ʱ�� ����ʵ������������Ҫ��һ���������罫���������� Ҳ���ǽ������������ ��¼����Ȼ�󴴽�һ���µ� ����objectȻ���������ǵ�scene loader�����¼������Ǵ���������³��� 
    
    public void SaveGameScene(GameSceneSO savedScene)
    {
        //�����ǰ�class��object��ת����string
        sceneToSave = JsonUtility.ToJson(savedScene);
    }

    public GameSceneSO GetSavedScene()
    {
        var newScene = ScriptableObject.CreateInstance<GameSceneSO>();
        //�����л������ݷ�ת��ԭ����
        JsonUtility.FromJsonOverwrite(sceneToSave, newScene);

        return newScene;
    }
}
