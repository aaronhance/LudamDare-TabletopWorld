using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour {

    public GameObject man;
    GameObject maanInst;
    bool init = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (!init) {
            maanInst = Instantiate(man, new Vector3(Random.Range(-30, 30), 38, Random.Range(-30, 30)), Quaternion.identity);
            PlayerManager.singleton.men.Add(maanInst);
            maanInst.GetComponent<Man>().myHouse = this;
            init = true;
            PlayerManager.singleton.people += 1;
        }

        if (maanInst == null) {
            Destroy(gameObject);
            PlayerManager.singleton.people -= 1;
        }
    }
}
