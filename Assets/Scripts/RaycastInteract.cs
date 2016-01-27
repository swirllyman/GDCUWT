using UnityEngine;
using System.Collections;

public class RaycastInteract : MonoBehaviour {
	private Ray ray;
	public GameObject currentObject;
	public float height;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Debug.Log("I am attached to : " + transform.root.name);

        if(currentObject != null)
        {
            currentObject.layer = 2;
        }


		RaycastHit hit;
		if(Physics.Raycast(new Vector3(transform.position.x, transform.position.y + height, transform.position.z), transform.TransformDirection(Vector3.forward), out hit, 5)){
			print("Hit: " +hit.collider.name);
			if(hit.collider.tag == "CraftInteract") {
				currentObject = hit.collider.gameObject;
				currentObject.GetComponent<ParticleSystem>().enableEmission = true;

			}
		} else if(Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.forward), out hit, 5)){
			print("Hit: " +hit.collider.name);
			if(hit.collider.tag == "CraftInteract") {
				currentObject = hit.collider.gameObject;
				currentObject.GetComponent<ParticleSystem>().enableEmission = true;
			}
		} else {
			if(currentObject != null){
				currentObject.GetComponent<ParticleSystem>().enableEmission = false;
			}
			currentObject = null;
		}
	}
}
