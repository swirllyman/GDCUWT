using UnityEngine;
using System.Collections;

public class TableColorChanger : MonoBehaviour {

	public GameObject blueprint;
	private Material blueprintColor;
	bool switched = false;

	// Use this for initialization
	void Start () {
		blueprintColor = blueprint.GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
		if(!switched && blueprint == null) {
			gameObject.GetComponent<Renderer>().material = blueprintColor;
			switched = true;
		}
	}
}
