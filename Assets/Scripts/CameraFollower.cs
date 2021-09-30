using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{

    public Transform target;
    public float gap = 5;
    public float lag = 0;

    private float accLag;

    void Start()
    {
        accLag = lag;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.position.x + gap - accLag, transform.position.y, transform.position.z);
            accLag += lag;
        }
    }
}
