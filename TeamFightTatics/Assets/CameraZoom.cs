using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 10.0f;

    private Camera mainCamera;
    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
        Zoom();
    }

    private void Zoom()
    {
        float distance = Input.GetAxis("Mouse ScrollWheel") * -1 * zoomSpeed;
        if (distance != 0)
        {
            mainCamera.fieldOfView += distance;
            mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, 20f, 100f);
        }
    }

}
