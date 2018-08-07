using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	Transform player;
	Collider2D col;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag("Player").transform;
		col = GetComponent<Collider2D>();	
	}
	
	bool k;
	void Update () 
	{
		if(k)
			return;

		var c = player.transform.position.y > transform.position.y;
		if(InputManager.JumpButtonPressed && InputManager.VerticalAxis < 0)
		{
			k = true;
			c = false;
			StartCoroutine(DelayedEvents(()=>{
				k = false;
				c = true;
			}, 0.2f));
		}
		col.isTrigger = !c;
	}
	IEnumerator DelayedEvents(System.Action ev, float time)
	{
		yield return new WaitForSeconds (time);
		ev ();
	}
}
