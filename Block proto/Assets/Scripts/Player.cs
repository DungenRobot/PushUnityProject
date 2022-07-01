using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 
Player : MonoBehaviour
{
	private bool isWalking = false;

	private AudioSource _audioSource;
	
	private Animator anim;

	[SerializeField]
	private AudioClip stone_move;

	private Vector3 oldDir = Vector3.down;
	private Vector3 blockNewPos, blockOldPos, newPos, oldPos;

    private GameObject block = null;

    private float lerpTime;

	public float timeToMove = 0.01f;
	public float pushTime = 100f;

    private float moveTime;

    void Start()
    {
		anim = GetComponent<Animator>();
        newPos = transform.position;
		_audioSource = GetComponent<AudioSource>();
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
		

        transform.position = newPos;

        if (block != null)
        {
            block.transform.position = Vector3.Lerp(blockOldPos, blockNewPos, lerpTime);
        }
	
			

        transform.position = Vector3.Lerp(oldPos, newPos, lerpTime);
		
		if (lerpTime >= 1) {
			isWalking = false;
		    anim.SetBool("isWalking", false);
			_audioSource.Stop();
		}

        // if ((transform.position - newPos).magnitude != 0)
        // {
        //     transform.position = Vector3.Lerp(oldPos, newPos, lerpTime);
        // }
    }

	void setMove(Vector3 moveDirection)
	{
		isWalking = true;
		anim.SetBool("isWalking", true);
		anim.SetFloat("x", moveDirection.x);
		anim.SetFloat("y", moveDirection.y);
        _audioSource.Stop();
        lerpTime = 0f;

        if (block != null)
        {
            block.transform.position = blockNewPos;
            block = null;
        } 

        oldPos = newPos;
		transform.position = newPos;

		newPos += moveDirection;

        moveTime = timeToMove;

        //Raycast from where the player is to where the player is trying to move
        RaycastHit2D playerCast = Physics2D.Raycast(transform.position - (moveDirection/3), moveDirection, 1f);
        if (playerCast.collider != null) 
        {
            Collider2D collision = playerCast.collider;
            if (collision.gameObject.tag == "Block")
            {
                RaycastHit2D blockCast = Physics2D.Raycast(transform.position + moveDirection, moveDirection, 1f);
                if (blockCast.collider == null || blockCast.collider.gameObject.tag == "Button")
                {
                    moveTime = pushTime;

					block = collision.gameObject;

                    blockOldPos = block.transform.position;
					blockNewPos = block.transform.position + moveDirection;

                    //play audio
                    _audioSource.clip = stone_move;
					_audioSource.Play();
                    return;

                }
                else
                {
                    newPos = oldPos;
                    return;
                }
            }
            if (playerCast.collider.gameObject.tag == "Obstacle")
            {
                newPos = oldPos;
                return;
            }
        }
	}
}
