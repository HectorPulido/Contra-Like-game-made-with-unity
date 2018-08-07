using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : MonoBehaviour 
{

	enum Direction
	{
		left,
		right
	}
	Direction dir;

	Animator anim;
	SpriteRenderer sr;
	Collider2D col;

	int currentNode = 0;
	public int lifes;
	public GameObject explosion;

	public float speed;
	public Transform[] nodes;


	void Start () 
	{
		sr = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
		col = GetComponent<Collider2D> ();
	}

	bool moving = true;
	Vector3 direction;
	void Update()
	{
		anim.SetBool ("Running", moving);
		if (!moving)
			return;

		direction = nodes [currentNode].position - transform.position;
		direction.Normalize ();

		if (direction.x != 0)
			sr.flipX = direction.x > 0;

		transform.position += direction * speed * Time.deltaTime;

		if(Vector3.Distance(transform.position, nodes[currentNode].position) < 0.1f)
		{
			currentNode++;
			if (currentNode >= nodes.Length)
				currentNode = 0;
		
			moving = false;

			StartCoroutine(DelayedEvents(()=>
				{
					moving = true;

				}, 1));
		}
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.CompareTag("Bullet"))
		{
			Destroy(col.gameObject);
			lifes--;

			if(lifes <= 0)
			{
				Destroy(gameObject);
				Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 1.2f);
			}
		}
	}
	IEnumerator DelayedEvents(System.Action ev, float time)
	{
		yield return new WaitForSeconds (time);
		ev ();
	}

}