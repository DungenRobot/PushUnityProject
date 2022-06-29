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
	private Vector3 blockNewPos, blockOldPos, newPos, oldPos;

    private GameObject block = null;

    private float lerpTime;

	public float timeToMove = 0.01f;
	public float pushTime = 100f;

    private bool pushing = false;

    private float moveTime;

    void Start()
    {
        newPos = transform.position;
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
		
        //Debug.Log("--");
        //Debug.Log(oldPos);
        //Debug.Log(newPos);

        if (pushing)
        {
            block.transform.position = Vector3.Lerp(blockOldPos, blockNewPos, lerpTime);
        }
        transform.position = Vector3.Lerp(oldPos, newPos, lerpTime);

        // if ((transform.position - newPos).magnitude != 0)
        // {
        //     transform.position = Vector3.Lerp(oldPos, newPos, lerpTime);
        // }
    }

	void setMove(Vector3 moveDirection)
	{
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

        pushing = false;

        //Raycast from where the player is to where the player is trying to move
        RaycastHit2D playerCast = Physics2D.Raycast(transform.position - (moveDirection/3), moveDirection, 1.4f);
        if (playerCast.collider != null) 
        {
            Collider2D collision = playerCast.collider;
            if (collision.gameObject.tag == "Block")
            {
                RaycastHit2D blockCast = Physics2D.Raycast(transform.position + moveDirection, moveDirection, 1.4f);
                if (blockCast.collider == null || blockCast.collider.gameObject.tag == "Button")
                {
                    
                    pushing = true;
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