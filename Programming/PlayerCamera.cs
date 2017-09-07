using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    Vector3 target;
    Vector3 velocity = Vector3.zero;

    public float smoothTime;

	public float speedH = 2.0f;
	public float speedV = 2.0f;

	private float yaw = 0.0f;
	private float pitch = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		var newTransformForward = transform.forward;

        if (Input.GetKey(KeyCode.W)) {
			transform.position = transform.position + Camera.main.transform.forward * 10 * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S)) {
			transform.position = transform.position - Camera.main.transform.forward * 10 * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A)) {
			transform.position = transform.position - Camera.main.transform.right * 10 * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D)) {
			transform.position = transform.position + Camera.main.transform.right * 10 * Time.deltaTime;
        }
			
		transform.forward = newTransformForward;

        //Vector3 newTransform = Vector3.zero;
        //newTransform.y = 45; //TODO

        //newTransform.z = Mathf.SmoothDamp(transform.position.z, transform.position.z + target.z, ref velocity.z, smoothTime);
        //newTransform.x = Mathf.SmoothDamp(transform.position.x, transform.position.x + target.x, ref velocity.x, smoothTime);

        //transform.position = newTransform;

		if (Input.GetKey(KeyCode.Mouse1)) {
			yaw += speedH * Input.GetAxis("Mouse X");
			pitch -= speedV * Input.GetAxis("Mouse Y");
			transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
		}
    }
}
