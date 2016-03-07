using UnityEngine;
using System.Collections;

public class CraftableItem : MonoBehaviour {

    bool hovering = false;
    bool selected = false;


    Vector3 currentPoint;
    Vector3 startingPos;

    public GameObject realObject;

    bool created = false;

    public float outlineValue = .0015f;
    // Use this for initialization
    void Start () {
        gameObject.layer = 2;
        startingPos = transform.position;
	}

    void Select(bool select)
    {
        if (select)
        {
            selected = true;
            gameObject.layer = 2;
        }
        else
        {
            selected = false;
            gameObject.layer = 0;
        }
    }

	// Update is called once per frame
	void Update () {
        if (realObject == null &! created) {
            gameObject.layer = 0;
            GetComponent<Renderer>().enabled = true;
            created = true;
        }

        if (hovering)
        {
            float currentOutlineAmount = GetComponent<Renderer>().material.GetFloat("_Outline");
            float lerpValue = Mathf.Lerp(currentOutlineAmount, outlineValue, Time.deltaTime * 20);
            GetComponent<Renderer>().material.SetFloat("_Outline", lerpValue);

            if (Input.GetMouseButtonDown(0))
            {
                Select(true);
            }
        }
        else
        {
            float currentOutlineAmount = GetComponent<Renderer>().material.GetFloat("_Outline");
            float lerpValue = Mathf.Lerp(currentOutlineAmount, 0, Time.deltaTime * 20);
            GetComponent<Renderer>().material.SetFloat("_Outline", lerpValue);
        }


        if (selected)
        {
            currentPoint = Input.mousePosition;
            currentPoint = Camera.main.ScreenToWorldPoint(currentPoint);
            currentPoint.y = 458f;
            transform.position = currentPoint;
            transform.localPosition = new Vector3(transform.localPosition.x - .005f, transform.localPosition.y, transform.localPosition.z);
            if (Input.GetMouseButtonUp(0))
            {
                Select(false);
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.tag != "CraftTable2")
                {
                    Debug.Log("Out of range, You hit: " + hit.collider.tag);
                    Select(false);
                    transform.position = startingPos;
                }
            }

        }


	}

    void OnMouseEnter()
    {
        hovering = true;
    }

    void OnMouseExit()
    {
        hovering = false;
    }
}
