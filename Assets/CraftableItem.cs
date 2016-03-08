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

    Collider myCollider;

    // Use this for initialization
    void Start () {
        myCollider = GetComponent<Collider>();
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
            myCollider.enabled = true;
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag != "CraftTable2")
                {
                    Debug.Log("Out of range, You hit: " + hit.collider.name);
                    Select(false);
                    transform.position = startingPos;
                }
                else
                {
                    currentPoint = new Vector3(hit.point.x, 458f, hit.point.z);
                    transform.position = currentPoint;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                Select(false);
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
