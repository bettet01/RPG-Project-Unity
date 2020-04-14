using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public float pitch;
    public float zoomSpeed = 4f;
    public float minZoom = 5;
    public float maxZoom = 15;

    //turn speed
    public float yawSpeed;

    // private variables
    private float currentZoom = 6f;
    private float currentYaw = 0f;


    // Update is called once per frame
    void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        if (Input.GetMouseButton(1))
        {
            currentYaw += Input.GetAxis("Mouse X") * yawSpeed * Time.deltaTime;
        }

        
    }

    private void LateUpdate()
    {
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);
        transform.RotateAround(target.position, Vector3.up, currentYaw);

    }
}
