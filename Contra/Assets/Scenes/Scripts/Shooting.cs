using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour 
{
	public Transform canon0;
	public Transform canon45;
	public Transform canon90;
	public Transform canon135;
	public Transform canon180;
	public Transform canon225;
	public Transform canon315;

	public GameObject bulletPrefab;
	public float bulletSpeed;

	SpriteRenderer r;
	void Start()
	{
		r = GetComponent<SpriteRenderer>();
	}

	void Update () 
	{
		if(InputManager.AttackButtonPressed)
		{
			if(!r.flipX)
			{
				if(InputManager.VerticalAxis == 1 && InputManager.HorizontalAxis != 0)
				{
					//Diagonal 45
					Shoot(canon45);
				}
				else if(InputManager.VerticalAxis == 1 && InputManager.HorizontalAxis == 0)
				{
					// Arriba 90
					Shoot(canon90);
				}
				else if(InputManager.VerticalAxis == -1 && InputManager.HorizontalAxis != 0)
				{
					// Arriba 90
					Shoot(canon315);
				}
				else if(InputManager.VerticalAxis == 0)
				{
					// Derecha
					Shoot(canon0);
				}
			}
			else
			{
				if(InputManager.VerticalAxis == 1 && InputManager.HorizontalAxis != 0)
				{
					Shoot(canon135);
				}
				else if(InputManager.VerticalAxis == 1 && InputManager.HorizontalAxis == 0)
				{
					Shoot(canon90);
				}
				else if(InputManager.VerticalAxis == -1 && InputManager.HorizontalAxis != 0)
				{
					// Arriba 90
					Shoot(canon225);
				}
				else if(InputManager.VerticalAxis == 0)
				{
					Shoot(canon180);
				}
			}
		}
	}
	void Shoot(Transform canon)
	{
		var b = Instantiate(bulletPrefab,canon.position,canon.rotation).GetComponent<Rigidbody2D>();
		b.AddForce(canon.right * bulletSpeed, ForceMode2D.Impulse);
		Destroy(b.gameObject, 10);
	}
}
