using UnityEngine;
using System.Collections;

public class Fog : MonoBehaviour {

	public GameObject player;
	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().material.renderQueue=1;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = player.transform.position;
	}
}
