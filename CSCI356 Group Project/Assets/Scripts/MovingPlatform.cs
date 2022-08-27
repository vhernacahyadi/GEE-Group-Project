using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private GameObject[] wayPoints;

    [SerializeField]
    private float speed;

    private int currentWayPointIndex = 0;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, wayPoints[currentWayPointIndex].transform.position) < 0.1f)
        {
            currentWayPointIndex++;
            if (currentWayPointIndex >= wayPoints.Length)
            {
                currentWayPointIndex = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[currentWayPointIndex].transform.position, speed * Time.deltaTime);
    }
}
