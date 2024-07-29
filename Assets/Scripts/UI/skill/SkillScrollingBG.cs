using UnityEngine;

public class SkillScrollingBG : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float delay;

    private void Update()
    {
        transform.Translate(-1 * speed * Time.deltaTime, 0, 0);
        if(transform.position.x <= delay)
        {
            transform.position = new Vector2 (0, 0);
        }

    }
}
