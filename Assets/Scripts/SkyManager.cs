using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyManager : MonoBehaviour {

    ObjectPool[] pools;
    float vSize;
    float hSize;

    void Start()
    {
        vSize = Camera.main.orthographicSize;
        hSize = vSize * Camera.main.aspect;

        pools = GetComponents<ObjectPool>();
        Invoke("Spawn", Random.Range(2.0f, 3.0f));
    }

    void Spawn()
    {
        int cloudType = (int)Random.Range(0, pools.Length - (float)1e-6);
        GameObject cloud = pools[cloudType].GetUnused();
        CameraFollower cf = cloud.GetComponent<CameraFollower>();

        cf.target = GameObject.FindGameObjectWithTag("Player").transform;
        cf.gap = hSize + 2.0f * cloud.GetComponent<SpriteRenderer>().sprite.bounds.extents.x;
        cf.lag = cf.lag + Random.Range(-0.02f, 0.02f);

        cloud.transform.position = new Vector3(-2*hSize, Random.Range(vSize/8, vSize), 0);
        cloud.SetActive(true);

        Invoke("Spawn", Random.Range(2.0f, 3.0f));
    }
}
