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
    public int nextLevelID;
    private SpriteRenderer sprite;

    private GlobalData gameData;
    
	public float errorFlashTime = 0.1f;

	// Start is called before the first frame update
    void Start()
    {

        gameData = GameObject.Find("Game Data").GetComponent<GlobalData>();
		audioSource = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();

        if (gameData.latestLevel < (nextLevelID - 1))
        {
            gameData.latestLevel = nextLevelID - 1;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.LoadScene(1);
        }
    }

    public void Open()
    {
        isOpen = true;
        sprite.color = Color.white;
    }

    public void Close()
    {
        isOpen = false;
        sprite.color = new Color(0.491f, 0.491f, 0.491f, 1.000f);
    }

    void OnTriggerEnter2D()
    {
        if (isOpen) {
            SceneManager.LoadScene(nextLevelID);
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
        if (isOpen)
        {
            sprite.color = Color.white;
        }
	}
}
