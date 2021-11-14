using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float cameraSpeed;
    [SerializeField] float scrollSpeed;

    private Vector2 camLimit;
    [SerializeField] float zoomLimitMin;
    float zoomLimitMax;

    Camera cam;
    float _firstCOZ;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        _firstCOZ = cam.orthographicSize;
        zoomLimitMax = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        ZoomInOut();
        
        CameraMove();
    }

    private void CameraMove()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            pos.y += cameraSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            pos.x -= cameraSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            pos.y -= cameraSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            pos.x += cameraSpeed * Time.deltaTime;
        }
        pos.x = Mathf.Clamp(pos.x, -camLimit.x, camLimit.x);
        pos.y = Mathf.Clamp(pos.y, -camLimit.y, camLimit.y);

        transform.position = pos;
    }

    private void ZoomInOut()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float newSize = cam.orthographicSize - scroll * scrollSpeed;
        cam.orthographicSize = Mathf.Clamp(newSize, zoomLimitMin, zoomLimitMax);

        camLimit.x = 2 * (_firstCOZ - cam.orthographicSize);
        camLimit.y = _firstCOZ - cam.orthographicSize;
    }

}
