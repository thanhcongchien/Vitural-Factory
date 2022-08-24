using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoving : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;

    public float Speed = 1.0f;
    private bool isMoveTriangle;
    Vector3 targetPosition;

    Material origin;
    public Material selectColor;

    void Start()
    {
        origin = GetComponent<MeshRenderer>().material;
        gameObject.SetActive(true);
    }

    void Update()
    {
        if (isMoveTriangle == true)
        {
            SetPosition(Vector3.MoveTowards(transform.position, targetPosition, Speed));
            if (transform.position == targetPosition)
            {
                isMoveTriangle = false;
            }
        }
    }

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // Store offset = gameobject world pos - mouse world pos

        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
        GetComponent<MeshRenderer>().material = selectColor;
    }

    private Vector3 GetMouseAsWorldPoint()
    {

        // Pixel coordinates of mouse (x,y)

        Vector3 mousePoint = Input.mousePosition;



        // z coordinate of game object on screen

        mousePoint.z = mZCoord;



        // Convert it to world points

        return Camera.main.ScreenToWorldPoint(mousePoint);

    }

    void OnMouseDrag()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            return;
        }
        SetPosition(GetMouseAsWorldPoint() + mOffset);
        
    }

    void OnMouseUp()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isMoveTriangle = true;
            targetPosition = GetMouseAsWorldPoint() + mOffset;
        }

        GetComponent<MeshRenderer>().material = origin;
    }

    public void SetPosition(Vector3 position)
    {
        if (transform.position == position)
            return;

        Vector3 targetPosition = transform.position;

        if (Controller.GetInstance().isXFreeze == false)
        {
            targetPosition.x = position.x;
        }
        if (Controller.GetInstance().isYFreeze == false)
        {
            targetPosition.y = position.z;
        }
        if (Controller.GetInstance().isZFreeze == false)
        {
            targetPosition.z = position.y;
        }

        transform.position = targetPosition;

        LogPosition(transform.localPosition);
    }

    public void LogPosition(Vector3 position)
    {
        //Controller.GetInstance().LogRelativePosition(position);
    }
}
