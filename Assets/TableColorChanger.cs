using UnityEngine;
using System.Collections;

public class TableColorChanger : MonoBehaviour {

	public GameObject blueprint;
	private Material blueprintColor;

	// Use this for initialization
	void Start () {
		blueprintColor = blueprint.GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
		if(blueprint == null) {
			gameObject.GetComponent<Renderer>().material = blueprintColor;
		}
	}
}
