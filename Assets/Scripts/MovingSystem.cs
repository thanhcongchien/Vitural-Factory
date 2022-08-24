using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSystem : MonoBehaviour
{
    enum Axis
    {
        X = 0,
        Y = 1,
        Z = 2,
        Origin = 3
    };

    public GameObject AxisX;
    public GameObject AxisY;
    public GameObject AxisZ;
    public GameObject Origin;

    public GameObject MovingObject;

    Material originX;
    Material originY;
    Material originZ;

    public Material selectColor;
    Axis selectAxis;

    Ray ray;
    RaycastHit hit;

    Vector3 oldMousePosition;
    bool isMousePressing = false;

    void Start()
    {
        originX = AxisX.GetComponentInChildren<MeshRenderer>().material;
        originY = AxisY.GetComponentInChildren<MeshRenderer>().material;
        originZ = AxisZ.GetComponentInChildren<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMouseOverAxis();
        CheckMouseDrag();
    }

    void CheckMouseDrag()
    {
        if (Input.mousePosition.x != oldMousePosition.x || Input.mousePosition.y != oldMousePosition.y)
        {
            //Debug.Log(Input.mousePosition);
            oldMousePosition = Input.mousePosition;

            if (isMousePressing == true)
            {
                SetPosition(GetMouseAsWorldPoint() + mOffset);
                
            }            
        }
    }

    private Vector3 mOffset;
    private float mZCoord;    

    private Vector3 GetMouseAsWorldPoint()
    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = mZCoord;

        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void CheckMouseOverAxis()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isMousePressing = false;
            AxisX.GetComponentInChildren<MeshRenderer>().material = originX;
            AxisY.GetComponentInChildren<MeshRenderer>().material = originY;
            AxisZ.GetComponentInChildren<MeshRenderer>().material = originZ;
        }

        if (isMousePressing == true)
            return; 

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.transform.IsChildOf(gameObject.transform))
            {
                bool isAxis = false;
                if (hit.collider.gameObject == AxisX)
                {
                    AxisX.GetComponentInChildren<MeshRenderer>().material = selectColor;
                    selectAxis = Axis.X;
                    isAxis = true;
                }
                if (hit.collider.gameObject == AxisY)
                {
                    AxisY.GetComponentInChildren<MeshRenderer>().material = selectColor;
                    selectAxis = Axis.Y;
                    isAxis = true;
                }
                if (hit.collider.gameObject == AxisZ)
                {
                    AxisZ.GetComponentInChildren<MeshRenderer>().material = selectColor;
                    selectAxis = Axis.Z;
                    isAxis = true;
                }
                if (hit.collider.gameObject == Origin)
                {
                    Origin.GetComponentInChildren<MeshRenderer>().material = selectColor;
                    selectAxis = Axis.Origin;
                    isAxis = true;
                }

                if (isAxis = true && Input.GetMouseButtonDown(0))
                {
                    isMousePressing = true;

                    mZCoord = Camera.main.WorldToScreenPoint(MovingObject.transform.position).z;
                    mOffset = MovingObject.transform.position - GetMouseAsWorldPoint();
                }
            }
            else
            {
                AxisX.GetComponentInChildren<MeshRenderer>().material = originX;
                AxisY.GetComponentInChildren<MeshRenderer>().material = originY;
                AxisZ.GetComponentInChildren<MeshRenderer>().material = originZ;
            }
        }
    }

    public void SetPosition(Vector3 position)
    {
        if (MovingObject.transform.position == position)
            return;

        Vector3 targetPosition = MovingObject.transform.position;

        if (selectAxis == Axis.X)
        {
            targetPosition.x = position.x;
        }
        else if (selectAxis == Axis.Y)
        {
            targetPosition.y = position.y;
        }
        else if (selectAxis == Axis.Z)
        {
            targetPosition.z = position.z;
        }
        else if (selectAxis == Axis.Origin)
        {
            targetPosition = position;
        }

        MovingObject.transform.position = targetPosition;
    }
}
