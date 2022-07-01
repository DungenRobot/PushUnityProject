using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
	private AudioSource audioSource;
	[SerializeField]
	private AudioClip failSound;

  	public bool isOpen = false;
    public int levelID;
    private SpriteRenderer sprite;
    
	public float errorFlashTime = 0.1f;

    private int levelData;

	// Start is called before the first frame update
    void Start()
    {
		audioSource = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();

		levelData = GameObject.Find("Game Data").GetComponent<GlobalData>().latestLevel;
		if (levelData >= levelID)
		{
			Open();
		}
    }


    private void Open()
    {
        isOpen = true;
        sprite.color = Color.white;
    }

    void OnTriggerEnter2D()
    {
        if (isOpen) {
            SceneManager.LoadScene(levelID);
		} else {
			StartCoroutine(cFail());
		}
    }

	IEnumerator cFail()
	{
		audioSource.clip = failSound;
		audioSource.Play();
	
		Color startColor = Color.red;
		Color targetColor = sprite.color;

		float elapsedTime = 0;

		while (elapsedTime < errorFlashTime) {
			sprite.color = Color.Lerp(startColor, targetColor, elapsedTime / errorFlashTime);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		sprite.color = targetColor;
	}
}
