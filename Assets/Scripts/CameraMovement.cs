using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    public float MoveSensitive = 0.4f;
    public float RotateSensitive = 1f;
    public float RotateRadius = 5.0f;

    public Transform oldTransform;
    void Start()
    {

    }

    void Update()
    {
        MouseInput();
    }

    void MouseInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            oldTransform = transform;            
        }
        if (Input.GetMouseButton(0))
        {

        }
        else if (Input.GetMouseButton(1))
        {
            MouseRightClick();
        }
        else if (Input.GetMouseButton(2))
        {
            MouseMiddleButtonClicked();            
        }
        else
        {
            MouseWheeling();
        }
    }

    void ShowAndUnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void HideAndLockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void MouseMiddleButtonClicked()
    {
        float deltaX = Input.GetAxis("Mouse X");
        float deltaY = Input.GetAxis("Mouse Y");

        Vector3 pos = transform.position;
        pos = pos + transform.up * -deltaY * MoveSensitive;
        pos = pos + transform.right * -deltaX * MoveSensitive;

        transform.position = pos;
        
    }

    void MouseRightClick()
    {
        float deltaX= Input.GetAxis ("Mouse X");
        float deltaY = Input.GetAxis ("Mouse Y");

        //Vector3 rotation = transform.eulerAngles;

        //rotation.x += -deltaY * RotateSensitive;
        //rotation.y += deltaX * RotateSensitive;

        //transform.eulerAngles = rotation;

        transform.RotateAround(oldTransform.position + oldTransform.forward * RotateRadius, new Vector3(-deltaY * RotateSensitive, deltaX * RotateSensitive, 0), 1);
        Vector3 rotary = transform.eulerAngles;
        rotary.z = 0;
        transform.eulerAngles = rotary;

    }

    void MouseWheeling()
    {
        Vector3 pos = transform.position;
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            pos = pos - transform.forward;
            transform.position = pos;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            pos = pos + transform.forward;
            transform.position = pos;
        }
    }

}
