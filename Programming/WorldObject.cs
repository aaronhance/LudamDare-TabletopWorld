using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour {

    public int collisions;
    public bool isColliding = false;

    public string name;
    public string type;

    public int woodCost;
    public int stoneCost;
    public int saplingCost;

    // Use this for initialization
    void Start () {
		
	}

    private void FixedUpdate() {
        collisions = 0;
    }

    // Update is called once per frame
    void Update () {
        if (collisions > 0) {
            isColliding = true;
        }
        else {
            isColliding = false;
        }
	}

    void OnCollisionEnter(Collision collision) {
        collisions += 1;
    }

    void OnCollisionExit(Collision collision) {
        collisions -= 1;
    }
}
