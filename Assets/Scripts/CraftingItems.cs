using UnityEngine;
using System.Collections;

public class CraftingItems : MonoBehaviour {
	public float speed = 1;
	public Camera mainCamera;

	public Camera craftCamera;
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
        craftCamera = GameObject.FindGameObjectWithTag("Craft Cam").GetComponent<Camera>();
        mainCamera.enabled = true;
        craftCamera.enabled = false;
        crafting = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(currentObject != null && interacting){
			if(Input.GetMouseButtonDown(0))
			{
				switch(currentObject.tag){
				case "Interact" :
					Destroy (currentObject);
					break;
				case "CraftingTable":
					crafting = true;
                    playerScript.crafting = true;
                    playerScript.HideCursor(false);
					break;
				}
			}
		}
		if(crafting) {
			currentObject.GetComponent<ParticleSystem>().enableEmission = false;
			mainCamera.enabled = false;
			craftCamera.enabled = true;
			Ray ray = craftCamera.ScreenPointToRay(Input.mousePosition);
			currentPoint = Input.mousePosition;
			currentPoint = craftCamera.ScreenToWorldPoint(currentPoint);
			currentPoint.y = 458f;
			//if(Physics.Raycast(transform.position, out hit, 10000.0f)) {
			if(Physics.Raycast (ray, out hit, 10000.0f)) {
				if(hit.collider.tag == "CraftInteract") {
					lightUpObject = hit.collider.gameObject;
					if(lightUpObject.GetComponent <Renderer>().enabled) {
						lightUpObject.GetComponent<ParticleSystem>().enableEmission = true;
						if(Input.GetMouseButtonDown(0)) {
							currentCraftObject = hit.collider.gameObject;
							//Debug.Log(Input.mousePosition);
						}
					}
				} else if(hit.collider.tag == "Border") {
					if(lightUpObject != null){
						lightUpObject.GetComponent<ParticleSystem>().enableEmission = false;
						lightUpObject = null;
					}
					if(currentCraftObject != null){
						currentCraftObject = null;
					}
				}else if(lightUpObject != null) {
					lightUpObject.GetComponent<ParticleSystem>().enableEmission = false;
					lightUpObject = null;
				}
			} else if(lightUpObject != null) {
				lightUpObject.GetComponent<ParticleSystem>().enableEmission = false;
				lightUpObject = null;
			}

			if(currentCraftObject != null) {
				Vector3.Lerp(transform.position, currentPoint, speed * Time.deltaTime);
				Debug.Log (hit.transform.position);
				currentCraftObject.transform.position = currentPoint;
				if(Physics.Raycast (ray, out hit, 10000.0f)) {
					if(hit.collider.tag == "Snap") {
						Debug.Log ("Snap");
						currentCraftObject.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y+25.0f, hit.transform.position.z);
						currentCraftObject = null;
					}
				}
				if(Input.GetMouseButtonUp (0)) {
					currentCraftObject = null;
				}
			}

		} else {
			mainCamera.enabled = true;
			craftCamera.enabled = false;
		}
	}

	void OnTriggerEnter(Collider c){
		if(c.gameObject.tag == "Interact" || c.gameObject.tag == "CraftingTable") {
			c.gameObject.GetComponent<ParticleSystem>().enableEmission = true;
			currentObject = c.gameObject;
			interacting = true;
		}
	}

	void OnTriggerExit (Collider c) {
		if(c.gameObject.tag == "Interact" || c.gameObject.tag == "CraftingTable") {
			c.gameObject.GetComponent<ParticleSystem>().enableEmission = false;
			currentObject = null;
			interacting = false;
		}
	}


	void OnGUI() {
		if(crafting){
			if(GUI.Button (new Rect(Screen.width - 100, 25, 75, 50), "Exit")) {
				crafting = false;
                playerScript.crafting = false;
                playerScript.HideCursor(true);
            }
		}
	}
}
