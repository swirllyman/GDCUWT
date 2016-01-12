using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ObjectiveManager : MonoBehaviour {

    //public GameObject[] objectives;
    public List<GameObject> objectiveList;
    public GameObject currentClosestObject;
    public GameObject targetItem;
    public Text itemName;

    // Use this for initialization
    void Start () {
        //objectives = GameObject.FindGameObjectsWithTag("Interact");
        objectiveList.AddRange(GameObject.FindGameObjectsWithTag("Interact"));
	}

    void Update()
    {
        if(currentClosestObject != null)
            targetItem.transform.LookAt(currentClosestObject.transform.position);
    }

    public void recheckArea(GameObject itemToBeRemoved)
    {
        //objectives = GameObject.FindGameObjectsWithTag("Interact");
        objectiveList.Remove(itemToBeRemoved);
    }
	
	void FixedUpdate () {
        if(objectiveList.Count <= 0)
        {
            targetItem.SetActive(false);
        }
        else
        {
            targetItem.SetActive(true);
        }
	    foreach(GameObject go in objectiveList)
        {
            if(currentClosestObject == null)
            {
                currentClosestObject = go;
            }
            if(Vector3.Distance(transform.position, go.transform.position) < Vector3.Distance(transform.position, currentClosestObject.transform.position))
            {
                currentClosestObject = go;
                itemName.text = currentClosestObject.name;
            }
        }
	}
}
