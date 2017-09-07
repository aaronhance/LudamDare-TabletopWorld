using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sapling : MonoBehaviour {

    float timeCounter = 0;
    float timeToGrow = 0;

    public GameObject tree;

    private void Start() {
        timeToGrow = Random.Range(10, 60);
    }

    // Update is called once per frame
    void FixedUpdate () {
        timeCounter += Time.fixedDeltaTime;

        if (timeCounter > timeToGrow) {
            Instantiate(tree, transform.position, Quaternion.Euler(0, Random.Range(0, 360), 0));
            Destroy(gameObject);
        }
	}
}
