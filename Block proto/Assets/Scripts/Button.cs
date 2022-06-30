using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{	
	private AudioSource audioSource;
	[SerializeField]
	private AudioClip buttonPress;

	public bool active = false;
	private LevelExit exit;
	// Start is called before the first frame update
    void Start()
    {
		audioSource = GetComponent<AudioSource>();
		exit = GameObject.Find("Level Exit").GetComponent<LevelExit>();
    }	

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Block") {
			active = true;
			audioSource.clip = buttonPress;
			audioSource.Play();
			Button[] buttons = FindObjectsOfType<Button>();
			int a = 0;
			for (int i = 0; i < buttons.Length; i++) {
				if (buttons[i].active)
					a+=1;
			}
			if (a == buttons.Length) 
				exit.Open();
		}
	}



	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag == "Block"){
			active = false;
			exit.Close();
		}
	}
}
