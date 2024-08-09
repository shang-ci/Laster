using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    [Header("事件监听")]
    public voidEventSO saveDataEvent;
    public voidEventSO loadDataEvent;//死亡后重新开始游戏的实现

    private List<ISacvAble> saveableList = new List<ISacvAble>();

    //临时的data存储数据,存储所有的数据
    private Data saveData;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        saveData = new Data();
    }

    private void OnEnable()
    {
        saveDataEvent.OnEventRaised += Save;
        loadDataEvent.OnEventRaised += Load;
    }

    private void OnDisable()
    {
        saveDataEvent.OnEventRaised -= Save;
        loadDataEvent.OnEventRaised -= Load;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Load();
        }
    }

    //数据注册进来
    public void RegisterSaveData(ISacvAble saveable)
    {
        //如果当前数据不在列表里，就添加进来
        if (!saveableList.Contains(saveable))
        {
            saveableList.Add(saveable);
        }
    }

    //注销数据，从列表中删去
    public void UnRegisterSaveData(ISacvAble saveable) 
    {
        saveableList.Remove(saveable);
    }

    public void Save()
    {
        //遍历所有的要求保存的，即挂载几接口的，让他们保存数据
        foreach (var saveable in saveableList)
        {
            saveable.GetSaveData(saveData);
        }

        foreach (var item in saveData.characterPosdict)
        {
            Debug.Log(item.Key + " " + item.Value);
        }
    }

    public void Load()
    {
        foreach (var saveable in saveableList)
        {
            saveable.LoadData(saveData);
        }
    }

}
