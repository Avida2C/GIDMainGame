using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
  
    //the speed which the stars are moving
    public float speed = 4f;
    //the position which the stars need to reach before reseting
    public float resetPosition = 0;
    //the default starting position
    private Vector2 StartPosition;


    void Start()
    {
        //the position of the StartPosition
        StartPosition = new Vector2 (-0.03f, 26.14f);
    }


    
    void Update()
    {
        //moves the stars down
        transform.Translate(Vector3.down*speed*Time.deltaTime);
        //if the current position of Y is smaller than the reset position
        if (transform.position.y < resetPosition)
        {
            //transform the position of stars to the StartPosition
            transform.position = StartPosition;
        }
    }
}
