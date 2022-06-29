using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 
PlayerEnt : MonoBehaviour
{
	private AudioSource _audioSource;

	[SerializeField]
	private AudioClip stone_move;

	private LevelExit exit;

	private Vector3 oldDir = Vector3.down;
	private Vector3 oldPos, newPos;

	public float moveTime = 0.01f;
	public float pushTime = 100f;
	
	private bool isMoving = false;

    void Start()
    {
		_audioSource = GetComponent<AudioSource>();
		exit = GameObject.Find("Level Exit").GetComponent<LevelExit>();
    }	

    // Update is called once per frame
    void Update()
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

		/* Perform a raycast from the origin of the player to the tile in the direction the player is facing */
		RaycastHit2D check = Physics2D.Raycast(transform.position, transform.TransformDirection(dir), 1f); 

		/* raw a line with the same properties as the raycast */
		/* TODO - remove for final build */
		//Debug.DrawRay(transform.position, transform.TransformDirection(dir), Color.green);

		/* if a colision occured, check if the collider was a box or a wall */

		/* of note: this if statement will always return true as RayCast2D will always return an object containing a collider
		even if this collider is of a Null value*/
		if (check) {
			Collider2D c = check.collider;
			if (c.gameObject.tag == "Block") {
				//Run another raycast from inside the box to verify that the box can be pushed
				RaycastHit2D boxCheck = Physics2D.Raycast(transform.position + dir, transform.TransformDirection(dir) + dir, 1f); 
				if (boxCheck.collider == null || boxCheck.collider.gameObject.tag == "Button") {
					pushing = true;
					_audioSource.clip = stone_move;
					_audioSource.Play();
					attached = c.gameObject;
					aOldPos = attached.transform.position;
					aNewPos = aOldPos + dir;
				}else{
					canmove = false;
				}
			}
			if (c.gameObject.tag == "Obstacle") 
				canmove = false;
		}
		
		isMoving = true;
		
		float elapsedTime = 0;

		oldPos = transform.position;
		newPos = oldPos + dir;
		
		if (!canmove) 
		newPos = oldPos;
		
		while (elapsedTime < (pushing ? pushTime : moveTime)) {
			if (pushing)
				attached.transform.position = Vector3.Lerp(aOldPos, aNewPos, (elapsedTime / pushTime));
			
			transform.position = Vector3.Lerp(oldPos, newPos, (elapsedTime / (pushing ? pushTime : moveTime)));
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
