using UnityEngine;

//���ݶ��壬�����ݼ���Ψһ��ʶ
public class DataDefinition : MonoBehaviour
{
    public PersisitentType persisitentType;
    public string ID;

    //�Դ��ģ������ݷ����仯ʱ���Զ�����
    private void OnValidate()
    {
        //���persistentType��������ֹÿ����������ͣ�����·���һ��ID
        if(persisitentType == PersisitentType.ReadWrite)
        {
            if (ID == string.Empty)
            {
                //ϵͳ����������Ψһ�ı�ʶ
                ID = System.Guid.NewGuid().ToString();
            }
        }
        else
        {
            ID = string.Empty;
        }
        
    }
}
