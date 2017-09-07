using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tree : MonoBehaviour {

    public float workRequired = 10;

    private void Update() {
        if (workRequired <= 0) {
            PlayerManager.singleton.wood += Random.Range(1, 4);
            PlayerManager.singleton.saplings += Random.Range(0, 3);
            Destroy(this.gameObject);
        }
    }

}
