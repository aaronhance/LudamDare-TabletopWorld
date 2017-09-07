using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour {

    public float population = 10;
    public float maxPopulation = 40;

    public float timeCounter = 0;
    public float fishSeks = 0.1f;

    public List<Transform> fishingSpots = new List<Transform>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (population > 2 && population < maxPopulation) {
            population += fishSeks / population * Time.deltaTime;
        }

        timeCounter += Time.deltaTime;
		
	}

    public Transform getSpot() {
        return fishingSpots[UnityEngine.Random.Range(0, fishingSpots.Count - 1)];
    }
}
