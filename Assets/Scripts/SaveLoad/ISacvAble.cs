using UnityEngine;

public interface ISacvAble 
{

    //让每个继承该接口的代码都有一个ID
    DataDefinition GetDataID();

    //把带有该接口的都注册进来
    void RegisterSaveData()=> DataManager.instance.RegisterSaveData(this);
    
    void UnRegisterSaveData() => DataManager.instance.UnRegisterSaveData(this);

    void GetSaveData(Data data);

    void LoadData(Data data);
}
