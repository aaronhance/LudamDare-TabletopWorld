using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour {

    public float currentHealth;
    public float maxHealth;

    public GameObject green;
    float size = 1.31f;

    public bool dead = false;

    public void damage(float amount) {
        currentHealth -= amount;
        green.transform.localScale = new Vector3((currentHealth * (size / maxHealth)), green.transform.localScale.y, green.transform.localScale.z);
        float pos = (green.transform.localScale.x - size) / 2;
        Vector3 tmp = green.transform.localPosition;
        green.transform.localPosition = new Vector3(0 - pos, tmp.y, tmp.z);
    }

    public void Update() {
        if (currentHealth < 1) {
            dead = true;
        }
    }

}
