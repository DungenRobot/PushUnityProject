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
        
    }

    public void Open()
    {
        isOpen = true;
        sprite.color = Color.white;
    }

    void OnTriggerEnter2D()
    {
        if (isOpen)
            SceneManager.LoadScene(nextLevelName);
    }
}
