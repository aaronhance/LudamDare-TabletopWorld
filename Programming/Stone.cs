using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : tree {

	// Update is called once per frame
	void Update () {
        if (workRequired <= 0) {
            PlayerManager.singleton.stone += Random.Range(0, 5);
            Destroy(this.gameObject);
        }
    }
}
