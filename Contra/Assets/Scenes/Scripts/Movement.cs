using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Movement : MonoBehaviour 
{
	[Header("Control")]
	public float speed = 10;
	public float jumpSpeed = 3;
	public GameObject explosion;


	[Header("GroundCheck")]
	public Transform groundCheckPosition;
	public Vector2 groundCheckSize;

	SpriteRenderer sr;
	Rigidbody2D rb;
	BetterJump bt;
	Animator anim;
	Collider2D colli;
	bool canPlay = true;

	public bool IsGround
	{
		get
		{
			var cc = Physics2D.BoxCast(groundCheckPosition.position, groundCheckSize,0, Vector2.up); 

			if (cc.collider == null)
				return false;
			if (cc.collider.gameObject == gameObject)
				return false;		
			
			return true;
		}
	}
		
	void Start () 
	{
		sr = GetComponent<SpriteRenderer> ();
		rb = GetComponent<Rigidbody2D> ();
		bt = GetComponent<BetterJump> ();
		anim = GetComponent<Animator>();
		colli = GetComponent<Collider2D>();
		rb.freezeRotation = true;
	}
	void OnDrawGizmosSelected()
	{
		if (!groundCheckPosition)
			return;
		Gizmos.DrawWireCube ((Vector3) groundCheckPosition.position, (Vector3)groundCheckSize);
	}

	float hor;
	float ver;
	bool ground;
	void Update () 
	{
		if(!canPlay)
			return;

		ground = IsGround;
	
		hor = InputManager.HorizontalAxis;
		ver = InputManager.VerticalAxis;

		anim.SetFloat("X", hor);
		anim.SetFloat("Y", ver);
		anim.SetBool("Ground", ground);
		anim.SetBool("Water", water);

		if(InputManager.JumpButtonPressed && ground)
			jumpRequest = true;

		ManageFlip ();

	}

	bool jumpRequest = false;
	void FixedUpdate()
	{
		if(!canPlay)
			return;
		if(ver < 0)
		{
			jumpRequest = false;
			return;
		}			

		if (jumpRequest) 
		{
			jumpRequest = false;
			if(!water)
				rb.AddForce (Vector2.up * jumpSpeed, ForceMode2D.Impulse);		
		}
	
		rb.velocity = new Vector3 (hor * speed, rb.velocity.y);	

		bt.ApplyBetterJump ();
			
	}
	void ManageFlip()
	{
		if(hor != 0)
			sr.flipX = hor < 0;
	}
	IEnumerator DelayedEvents(System.Action ev, float time)
	{
		yield return new WaitForSeconds (time);
		ev ();
	}

	bool water;
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.CompareTag("Water"))
		{
			water = true;
		}
		else if(col.CompareTag("Bullet"))
		{
			Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 1.5f);

			canPlay = false;
			sr.enabled = false;
			rb.isKinematic = true;
			colli.enabled = false;
			
			StartCoroutine(DelayedEvents(()=>{
				canPlay = true;
				sr.enabled = true;
				rb.isKinematic = false;
				colli.enabled = true;
			}, 1));
		}
		else if(col.CompareTag("Platform"))
		{
			if(!water)
				return;
			rb.AddForce (Vector2.up * jumpSpeed, ForceMode2D.Impulse);		
		}
	}
	void OnTriggerExit2D(Collider2D col)
	{
		if(col.CompareTag("Water"))
		{
			water = false;
		}
	}


}
