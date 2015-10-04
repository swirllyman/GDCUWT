using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

	bool inventoryOpen;
	bool showInventory;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!inventoryOpen) {
			if(Input.GetButtonDown("Inventory")) {
				inventoryOpen = true;
			}
		} else {
			if(Input.GetButtonDown("Inventory")) {
				inventoryOpen = false;
			}
		}
	}

	void OnGUI () {
		if(inventoryOpen) {
			GUI.BeginGroup(new Rect(100, 100, Screen.width - 200, Screen.height - 200));
			GUI.Box (new Rect(0, 0, Screen.width - 200, Screen.height - 200), "Inventory Backpanel");
			GUI.Box (new Rect(Screen.width - 725, 25, 500, 675), "Inventory");
			GUI.EndGroup();
		}
	}

}
