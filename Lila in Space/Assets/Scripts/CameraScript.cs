using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera cam = GetComponent<Camera>();
        cam.pixelRect = new Rect(10, 0, 1560, 1080);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
