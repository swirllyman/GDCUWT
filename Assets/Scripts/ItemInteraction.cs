using UnityEngine;
using System.Collections;

public class ItemInteraction : MonoBehaviour {
	public float speed = 1;
	public Camera mainCamera;

	public Camera craftCamera;
	private bool crafting;
	private GameObject currentObject;
	private RaycastHit hit;

	private Vector3 currentPoint;

	private GameObject lightUpObject;

	// Use this for initialization
	void Start () {
		crafting = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(crafting) {
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
							currentObject = hit.collider.gameObject;
							//Debug.Log(Input.mousePosition);
						}
					}
				} else if(hit.collider.tag == "Border") {
					if(lightUpObject != null){
						lightUpObject.GetComponent<ParticleSystem>().enableEmission = false;
						lightUpObject = null;
					}
					if(currentObject != null){
						currentObject = null;
					}
				}else if(lightUpObject != null) {
					lightUpObject.GetComponent<ParticleSystem>().enableEmission = false;
					lightUpObject = null;
				}
			} else if(lightUpObject != null) {
				lightUpObject.GetComponent<ParticleSystem>().enableEmission = false;
				lightUpObject = null;
			}

			if(currentObject != null) {
				Vector3.Lerp(transform.position, currentPoint, speed * Time.deltaTime);
				Debug.Log (hit.transform.position);
				currentObject.transform.position = currentPoint;
				if(Physics.Raycast (ray, out hit, 10000.0f)) {
					if(hit.collider.tag == "Snap") {
						Debug.Log ("Snap");
						currentObject.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y+25.0f, hit.transform.position.z);
						currentObject = null;
					}
				}
				if(Input.GetMouseButtonUp (0)) {
					currentObject = null;
				}
			}

		} else {
			mainCamera.enabled = true;
			craftCamera.enabled = false;
		}
	}

	void OnTriggerStay(Collider c){
		if(c.gameObject.tag == "Interact") {
			c.gameObject.GetComponent<ParticleSystem>().enableEmission = true;
			if(Input.GetMouseButton (0)) {
				Destroy (c.gameObject);
			}
		}
		if(c.gameObject.tag == "CraftingTable") {
			c.gameObject.GetComponent<ParticleSystem>().enableEmission = true;
			if(Input.GetMouseButton (0)) {
				crafting = true;
			}
		}

		if(c.gameObject.tag == "Blueprint") {
			c.gameObject.GetComponent<ParticleSystem>().enableEmission = true;
			if(Input.GetMouseButton (0)) {
				Destroy (c.gameObject);
			}
		}
	}

	void OnTriggerExit (Collider c) {
		if(c.gameObject.tag == "Interact") {
			c.gameObject.GetComponent<ParticleSystem>().enableEmission = false;
		}

		if(c.gameObject.tag == "CraftingTable") {
			c.gameObject.GetComponent<ParticleSystem>().enableEmission = false;
		}

		if(c.gameObject.tag == "Blueprint") {
			c.gameObject.GetComponent<ParticleSystem>().enableEmission = false;
		}
	}


	void OnGUI() {
		if(crafting){
			if(GUI.Button (new Rect(Screen.width - 100, 25, 75, 50), "Exit")) {
				crafting = false;
			}
		}
	}
}
