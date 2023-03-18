using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    public float seamRepeat;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;    
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < startPos.x - seamRepeat)
        { 
            transform.position = startPos;
        }
    }
}
