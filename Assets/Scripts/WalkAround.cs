using UnityEngine;
using System.Collections;

public class WalkAround : MonoBehaviour {
	bool forward;
	bool backward;
	bool left;
	bool right;
	bool diagonal;
    bool highSlope;
    public bool cursorHide = true;
    public bool crafting;
    public GameObject mesh;
	public GameObject myCamera;

	public float baseGravityAmount;
	public float jumpAmount;
	public float gravityAmount;

	private float inverse_speed;

	public bool grounded;
	public bool swimming;

	private float swimSpeed = 0.0f;
	private float swimCD = .5f;

	bool sprinting;
	public bool falling;

    Animator anim;

	//Quaternion startingLoc;

	void Awake () {
        anim = mesh.GetComponent<Animator>();
        crafting = false;
		gravityAmount = baseGravityAmount;
		swimming = false;
		inverse_speed = 1.5f;
		sprinting = false;
		falling = false;
		forward = false;
		backward = false;
		left = false;
		right = false;
        grounded = true;
        HideCursor(true);
		//startingLoc = mesh.transform.rotation;
	}

	void Start () {
		anim.SetBool ("Moving", false);
	}

	void FixedUpdate() {
		if(swimSpeed >= 0.0f) {
			swimSpeed -= Time.deltaTime;
		}
	}


    public void HideCursor(bool b)
    {
        if (b)
        {
            cursorHide = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            cursorHide = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown (KeyCode.Escape))
	  	{
            if (cursorHide)
                HideCursor(false);
			else
                HideCursor(true);
        }

		RaycastHit hit;

        ///     Checking area in front to make sure the slope isn't too high    ///
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z), mesh.transform.forward, out hit, 2.5f))
        {
            if (hit.collider.tag == "Ground")
            {
                if (hit.normal.y < .8f)
                    highSlope = true;
            }
        }
        else
            highSlope = false;


        //  Swimming    //
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
                grounded = false;
				GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpAmount, 0));
			}
		} else {
			GetComponent<Rigidbody>().AddForce(new Vector3(0, gravityAmount, 0));
		}

		if(sprinting) {
			inverse_speed = 2;
            anim.SetBool("Sprint", true);
		} else {
			inverse_speed = 3;
            anim.SetBool("Sprint", false);
		}



        /// Movement and rotations ///
        if (!crafting)
        {
            if (Input.GetButton("Sprint"))
                sprinting = true;
            else
                sprinting = false;

            if (Time.timeScale != 0.0 && cursorHide)
                transform.rotation = Quaternion.Euler(0, myCamera.transform.eulerAngles.y, 0);

            if (Input.GetButton("Forward"))
                forward = true;
            else
                forward = false;

            if (Input.GetButton("Backward"))
                backward = true;
            else 
                backward = false;

            if (Input.GetButton("Left"))
                left = true;
            else 
                left = false;

            if (Input.GetButton("Right"))
                right = true;
            else
                right = false;

            //Diagonal Check
            if ((left && forward) || (right && forward) || (left && backward) || (right && backward))
                diagonal = true;
            else
                diagonal = false;


            if (!diagonal)
            {
                if (forward)
                {
                    if(!highSlope) gameObject.transform.Translate(new Vector3(0, 0, 1) / inverse_speed);
                    rotater(0);
                }
                if (backward)
                {
                    if (!highSlope) gameObject.transform.Translate(new Vector3(0, 0, -1) / inverse_speed);
                    rotater(180);
                }
                if (left)
                {
                    if (!highSlope) gameObject.transform.Translate(new Vector3(-1, 0, 0) / inverse_speed);
                    rotater(-90);
                }
                if (right)
                {
                    if (!highSlope) gameObject.transform.Translate(Vector3.right / inverse_speed);
                    rotater(90);
                }
            }
            else
            {
                if (left && forward)
                {
                    if (!highSlope)
                    {
                        gameObject.transform.Translate(new Vector3(-.75f, 0, 0) / inverse_speed);
                        gameObject.transform.Translate(new Vector3(0, 0, .75f) / inverse_speed);
                    }
                    rotater(315);
                }
                if (right && forward)
                {
                    if (!highSlope)
                    {
                        gameObject.transform.Translate(new Vector3(.75f, 0, 0) / inverse_speed);
                        gameObject.transform.Translate(new Vector3(0, 0, .75f) / inverse_speed);
                    }
                    rotater(45);
                }
                if (left && backward)
                {
                    if (!highSlope)
                    {
                        gameObject.transform.Translate(new Vector3(-.75f, 0, 0) / inverse_speed);
                        gameObject.transform.Translate(new Vector3(0, 0, -.75f) / inverse_speed);
                    }
                    rotater(205);
                }
                if (right && backward)
                {
                    if (!highSlope)
                    {
                        gameObject.transform.Translate(new Vector3(.75f, 0, 0) / inverse_speed);
                        gameObject.transform.Translate(new Vector3(0, 0, -.75f) / inverse_speed);
                    }
                    rotater(135);
                }
            }



            //Animations
            if (left || right || forward || backward)
            {
               anim.SetBool("Moving", true);
            }
            else
            {
                anim.SetBool("Moving", false);
            }
        }
	}



	void setSwimming (bool b) {
		if (b) {
			gravityAmount = baseGravityAmount/2;
			swimming = true;
			falling = false;
			grounded = false;
		} else {
            gravityAmount = baseGravityAmount;
			swimming = false;
			falling = true;
		}
	}


	void OnCollisionExit(Collision c) {
		if(!swimming){
			if(c.collider.tag == "Ground") {
				grounded = false;
                falling = true;
			}
		}
	}

	void OnCollisionEnter (Collision c) {
		if(!swimming){
			if(c.collider.tag == "Ground") {
                grounded = true;
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
