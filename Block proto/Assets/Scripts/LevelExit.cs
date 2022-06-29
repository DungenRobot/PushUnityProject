using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
	private AudioSource audioSource;
	[SerializeField]
	private AudioClip failSound;
    public bool isOpen = false;
    public string nextLevelName;
    private SpriteRenderer sprite;
    
	public float errorFlashTime = 0.1f;

	// Start is called before the first frame update
    void Start()
    {
		audioSource = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        isOpen = true;
        sprite.color = Color.white;
    }

    void OnTriggerEnter2D()
    {
        if (isOpen) {
            SceneManager.LoadScene(nextLevelName);
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
