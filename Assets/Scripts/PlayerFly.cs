using UnityEngine;
using System.Collections;

public class PlayerFly : MonoBehaviour 
{
	#region public float variables
	//public float variables
	public float speed = 2.0f;
	public float smooth = 2.0f;
	public float startupBoost = 1f;
	public float topSpeed = 25f;
	#endregion


	#region private float variables
	//private float variables
	private float b;
	private float end;
	private float moveVert;
	private float moveHoriz;
	private float adjHoriz;
	private float adjVert;//used to make turning and accelerating easier
	#endregion


	#region RigidBody
	//rigidbody on sphere
	public Rigidbody2D sphere;
	#endregion

	#region ForceMode
	public ForceMode2D force;
	#endregion

	#region fixedupdate
	// Update is called once per frame
	void FixedUpdate () 
	{
		moveHoriz = Input.GetAxis ("Horizontal");
		moveVert = Input.GetAxis ("Vertical");
		var vel = GetComponent<Rigidbody2D>().velocity;
		//Debug.Log (vel);

		#region thruster
		if (vel [0] < 10f && moveHoriz > 0) 
		{
			adjHoriz = 1;
		} 
		else if (vel [0] > -10f && moveHoriz < 0) 
		{
			adjHoriz = -1;
		} else 
		{
			adjHoriz = 0f;
		}

//		if(vel[2] < 10f && moveVert > 0)
//		{
//			adjVert = 1;
//		}
//		else if (vel [2] > -10f && moveVert < 0) 
//		{
//			adjVert = -1;
//		} 
//		else 
//		{
//			adjVert = 0f;
//		}

		Vector2 thruster = new Vector2 (adjHoriz, adjVert);
		#endregion

		#region vector math
		Vector2 movement = new Vector2(moveHoriz,moveVert);

		if (movement.magnitude > 1) 
		{
			movement = movement/movement.magnitude;
		}

		sphere.AddForce ((movement + (thruster.normalized * startupBoost)) * speed, force);
		sphere.drag = b;
		sphere.angularDrag = b;


		//limits the speed of the ball, mostly to prevent clipping through the geometry
		Vector2 antiSpeed = new Vector2 (-vel [0], -vel [1]);
		if (vel.magnitude > topSpeed) 
		{
			sphere.AddForce (antiSpeed, force);
		}
		#endregion
	}
	#endregion
}
