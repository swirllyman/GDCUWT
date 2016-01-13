using UnityEngine;
using System.Collections;

public class FakeMover : MonoBehaviour {
    public float rotationOffset = 5;
    public float flyAmount = 25;
    Rigidbody body;
	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
	}
	
    void FixedUpdate()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotationOffset, 0);
        body.AddForce(transform.forward * Input.GetAxisRaw("Vertical") * 50);

        if (Input.GetButton("Jump"))
        {
            body.AddForce(0, flyAmount, 0);
        }
    }
	// Update is called once per frame
	void Update () {
	    
	}
    
}
