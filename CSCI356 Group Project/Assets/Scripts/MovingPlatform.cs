using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] GameObject[] wayPoints;
    int currentWayPointIndex = 0;
    [SerializeField] float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, wayPoints[currentWayPointIndex].transform.position)<0.1f)
        {
            currentWayPointIndex++;
            if(currentWayPointIndex>=wayPoints.Length)
            {
                currentWayPointIndex = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[currentWayPointIndex].transform.position, speed * 5.0f * Time.deltaTime);
    }
}