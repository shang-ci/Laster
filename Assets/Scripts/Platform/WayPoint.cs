using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public GameObject[] wayPoints;
    private int currentWayPoint = 0;//Ŀǰ��·����
    public float speed;

    // Update is called once per frame
    public void Update()
    {
        //��ƽ̨�ƽ�Ԥ���
        if (Vector2.Distance(wayPoints[currentWayPoint].transform.position, transform.position) < .1f)
        {
            currentWayPoint++;
            if(currentWayPoint >= wayPoints.Length)
            {
                currentWayPoint = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[currentWayPoint].transform.position,Time.deltaTime*speed);
    }
}
