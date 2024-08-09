using UnityEngine;

//数据定义，给数据加上唯一标识
public class DataDefinition : MonoBehaviour
{
    public PersisitentType persisitentType;
    public string ID;

    //自带的，当数据发生变化时会自动调用
    private void OnValidate()
    {
        //添加persistentType是用来防止每次启动或暂停会重新分配一个ID
        if(persisitentType == PersisitentType.ReadWrite)
        {
            if (ID == string.Empty)
            {
                //系统帮我们生成唯一的标识
                ID = System.Guid.NewGuid().ToString();
            }
        }
        else
        {
            ID = string.Empty;
        }
        
    }
}
