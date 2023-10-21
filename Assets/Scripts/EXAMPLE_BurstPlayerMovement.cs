using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;


	private float moveSpeed = 30f;           // Value that controls movement speed

	private float dashSpeed = 2400f;          // Value that controls burst option speed

	private float dashDuration = 45f;        // Value that controls the length of the dash duration.

	private float dashCooldown = 240f;

    private float dashFrameCount = -1;

    private float cdFrameCount = -1;
	
	private float moveVelocity = 0f;        // Placeholder used for calculated value

	private Rigidbody2D rb2d;               // Reference to physics component on player
	
	private bool jump = false;               // Condition for whether the player inputs jump.

	private bool dash = false;				// Condition for whether the player inputs burst move.

	private float jumpForce = 2750f;			// Amount of force added when the player jumps.

	private bool grounded = false;			// Whether or not the player standing on the ground.
	



	// Use this for initialization
	void Start () {
		
		rb2d = GetComponent<Rigidbody2D>();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (dash && dashFrameCount < dashDuration && cdFrameCount == -1)
		{
			if (dashFrameCount == -1)
			{
				dashFrameCount = 0;
			}
			if (facingRight)
				rb2d.AddForce(transform.right * dashSpeed);
			else if (!facingRight)
                rb2d.AddForce(-transform.right * dashSpeed);
        }

		if (Input.GetAxis("Horizontal") > 0 && !facingRight){
			Flip();
		}
		else if (Input.GetAxis("Horizontal") < 0 && facingRight){
			Flip();
		}

		if(Input.GetButtonDown("Jump") && grounded) {
			jump = true;
		}

		if (dashFrameCount >= dashDuration && dash)
		{
			dashFrameCount = -1;
			if (cdFrameCount == -1)
			{
				cdFrameCount = 0;
			}
			dash = false;
		}

        if (dashFrameCount != -1)
        {
			//Debug.Log("Dash " + dashFrameCount);
            dashFrameCount++;
        }

		if (cdFrameCount >= dashCooldown)
		{
			cdFrameCount = -1;
		}

		if (cdFrameCount != -1)
		{
			//Debug.Log("CD " + cdFrameCount);
			cdFrameCount++;
		}

        //if (grounded){
        //Only allow movement when player is in contact with asset tagged "Ground"
        MoveControl();
		//}
	}

	//Called at a fixed framerate used for physics calculations and logic
	void FixedUpdate(){
		rb2d.velocity = new Vector2(moveVelocity, rb2d.velocity.y);

        // If the player should jump...
        if (jump)
		{
			// Add a vertical force to the player.
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));

			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			jump = false;

		}
	}

	//Custom function called as needed
	void Flip()
	{
		//Reverse the X scale of player to make the face opposite direction.
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


	//Custom function called as needed
	void MoveControl () {

		bool directionalPress = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
		if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift) && cdFrameCount == -1)
		{
			dash = true;
		}

		if (!dash)
		{
            //Reset Velocity value.
            moveVelocity = 0f;
            //Changed GetAxis to GetAxisRaw to eliminate annoying slide
            if (Input.GetAxisRaw("Horizontal") < 0f)
			{
				moveVelocity = -moveSpeed;
			}
			else if (Input.GetAxisRaw("Horizontal") > 0f)
			{

				moveVelocity = moveSpeed;
			}
		}
		
	}
	
	//Detects when a game collider starts to overlap with this GameObject
	void OnTriggerEnter2D(Collider2D coll) {
		
		//Debug.Log ("Collided with: " + coll.gameObject.tag);
		if (coll.gameObject.tag == "Ground") {
			grounded = true;
		}

	}


	//Detects when a game collider stops overlapping with this GameObject
	void OnTriggerExit2D(Collider2D coll) {
		
		//Debug.Log ("Collided with: " + coll.gameObject.tag);
		if (coll.gameObject.tag == "Ground") {
			grounded = false;
		}
		
	}

}
