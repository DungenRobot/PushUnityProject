using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOffsetter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {

        Debug.Log(RoundPixel(transform.parent.position) - transform.parent.position);

        transform.localPosition = (RoundPixel(transform.parent.position) - transform.parent.position);
    }

    Vector3 RoundPixel(Vector3 input)
    {
        input *= 32;
        input.x = Mathf.Round(input.x);
        input.y = Mathf.Round(input.y);
        input.z = Mathf.Round(input.z);
        input /= 32;
        return input;
    }
}
