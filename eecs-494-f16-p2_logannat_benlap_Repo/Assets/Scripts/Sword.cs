using UnityEngine;
using System.Collections;

public class Sword : Projectile
{
	GameObject target;
	public float lifetime, damage;
	float xDistance, zDistance, xSpeed, zSpeed, yDistance, ySpeed;
	// Use this for initialization

	public override void Start()
	{
		int max = -1;
		foreach (car_status car in GameLogic.S.cars)
		{
			if (car.score > max && car.gameObject.tag != spawnedBy)
			{
				target = car.gameObject;
				max = car.score;
			}
		}
		if (max == -1)
		{
			Destroy(this.gameObject);
		}
		print("sp" + spawnedBy);
		print("tg" + target.tag);
		Invoke("end", lifetime);
	}

	void end()
	{
		Destroy(this.gameObject);
	}

	public override void FixedUpdate()
	{
		transform.LookAt(target.transform);
		transform.RotateAround(transform.position, transform.up, 180f);

		Vector3 current = this.gameObject.transform.position;
		xDistance = current.x - target.transform.position.x;
		yDistance = current.y - target.transform.position.y;
		zDistance = current.z - target.transform.position.z;

		if (Mathf.Abs(xDistance) > Mathf.Abs(zDistance))
		{
			xSpeed = (xDistance > 0) ? speed * -1 : speed;
			zSpeed = (zDistance > 0) ? Mathf.Abs((zDistance / xDistance) * speed) * -1 : Mathf.Abs((zDistance / xDistance) * speed);
		}
		else
		{
			zSpeed = (zDistance > 0) ? speed * -1 : speed;
			xSpeed = (xDistance > 0) ? Mathf.Abs((xDistance / zDistance) * speed) * -1 : Mathf.Abs((xDistance / zDistance) * speed);
		}
		ySpeed = (yDistance > 0) ? speed * -1 : speed;
		Move();
	}
	// Update is called once per frame
	public override void Move()
	{
		Vector3 tempPos = this.gameObject.transform.position;
		tempPos.z += zSpeed * Time.deltaTime;
		tempPos.x += xSpeed * Time.deltaTime;
		if (yDistance != 0) {
			tempPos.y += ySpeed * Time.deltaTime;
		}
		this.gameObject.transform.position = tempPos;
	}

	public void OnTriggerEnter(Collider coll)
	{

		if (coll.gameObject.tag != spawnedBy)
		{
			print ("destroyedby" + coll.gameObject);
			if (coll.gameObject.tag == "Car0")
			{
				GameLogic.S.cars[0].SwordHit(damage);
				Destroy(this.gameObject);
			}
			else if (coll.gameObject.tag == "Car1")
			{
				GameLogic.S.cars[1].SwordHit(damage);
				Destroy(this.gameObject);
			}
			else if (coll.gameObject.tag == "Car2")
			{
				GameLogic.S.cars[2].SwordHit(damage);
				Destroy(this.gameObject);
			}
			else if (coll.gameObject.tag == "Car3")
			{
				GameLogic.S.cars[3].SwordHit(damage);
				Destroy(this.gameObject);
			}
			Destroy (this.gameObject);
		}
	}
}
