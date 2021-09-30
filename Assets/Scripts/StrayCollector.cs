using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrayCollector : MonoBehaviour {

    private Collider2D colliderBox;

	// Use this for initialization
	void Start () {
        colliderBox = this.GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        var platforms = GameObject.FindGameObjectsWithTag("Platform");
        // Debug.Log(platforms.Length);
        foreach (GameObject platform in platforms)
        {
            if (platform.activeInHierarchy)
            {
                if (colliderBox.IsTouching(platform.GetComponent<Collider2D>()))
                {
                    platform.SetActive(false);
                }
            }
        }
	}
}
