using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider c) {
		if(c.gameObject.tag == "Player") {
			c.gameObject.GetComponent("WalkAround").SendMessage("setSwimming", true);
		}
	}

	void OnTriggerExit(Collider c) {
		if(c.gameObject.tag == "Player") {
			c.gameObject.GetComponent("WalkAround").SendMessage("setSwimming", false);
		}
	}
}
