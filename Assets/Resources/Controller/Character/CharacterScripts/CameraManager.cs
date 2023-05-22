using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	public float followSpeed = 3;
	public float mouseSpeed = 2; 
	
	public float cameraDist = 3; 

	public Transform target;

	[HideInInspector]
	public Transform pivot;
	[HideInInspector]
	public Transform camTrans; 
	float turnSmoothing = .1f;
	public float minAngle = -35; 
	public float maxAngle = 35; 

	float smoothX;
	float smoothY;
	float smoothXvelocity;
	float smoothYvelocity;
	public float lookAngle; 
	public float tiltAngle; 

	public void Init()
	{
		camTrans = Camera.main.transform;
		pivot = camTrans.parent;
	}

	void FollowTarget(float d)
	{
		float speed = d * followSpeed; 
		Vector3 targetPosition = Vector3.Lerp(transform.position, target.position, speed);
		transform.position = targetPosition; 
	}

	void HandleRotations(float d, float v, float h, float targetSpeed)
	{ 
		if (turnSmoothing > 0)
		{
			smoothX = Mathf.SmoothDamp(smoothX, h, ref smoothXvelocity, turnSmoothing); 
			smoothY = Mathf.SmoothDamp(smoothY, v, ref smoothYvelocity, turnSmoothing);
		}
		else
		{
			smoothX = h;
			smoothY = v;
		}

		tiltAngle -= smoothY * targetSpeed; 
		tiltAngle = Mathf.Clamp(tiltAngle, minAngle, maxAngle); 
		pivot.localRotation = Quaternion.Euler(tiltAngle, 0, 0);

		lookAngle += smoothX * targetSpeed; 
		transform.rotation = Quaternion.Euler(0, lookAngle, 0);

	}

	private void FixedUpdate()
	{
		float h = Input.GetAxis("Mouse X");
		float v = Input.GetAxis("Mouse Y");

	

		float targetSpeed = mouseSpeed;

		

		FollowTarget(Time.deltaTime);
		HandleRotations(Time.deltaTime, v, h, targetSpeed); 
	}

	private void LateUpdate()
	{
		
		float dist = cameraDist + 1.0f; 
		Ray ray = new Ray(camTrans.parent.position, camTrans.position - camTrans.parent.position);
		RaycastHit hit;
		
		if (Physics.Raycast(ray, out hit, dist))
		{
			if (hit.transform.tag == "Wall")
			{
				// store the distance;
				dist = hit.distance - 0.25f;
			}
		}
	
		if (dist > cameraDist) dist = cameraDist;
		camTrans.localPosition = new Vector3(0, 0, -dist);
	}

	public static CameraManager singleton; 
	void Awake()
	{
		singleton = this;
		Init();
	}

}
