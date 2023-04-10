using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    public float speed = 4f;
    public float resetPosition = 0;
    private Vector3 StartPosition;
    // Start is called before the first frame update
    void Start()
    {
        StartPosition = new Vector2 (-0.03f, 26.14f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down*speed*Time.deltaTime);
        if (transform.position.y < resetPosition)
        {
            transform.position = StartPosition;
        }
    }
}
