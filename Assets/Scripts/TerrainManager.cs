using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TerrainManager : MonoBehaviour {

    ObjectPool[] pools;
    GameObject lastPlatform = null;

    float lastLocation = 0;

	void Start ()
    {
        pools = GetComponents<ObjectPool>();
        Spawn();
	}
	
	// Update is called once per frame
	void Spawn ()
    {
        int platformType = (int)Random.Range(0, pools.Length - (float)1e-6);
        GameObject platform = pools[platformType].GetUnused();

        float xOffset = Random.Range(10, 17) + platform.GetComponent<Collider2D>().bounds.extents.x;
        if (lastPlatform != null)
        {
            xOffset += lastPlatform.GetComponent<Collider2D>().bounds.extents.x;
        }
        platform.transform.position = transform.position + new Vector3(lastLocation + xOffset, Random.Range(-3, 3), 0);
        lastLocation += xOffset;
        platform.SetActive(true);
        lastPlatform = platform;

        Invoke("Spawn", 1f);
	}
}
