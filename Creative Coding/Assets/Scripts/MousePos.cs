using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePos : MonoBehaviour
{
    //public Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Vector3 worldPosition;
    Plane plane = new Plane(Vector3.up, 0);
    void Update()
    {
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            transform.position = ray.GetPoint(distance);
        }
    }
}
