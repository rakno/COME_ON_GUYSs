using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

public class CharacterControls : MonoBehaviour {
	private Animator animator;

	private AudioClip jumpclip;
	public AudioSource jumpclipSource;

	private AudioClip breaksound;
	public AudioSource breakSource;




	public float speed = 10.0f;
	public float airVelocity = 8f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
	public float jumpHeight = 2.0f;
	public float maxFallSpeed = 20.0f;
	public float rotateSpeed = 25f; 
	private Vector3 moveDir;
	public GameObject cam;
	private Rigidbody rb;

	private float distToGround;

	private bool canMove = true; 
	private bool isStuned = false;
	private bool wasStuned = false; 
	private float pushForce;
	private Vector3 pushDir;

	public Vector3 checkPoint;
	private bool slide = false;

	void  Start (){

		animator = GetComponent<Animator>();
	
		distToGround = GetComponent<Collider>().bounds.extents.y;
	}
	
	bool IsGrounded (){
		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
	}
	
	void Awake () {
		rb = GetComponent<Rigidbody>();
		rb.freezeRotation = true;
		rb.useGravity = false;

		checkPoint = transform.position;
		Cursor.visible = false;
	}
	
	void FixedUpdate () {

		animator.SetFloat("Speed", moveDir.magnitude);
		animator.SetBool("IsGrounded", IsGrounded());
		animator.SetBool("IsStunned", isStuned);
		
		if (canMove)
		{
			if (moveDir.x != 0 || moveDir.z != 0)
			{
				
				Vector3 targetDir = moveDir; 

				targetDir.y = 0;
				if (targetDir == Vector3.zero)
					targetDir = transform.forward;
				Quaternion tr = Quaternion.LookRotation(targetDir); 
				Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, Time.deltaTime * rotateSpeed); 
				transform.rotation = targetRotation;
			}

			if (IsGrounded())
			{
			 
				Vector3 targetVelocity = moveDir;
				targetVelocity *= speed;

				
				Vector3 velocity = rb.velocity;
				if (targetVelocity.magnitude < velocity.magnitude) 
				{
					targetVelocity = velocity;
					rb.velocity /= 1.1f;
				}
				Vector3 velocityChange = (targetVelocity - velocity);
				velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
				velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
				velocityChange.y = 0;
				if (!slide)
				{
					if (Mathf.Abs(rb.velocity.magnitude) < speed * 1.0f)
						rb.AddForce(velocityChange, ForceMode.VelocityChange);
				}
				else if (Mathf.Abs(rb.velocity.magnitude) < speed * 1.0f)
				{
					rb.AddForce(moveDir * 0.15f, ForceMode.VelocityChange);
					
				}

			
				if (IsGrounded() && Input.GetButton("Jump"))
				{

					jumpclipSource.Play();
					rb.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
				}
			}
			else
			{
				if (!slide)
				{
					Vector3 targetVelocity = new Vector3(moveDir.x * airVelocity, rb.velocity.y, moveDir.z * airVelocity);
					Vector3 velocity = rb.velocity;
					Vector3 velocityChange = (targetVelocity - velocity);
					velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
					velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
					rb.AddForce(velocityChange, ForceMode.VelocityChange);
					if (velocity.y < -maxFallSpeed)
						rb.velocity = new Vector3(velocity.x, -maxFallSpeed, velocity.z);
				}
				else if (Mathf.Abs(rb.velocity.magnitude) < speed * 1.0f)
				{
					rb.AddForce(moveDir * 0.15f, ForceMode.VelocityChange);
				}
			}
		}
		else
		{
			rb.velocity = pushDir * pushForce;
		}
		
		rb.AddForce(new Vector3(0, -gravity * GetComponent<Rigidbody>().mass, 0));
	}

	private void Update()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		Vector3 v2 = v * cam.transform.forward; 
		Vector3 h2 = h * cam.transform.right; 
		moveDir = (v2 + h2).normalized; 
		RaycastHit hit;
		if (Physics.Raycast(transform.position, -Vector3.up, out hit, distToGround + 0.1f))
		{
			if (hit.transform.tag == "Slide")
			{
				slide = true;
			}
			else
			{
				slide = false;
			}
		}
	
	}

	float CalculateJumpVerticalSpeed () {
		
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}

	public void HitPlayer(Vector3 velocityF, float time)
	{
		rb.velocity = velocityF;

		pushForce = velocityF.magnitude;
		pushDir = Vector3.Normalize(velocityF);
		StartCoroutine(Decrease(velocityF.magnitude, time));
	}

	public void LoadCheckPoint()
	{
		transform.position = checkPoint;
	}

	private IEnumerator Decrease(float value, float duration)
	{
		if (isStuned)

			wasStuned = true;
		Debug.Log("is player stunned?: "+isStuned);

		isStuned = true;
		Debug.Log("player is stunned");
		canMove = false;

		float delta = 0;
		delta = value / duration;

		for (float t = 0; t < duration; t += Time.deltaTime)
		{
			yield return null;
			if (!slide) 
			{
				pushForce = pushForce - Time.deltaTime * delta;
				pushForce = pushForce < 0 ? 0 : pushForce;
				
			}
			rb.AddForce(new Vector3(0, -gravity * GetComponent<Rigidbody>().mass, 0)); //Add gravity
		}
		Add(4, 5);
		if (wasStuned)
		{
			wasStuned = false;
			
		}
		else
		{
			isStuned = false;
			Debug.Log("player stunned is set to false: ");
			canMove = true;
		}
	}

	public int Add(int a, int b)
    {
		int c = a + b;
		return c;
    }

	private bool hasCollided = false;

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.CompareTag("breakables"))
        {
			breakSource.Play();

		}
		if (!hasCollided && collision.gameObject.CompareTag("FinishLine"))
		{
			hasCollided = true;
			Invoke("ChangeScene", 2f);
		}
		
	}

	void ChangeScene()
	{
		
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

	
		int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

		
		SceneManager.LoadScene("Win");
	}
}
