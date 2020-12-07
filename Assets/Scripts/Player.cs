using System;
using UnityEngine;
using System.Collections;

//Kyle Code
using Photon.Pun;
//end kyle code

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {

	//Kyle code
	private PhotonView photonView;
	//end Kyle code
	
	public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	public float moveSpeed = 6;
	public float ropeVelocity;

	public Vector2 wallJumpClimb;
	public Vector2 wallJumpOff;
	public Vector2 wallLeap;

	public float wallSlideSpeedMax = 3;
	public float wallStickTime = .25f;
	float timeToWallUnstick;

	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

	Controller2D controller;
    private Animator ani;
    Vector3 characterScale;
    float characterScaleX;
   float characterScaley;

   public bool onRope;
   public bool attachedToRope;
   public bool canMove;
	void Start() {
		//Kyle code
		photonView = GetComponent<PhotonView>();
		//end Kyle code
		controller = GetComponent<Controller2D> ();

		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);
		print ("Gravity: " + gravity + "  Jump Velocity: " + maxJumpVelocity);

        ani = GetComponent<Animator>();
        characterScale = transform.localScale;
        characterScaleX = characterScale.x;
        characterScaley = characterScale.y;
        onRope = false;
        canMove = true;

	}

	void Update()
	{
		//Kyle Code
		//stops physics if player is not local instance
		if (!photonView.IsMine)
		{
			return;
		}
		//End Kyle Code 
		checkFalling();
		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		int wallDirX = (controller.collisions.left) ? -1 : 1;

        if(input.x > 0 && canMove)
        {
            characterScale.x = characterScaleX;
            ani.SetTrigger("run");
        }
        if(input.x < 0 && canMove)
        {
            characterScale.x = -characterScaleX;
            ani.SetTrigger("run");
        }
        transform.localScale = characterScale;
        
		float targetVelocityX = input.x * moveSpeed;
		if (canMove)
		{
			velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing,
				(controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
		}

		bool wallSliding = false;
		if ((controller.collisions.left || controller.collisions.right) && (!controller.collisions.below) &&
		    (Input.GetKey(KeyCode.Q)))
		{
			velocity.y = 0;
		}
		if ((controller.collisions.left || controller.collisions.right) && (!controller.collisions.below && velocity.y < 0) && (wallDirX == input.x)) {
			wallSliding = true;

			if (velocity.y < -wallSlideSpeedMax) {
				velocity.y = -wallSlideSpeedMax;
			}

			if (timeToWallUnstick > 0) {
				velocityXSmoothing = 0;
				velocity.x = 0;

				if (input.x != wallDirX && input.x != 0) {
					timeToWallUnstick -= Time.deltaTime;
				}
				else {
					timeToWallUnstick = wallStickTime;
				}
			}
			else {
				timeToWallUnstick = wallStickTime;
			}

		}

		if (Input.GetKeyDown (KeyCode.Space)) {

            ani.SetTrigger("jump");
            if (controller.collisions.left || controller.collisions.right) {
				if (wallDirX == input.x) {
					velocity.x = -wallDirX * wallJumpClimb.x;
					velocity.y = wallJumpClimb.y;
				}
				else if (input.x == 0) {
					velocity.x = -wallDirX * wallLeap.x;
					velocity.y = wallLeap.y;
					StartCoroutine(movementLockout(.3f, false));
				}
				else {
					velocity.x = -wallDirX * wallLeap.x;
					velocity.y = wallLeap.y;
					StartCoroutine(movementLockout(.3f, false));
				}
			}
			if (controller.collisions.below) {
				velocity.y = maxJumpVelocity;
			}
        }
		if (Input.GetKeyUp (KeyCode.Space)) {
			if (velocity.y > minJumpVelocity) {
				velocity.y = minJumpVelocity;
			}
		}
		

		if (onRope && !Input.anyKey)
		{
			velocity.y = 0;
		}

		if (onRope && Input.GetKey(KeyCode.W))
		{
			velocity.y = ropeVelocity;
		}
		if (onRope && Input.GetKey(KeyCode.S))
		{
			velocity.y = -ropeVelocity;
		}
		

		if (!attachedToRope)
		{
			velocity.y += gravity * Time.deltaTime;
		}

		
		controller.Move(velocity * Time.deltaTime, input);
		

		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}

        if(!Input.anyKey)
            ani.SetTrigger("idle");

	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.layer == 12)
		{
			damage();
		}
	}

	public void damage()
	{
		//play damage animation
		transform.position = new Vector3(0,0);
		StartCoroutine(movementLockout(1f, true));
	}

	private void checkFalling()
	{
		if (transform.position.y < -10)
		{
			transform.position = new Vector3(0,0);
		}
	}

	IEnumerator movementLockout(float lockoutTime, bool killMomentum)
	{
		canMove = false;
		if (killMomentum)
		{
			velocity.x = 0;
		}
		yield return new WaitForSeconds(lockoutTime);
		canMove = true;
	}
}
