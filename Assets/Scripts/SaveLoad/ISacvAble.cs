using UnityEngine;

public interface ISacvAble 
{

    //��ÿ���̳иýӿڵĴ��붼��һ��ID
    DataDefinition GetDataID();

    //�Ѵ��иýӿڵĶ�ע�����
    void RegisterSaveData()=> DataManager.instance.RegisterSaveData(this);
    
    void UnRegisterSaveData() => DataManager.instance.UnRegisterSaveData(this);

    void GetSaveData(Data data);

    void LoadData(Data data);
}
