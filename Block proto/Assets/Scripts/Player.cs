using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 
Player : MonoBehaviour
{
	private AudioSource _audioSource;

	[SerializeField]
	private AudioClip stone_move;

	private LevelExit exit;

	private Vector3 oldDir = Vector3.down;
	private Vector3 blockNewPos, newPos, oldPos;

    private GameObject block = null;

    public float lerpTime;

	public float timeToMove = 0.01f;
	public float pushTime = 100f;

    private float moveTime;
	
	private bool isMoving = false;

    void Start()
    {
        newPos = transform.position;
        Debug.Log(newPos);
		_audioSource = GetComponent<AudioSource>();
		exit = GameObject.Find("Level Exit").GetComponent<LevelExit>();
    }	

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.A))
            setMove(Vector3.left);
	
		if (Input.GetKeyDown(KeyCode.D))
            setMove(Vector3.right);	

		if (Input.GetKeyDown(KeyCode.W))
            setMove(Vector3.up);

		if (Input.GetKeyDown(KeyCode.S))
            setMove(Vector3.down);

        lerpTime += Time.deltaTime / moveTime;
		
        Debug.Log("--");
        Debug.Log(oldPos);
        Debug.Log(newPos);
        transform.position = Vector3.Lerp(oldPos, newPos, lerpTime);

        // if ((transform.position - newPos).magnitude != 0)
        // {
        //     transform.position = Vector3.Lerp(oldPos, newPos, lerpTime);
        // }
    }

	void setMove(Vector3 moveDirection)
	{
        lerpTime = 0f;

        oldPos = newPos;
		transform.position = newPos;

		newPos += moveDirection;

        moveTime = timeToMove;

        //bool canmove = true;
        //bool pushing = false;

        //Raycast from where the player is to where the player is trying to move
        //RaycastHit2D playerCast = Physics2D.Raycast(transform.position, newPos, 1f);

        // if (playerCast.collider.gameObject != null) 
        // {
        //     if (playerCast.collider.gameObject.tag == "Block")
        //     {
        //         RaycastHit2D blockCast = Physics2D.Raycast(transform.position + moveDirection, newPos + moveDirection, 1f);
        //         if (blockCast.collider == null || blockCast.collider.gameObject.tag == "Button")
        //         {
        //             //get the block for position changing later
        //             pushing = true;
		// 			block = playerCast.collider.gameObject;
                    
		// 			blockNewPos = block.transform.position + moveDirection;

        //             //play audio
        //             _audioSource.clip = stone_move;
		// 			_audioSource.Play();
        //             return;

        //         }
        //         else
        //         {
        //             newPos = transform.position;
        //             return;
        //         }
        //     }
        //     if (playerCast.collider.gameObject.tag == "Obstacle")
        //     {
        //         newPos = transform.position;
        //         return;
        //     }
        // }
	}
}