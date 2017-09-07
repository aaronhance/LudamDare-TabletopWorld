using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCycle : MonoBehaviour {

    public float timeCounter;

    public int day;
    public bool dayTime = true;

    public bool spawnZ;

    public new float light = 0;

    public Light lightObject;
    public GameObject zombie;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (dayTime) {
            //light incease
            if (timeCounter > 100) {
                timeCounter = 0;
                dayTime = false;
            }
            light += 1.07f * Time.deltaTime;
        }
        else {
            //light decrease
            if (timeCounter > 100) {
                spawnZ = true;
                timeCounter = 0;
                dayTime = true;
                day++;
            }
            light -= 1.07f * Time.deltaTime;

            //zombie spawning
        }
        timeCounter += Time.deltaTime;

        lightObject.intensity = light / 50;

        if (light < 50 && spawnZ) {
            for (int i = 0; i <= (day / 2); i++) {
                Instantiate(zombie, new Vector3(Random.Range(-30, 30), 38, Random.Range(-30, 30)), Quaternion.identity);
            }
            spawnZ = false;
        }
		
	}
}
