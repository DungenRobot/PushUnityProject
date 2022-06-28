using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
	public bool active = false;
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Block") {
			active = true;
			Button[] buttons = FindObjectsOfType<Button>();
			int a = 0;
			for (int i = 0; i < buttons.Length; i++) {
				if (buttons[i].active)
					a+=1;
			}
			if (a == buttons.Length) 
				Debug.Log("you win!");
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag == "Block")
			active = false;
	}
}