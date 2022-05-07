using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePos2D : MonoBehaviour
{
    public Camera mainCamera;
    public Transform aim;
    public GameObject crosshair;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        transform.position = mouseWorldPosition;
        crosshair.transform.position = mouseWorldPosition;
    }
}
