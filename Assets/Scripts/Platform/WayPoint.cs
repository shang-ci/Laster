using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public GameObject[] wayPoints;
    private int currentWayPoint = 0;//目前的路径点
    public float speed;

    // Update is called once per frame
    public void Update()
    {
        //当平台逼近预设点
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
