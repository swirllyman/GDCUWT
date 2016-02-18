using UnityEngine;
using System.Collections;

public class CraftingItems : MonoBehaviour {
	public float speed = 1;
    public Camera[] cams;
	private bool crafting;
	private GameObject currentCraftObject;
	private GameObject currentObject;
	private bool interacting;
	private RaycastHit hit;

	private Vector3 currentPoint;

	private GameObject lightUpObject;

    private WalkAround playerScript;

	// Use this for initialization
	void Start () {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<WalkAround>();
        cams[1] = GameObject.FindGameObjectWithTag("Craft Cam").GetComponent<Camera>();
        cams[1].tag = "MainCamera";
	}
	
	// Update is called once per frame
	void Update () {
		if(currentObject != null && interacting){
			if(Input.GetMouseButtonDown(0))
			{
				switch(currentObject.tag){
				case "Interact" :
                    transform.root.GetComponent<ObjectiveManager>().recheckArea(currentObject);
					Destroy (currentObject);
					break;
				case "CraftingTable":
                    SwitchCams(cams[0], cams[1]);   
                    playerScript.crafting = true;
                    playerScript.HideCursor(false);
					break;
				}
			}
		}
    }

    float currentObjectEmissionRate;

    void SwitchCams(Camera a, Camera b) {
        b.enabled = true;
        a.enabled = false;
    }
	void OnTriggerEnter(Collider c){
		if(c.gameObject.tag == "Interact" || c.gameObject.tag == "CraftingTable") {
            currentObjectEmissionRate = c.gameObject.GetComponent<ParticleSystem>().emissionRate;
            c.gameObject.GetComponent<ParticleSystem>().emissionRate = 100;

            currentObject = c.gameObject;
			interacting = true;
		}
	}

	void OnTriggerExit (Collider c) {
		if(c.gameObject.tag == "Interact" || c.gameObject.tag == "CraftingTable") {
            c.gameObject.GetComponent<ParticleSystem>().emissionRate = currentObjectEmissionRate;
            currentObject = null;
			interacting = false;
		}
	}


	void OnGUI() {
		if(crafting){
			if(GUI.Button (new Rect(Screen.width - 100, 25, 75, 50), "Exit")) {
                SwitchCams(cams[1], cams[0]);
                playerScript.crafting = false;
                playerScript.HideCursor(true);
            }
		}
	}
}
