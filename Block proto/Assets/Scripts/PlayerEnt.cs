using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnt : MonoBehaviour
{
	private Vector3 oldDir = Vector3.down;
	private Vector3 oldPos, newPos;
	public float moveTime = 0.15f;
	public float pushTime = 0.2f;
	private bool isMoving = false;

    void Start()
    {
    }

    // Update is called once per frame
    void 
	Update()
    {
		if (Input.GetKey(KeyCode.A) && !isMoving)
			StartCoroutine(Move(Vector3.left));
		
		if (Input.GetKey(KeyCode.D) && !isMoving)
			StartCoroutine(Move(Vector3.right));	

		if (Input.GetKey(KeyCode.W) && !isMoving)
			StartCoroutine(Move(Vector3.up));

		if (Input.GetKey(KeyCode.S) && !isMoving)
			StartCoroutine(Move(Vector3.down));
    }

	private IEnumerator 
	Move(Vector3 dir) 
	{
		bool canmove = true;
		bool pushing = false;
		Vector3 aOldPos = Vector3.zero, aNewPos = Vector3.zero;

		GameObject attached = null;
		RaycastHit2D check = Physics2D.Raycast(transform.position + (Vector3.up * 0.5f), transform.TransformDirection(dir), 1f); 
		Debug.DrawRay(transform.position + new Vector3(0.0f, 0.5f, 0.0f), transform.TransformDirection(dir), Color.green);
		if (check) {
			Collider2D c = check.collider;
			if (c.gameObject.tag == "Block")
				pushing = true;
				attached = c.gameObject;
				aOldPos = attached.transform.position;
				aNewPos = aOldPos + dir;
			if (c.gameObject.tag == "Obstacle")
				canmove = false;
		}
		
		isMoving = true;
		
		float elapsedTime = 0;

		oldPos = transform.position;
		newPos = oldPos + dir;
		if (!canmove) newPos = oldPos;

		while (elapsedTime < pushTime) {
			if (pushing)
				attached.transform.position = Vector3.Lerp(aOldPos, aNewPos, (elapsedTime / pushTime));
			transform.position = Vector3.Lerp(oldPos, newPos, (elapsedTime / moveTime));
			//transform.position = newPos;
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		if (pushing)
			attached.transform.position = aNewPos;
		transform.position = newPos;
		oldDir = dir;
		isMoving = false;
	}
} 
