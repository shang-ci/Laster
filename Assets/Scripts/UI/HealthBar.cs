using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Vector3 offset; //����Ѫ��λ��

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    void Update()
    {
        // ����Ѫ��λ��
        if (slider != null)
        {
            transform.position = transform.parent.position + offset;
        }
    }
}

