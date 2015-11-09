using UnityEngine;
using System.Collections;

public class CameraInWater : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider c) {
		if(c.gameObject.tag == "Water") {
			GetComponent<Renderer>().enabled = true;
		}
	}

	void OnTriggerExit(Collider c) {
		if(c.gameObject.tag == "Water") {
			GetComponent<Renderer>().enabled = false;
		}
	}
}
