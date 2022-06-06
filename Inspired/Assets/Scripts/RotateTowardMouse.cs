using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FaceMouse();
    }

    private void FaceMouse()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePos);
        

        Vector2 directionToLook = worldPoint - transform.position;
        transform.up = -directionToLook;
    }
}
