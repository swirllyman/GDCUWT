using UnityEngine;
using System.Collections;

public class FloatingItem : MonoBehaviour {

    public float spinSpeed;
    public float bounceAmount;
    public float bounceSpeed;

    float startingYPos;


	// Use this for initialization
	void Start () {
        startingYPos = transform.position.y;
	}
    
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, spinSpeed * Time.deltaTime, 0);

        transform.position = new Vector3(transform.position.x, startingYPos + Mathf.PingPong(Time.time * bounceSpeed, bounceAmount), transform.position.z);
	}
}
