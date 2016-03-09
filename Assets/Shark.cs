using UnityEngine;
using System.Collections;

public class Shark : MonoBehaviour {

    Vector3 startPos;
    public float speed;

    public float randomTimer;
    public bool turning;
    public Quaternion destRot;


    public float distance;
	// Use this for initialization
	void Start () {
        startPos = transform.position;
        randomTimer = Random.Range(2, 10);
	}

    // Update is called once per frame
    void Update()
    {

        distance = Vector3.Distance(transform.position, startPos);

        if (turning)
        {
            float angle = Quaternion.Angle(transform.rotation, destRot);
            if (angle < 15) {
                turning = false;
            }
        }
            

        if (distance > 20)
        {
            turning = true;
            transform.Rotate(0, 180, 0);
           // destRot = Quaternion.Euler(0, 180, 0);
        }
        else {
            randomTimer -= Time.deltaTime;

            if (randomTimer <= 0.0f)
            {
                turning = true;
                //destRot = Quaternion.Euler(0, Random.Range(0, 360), 0);
                transform.Rotate(0, Random.Range(0, 360), 0);
                randomTimer = Random.Range(2, 10);
            }
        }

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 5))
        {
            if (hit.collider.tag == "Ground")
            {
                turning = true;
               // destRot = Quaternion.Euler(0, 180, 0);
                transform.Rotate(0, 180, 0);
            }
        }

       // transform.rotation = Quaternion.Slerp(transform.rotation, destRot, Time.deltaTime * 2);
        transform.position += transform.forward * speed;

        if (!turning)
        {
           // transform.position += transform.forward * speed;
        }


    }
}
