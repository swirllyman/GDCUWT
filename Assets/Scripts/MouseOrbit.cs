using UnityEngine;
using System.Collections;

public class MouseOrbit : MonoBehaviour {
	
	public Transform target;
	public float currentMaxDistance;
	public float currentDistance;
	public float xSpeed;
	public float ySpeed;
	
	public float yMinLimit = 10f;
	public float yMaxLimit = 80f;
	
	public float distanceMin;
	public float distanceMax;
	
	float x = 0.0f;
	float y = 0.0f;
	
	// Use this for initialization
	void Start () {
		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;
		
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
	}

	void FixedUpdate() {
		RaycastHit hit;
		if (Physics.Linecast (target.position, transform.position, out hit)) {
			currentDistance = hit.distance;
		} else if (!Physics.Raycast(transform.position, -transform.forward, out hit, 1)) {
			currentDistance = Mathf.Lerp(currentDistance, currentMaxDistance, .05f);
		}
	}

	void LateUpdate () {
		if (target != null && transform.root.GetComponent<WalkAround>().cursorHide) {
			if(currentDistance > 50){
				xSpeed = 2;
			}else if(currentDistance > 15){
				xSpeed = 3;
			}else{
				xSpeed = 4;
			}
			x += Input.GetAxis("Mouse X") * xSpeed * currentDistance * 0.02f;
			y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
			y = ClampAngle(y, yMinLimit, yMaxLimit);
			Quaternion rotation = Quaternion.Euler(y, x, 0);
			currentDistance = Mathf.Clamp(currentDistance - Input.GetAxis("Mouse ScrollWheel")*5, distanceMin, distanceMax);
			if(currentDistance > currentMaxDistance) {
				currentMaxDistance = currentDistance;
			}

			Vector3 negDistance = new Vector3(0.0f, 0.0f, -currentDistance);
			Vector3 position = rotation * negDistance + target.position;
			transform.rotation = rotation;
			//player.transform.rotation = transform.eulerAngles;
			transform.position = position;
		}
	}
	
	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}
	
	
}