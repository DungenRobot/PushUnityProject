using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public bool isOpen = false;
    public string nextLevelName;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        if (isOpen)
            SceneManager.LoadScene(nextLevelName);
    }
}
