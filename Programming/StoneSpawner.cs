using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpawner : MonoBehaviour {

    public GameObject stone;

    GameObject stoneInstance;

    float timeCounter = 0;
	
	// Update is called once per frame
	void Update () {

        if (stoneInstance == null) {
            timeCounter += Time.deltaTime;

            if (timeCounter > 30) {
                stoneInstance = Instantiate(stone, transform.position, transform.rotation);
                WorldObject wi = stoneInstance.gameObject.GetComponent<WorldObject>();
                wi.type = "Tree";
            }
        }
		
	}
}
