using UnityEngine;
using UnityEngine.UI;
public class PlayerState : MonoBehaviour
{
    public Image healthImage;
    public Image healthDelayImage;
    public Image powerImage;

    private void Update()
    {
        if(healthDelayImage.fillAmount > healthImage.fillAmount)
        {
            healthDelayImage.fillAmount -= Time.deltaTime * 0.2f;
        }
    }

    //Ѫ���ٷֱ�
    public void OnHealthChange(float persent)
    {
        Debug.Log(persent);
        healthImage.fillAmount = persent;
    }
}
