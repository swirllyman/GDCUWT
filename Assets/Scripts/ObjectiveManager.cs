using UnityEngine;
using System.Collections;

public class ObjectiveManager : MonoBehaviour {

    public GameObject[] objectives;
    public GameObject currentClosestObject;
    public GameObject targetItem;
    // Use this for initialization
    void Start () {
        objectives = GameObject.FindGameObjectsWithTag("Interact");
	}

    void Update()
    {
        if(currentClosestObject != null)
            targetItem.transform.LookAt(currentClosestObject.transform.position);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
	    foreach(GameObject go in objectives)
        {
            if(currentClosestObject == null)
            {
                currentClosestObject = go;
            }
            if(Vector3.Distance(transform.position, go.transform.position) < Vector3.Distance(transform.position, currentClosestObject.transform.position))
            {
                currentClosestObject = go;
            }
        }
	}
}
