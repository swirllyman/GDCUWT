using UnityEngine;
using System.Collections;

public class WalkAround : MonoBehaviour {
	bool forward;
	bool backward;
	bool left;
	bool right;
	bool diagonal;
	public GameObject mesh;
	public GameObject camera;

	private float baseGravityAmount;
	public float jumpAmount;
	public float gravityAmount;

	private float inverse_speed;

	public bool grounded;
	public bool swimming;

	private float swimSpeed = 0.0f;
	private float swimCD = .5f;

	bool sprinting;
	public bool falling;


	public Camera mainCamera;
	public Camera craftCamera;

	//Quaternion startingLoc;

	void Awake () {
		mainCamera.enabled = true;
		craftCamera.enabled = false;
		baseGravityAmount = gravityAmount;
		swimming = false;
		inverse_speed = 1.5f;
		sprinting = false;
		falling = false;
		forward = false;
		backward = false;
		left = false;
		right = false;
		//startingLoc = mesh.transform.rotation;
	}

	void Start () {
	}

	void FixedUpdate() {
		if(swimSpeed >= 0.0f) {
			swimSpeed -= Time.deltaTime;
		}
	}
	// Update is called once per frame
	void Update () {
		RaycastHit ray;

		if(Physics.Raycast(new Vector3(transform.position.x, transform.position.y+1.5f, transform.position.z), mesh.transform.forward, out ray, 2.5f)) {
			Debug.DrawLine (new Vector3(transform.position.x, transform.position.y+1.5f, transform.position.z), ray.point, Color.green);
			if(ray.collider.name == "ground") {
				Debug.Log ("Hit Ground");
			}
		}

		if(swimming) {
			grounded = false;
			falling = false;
			GetComponent<Rigidbody>().useGravity = false;
			if(swimSpeed <= 0.0f){
				if(Input.GetKeyDown(KeyCode.Space)){
					GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpAmount, 0));
					swimSpeed = swimCD;
				}
			}
		} else {
			gravityAmount = baseGravityAmount;
		}

		if(grounded) {
			falling = false;
			if(Input.GetKeyDown(KeyCode.Space)){
				falling = true;
				GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpAmount, 0));
			}
		} else {
			GetComponent<Rigidbody>().AddForce(new Vector3(0, gravityAmount, 0));
		}

		if(sprinting) {
			inverse_speed = 2;
			mesh.GetComponent<Animation>()["Walk"].speed = 2;
		} else {
			inverse_speed = 3;
			mesh.GetComponent<Animation>()["Walk"].speed = 1;
		}



				/// Movement and rotations ///
		 
		if(Input.GetButtonDown ("Sprint")) {
			sprinting = true;
		}
		if(Input.GetButtonUp ("Sprint")) {
			sprinting = false;
		}

		if(Time.timeScale != 0.0) {
			transform.rotation = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0);
		}
		if((left && forward) || (right && forward) || (left && backward) || (right && backward)){
			diagonal = true;
		}
		else {
			diagonal = false;
		}

		if(Input.GetButtonDown ("Forward")) {
			forward = true;
		} else if( Input.GetButtonUp ("Forward")){
			forward = false;
		}

		if(Input.GetButtonDown ("Backward")) {
			backward = true;
		} else if( Input.GetButtonUp ("Backward")){
			backward = false;
		}

		if(Input.GetButtonDown ("Left")) {
			left = true;
		} else if( Input.GetButtonUp ("Left")){
			left = false;
		}

		if(Input.GetButtonDown ("Right")) {
			right = true;
		} else if( Input.GetButtonUp ("Right")){
			right = false;
		}
		if(!diagonal) {
			if(forward){
				gameObject.transform.Translate(new Vector3(0, 0, 1) / inverse_speed);         //Vector3.forward;
				//GetComponent<Rigidbody>().AddForce(mesh.transform.forward * 20);
				rotater (0);
			}
			if(backward){
				gameObject.transform.Translate(new Vector3(0, 0, -1) / inverse_speed);
				//GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -20));//new Vector 3 (0, 0, -1);
				rotater (180);
			}
			if(left){
				gameObject.transform.Translate(new Vector3(-1, 0, 0) / inverse_speed);         //Vector3.forward;
				//GetComponent<Rigidbody>().AddForce(new Vector3(-20, 0, 0));
				rotater (-90);
			}
			if(right){
				gameObject.transform.Translate(Vector3.right / inverse_speed);         //Vector3.forward;
				//GetComponent<Rigidbody>().AddForce(new Vector3(20, 0, 0));
				rotater (90);
			}
		} else {
			if(left && forward) {
				gameObject.transform.Translate(new Vector3(-.75f, 0, 0) / inverse_speed);
				gameObject.transform.Translate(new Vector3(0, 0, .75f) / inverse_speed);
				rotater (315);
			}
			if(right && forward) {
				gameObject.transform.Translate(new Vector3(.75f, 0, 0) / inverse_speed);
				gameObject.transform.Translate(new Vector3(0, 0, .75f) / inverse_speed);
				rotater (45);
			}
			if(left && backward) {
				gameObject.transform.Translate(new Vector3(-.75f, 0, 0) / inverse_speed);
				gameObject.transform.Translate(new Vector3(0, 0, -.75f) / inverse_speed);
				rotater (205);
			}
			if(right && backward) {
				gameObject.transform.Translate(new Vector3(.75f, 0, 0) / inverse_speed);
				gameObject.transform.Translate(new Vector3(0, 0, -.75f) / inverse_speed);
				rotater (135);
			}
		}

		if (left || right || forward || backward) {
			mesh.GetComponent<Animation>().CrossFade("Walk");
		}
		else {
			mesh.GetComponent<Animation>().Stop ("Walk");
			mesh.GetComponent<Animation>().Play ("Idle");
		}
	}

	void setSwimming (bool b) {
		if (b) {
			gravityAmount = gravityAmount/2;
			swimming = true;
			falling = false;
			grounded = false;
		} else {
			swimming = false;
			falling = true;
		}
	}


	void OnCollisionExit(Collision c) {
		if(!swimming){
			if(c.gameObject.name == "ground") {
				grounded = false;
			}
		}
	}

	void OnCollisionStay (Collision c) {
		if(!swimming){
			if(c.gameObject.name == "ground") {
				grounded = true;
			} else {
				grounded = false;
			}
		}
	}

	void OnCollisionEnter (Collision c) {
		if(!swimming){
			if(c.gameObject.name == "ground") {
				falling = false;
			}
		}
	}

	void rotater(int num){
		if(mesh.transform.rotation.y != num){
			mesh.transform.rotation = transform.rotation;
			mesh.transform.Rotate(new Vector3(0, num, 0));
		}
	}
}
