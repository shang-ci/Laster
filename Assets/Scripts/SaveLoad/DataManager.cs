using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    [Header("�¼�����")]
    public voidEventSO saveDataEvent;
    public voidEventSO loadDataEvent;//���������¿�ʼ��Ϸ��ʵ��

    private List<ISacvAble> saveableList = new List<ISacvAble>();

    //��ʱ��data�洢����,�洢���е�����
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

    //����ע�����
    public void RegisterSaveData(ISacvAble saveable)
    {
        //�����ǰ���ݲ����б������ӽ���
        if (!saveableList.Contains(saveable))
        {
            saveableList.Add(saveable);
        }
    }

    //ע�����ݣ����б���ɾȥ
    public void UnRegisterSaveData(ISacvAble saveable) 
    {
        saveableList.Remove(saveable);
    }

    public void Save()
    {
        //�������е�Ҫ�󱣴�ģ������ؼ��ӿڵģ������Ǳ�������
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
