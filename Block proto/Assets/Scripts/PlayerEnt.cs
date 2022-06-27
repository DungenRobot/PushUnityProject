using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnt : MonoBehaviour
{
	private Vector3 oldDir = Vector3.down;
	private Vector3 oldPos, newPos;
	public float moveTime = 0.2f;
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
		isMoving = true;
		
		float elapsedTime = 0;

		oldPos = transform.position;
		newPos = oldPos + dir;

		while (elapsedTime < moveTime) {
			transform.position = Vector3.Lerp(oldPos, newPos, (elapsedTime / moveTime));
			//transform.position = newPos;
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		transform.position = newPos;
		oldDir = dir;
		isMoving = false;
	}
} 
