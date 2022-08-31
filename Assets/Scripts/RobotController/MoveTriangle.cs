using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MoveTriangle : MonoBehaviour {

    private Vector3 mOffset;
    private float mZCoord;

    public float Speed = 0.1f;
    private bool isMoveTriangle;
    Vector3 targetPosition;

    public GameObject LaserPoint;

    Vector3 oldPosition;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (isMoveTriangle == true)
        {
            SetPosition(Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime));
            if (transform.position == targetPosition)
            {
                isMoveTriangle = false;
            }
        }
        
        if (LaserPoint.transform.position.x != transform.position.x || LaserPoint.transform.position.z != transform.position.z)
        {
            LaserPoint.transform.position = new Vector3(transform.position.x, LaserPoint.transform.position.y, transform.position.z);
        }

        if (Input.GetMouseButtonUp(0) && (oldPosition != transform.localPosition))
        {
            oldPosition = transform.localPosition;
            LogPosition(transform.localPosition);
        }        
    }

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        
        // Store offset = gameobject world pos - mouse world pos

        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
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

        Debug.Log(GetMouseAsWorldPoint());
    }

    void OnMouseUp()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isMoveTriangle = true;
            targetPosition = GetMouseAsWorldPoint() + mOffset;
        }
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
            targetPosition.y = position.y;
        }
        if (Controller.GetInstance().isZFreeze == false)
        {
            targetPosition.z = position.z;
        }

        transform.position = targetPosition;
    }

    public void LogPosition(Vector3 position)
    {
        Controller.GetInstance().LogRelativePosition(position);
    }
}
