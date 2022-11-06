using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovementSocketIO : MonoBehaviour
{

    public static RobotMovementSocketIO instance;
    public GameObject movingSystemBase;
    public GameObject test;

    public float intial_X, intial_Y, intial_Z, current_X, current_Y, current_Z, min_X, min_Y, min_Z, max_X, max_Y, max_Z;
    float stepDefatult = 0.2f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = GetComponent<RobotMovementSocketIO>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        intial_X = movingSystemBase.transform.position.x;
        intial_Y = movingSystemBase.transform.position.y;
        intial_Z = movingSystemBase.transform.position.z;
        min_X = intial_X - stepDefatult;
        min_Y = intial_Y - stepDefatult;
        min_Z = intial_Z - stepDefatult;
        current_X = intial_X;
        current_Y = intial_Y;
        current_Z = intial_Z;
        max_X = intial_X + stepDefatult;
        max_Y = intial_Y + stepDefatult;
        max_Z = intial_Z + stepDefatult;
    }


    public void ControlRobotMovement(string direction, float step)
    {
        float stepMove = step / 10;
        if (direction.Contains("FORWARD"))
        {
            movingSystemBase.transform.position += new Vector3(0, 0, stepMove);
            Debug.Log("FORWARD");
        }
        if (direction.Contains("BACKWARD"))
        {
            movingSystemBase.transform.position += new Vector3(0, 0, -stepMove);
        }
        if (direction.Contains("LEFT"))
        {
            movingSystemBase.transform.position += new Vector3(-stepMove, 0, 0);
        }
        if (direction.Contains("RIGHT"))
        {
            movingSystemBase.transform.position += new Vector3(stepMove, 0, 0);
        }
        if (direction.Contains("UP"))
        {
            movingSystemBase.transform.position += new Vector3(0, stepMove, 0);
        }
        if (direction.Contains("DOWN"))
        {
            movingSystemBase.transform.position += new Vector3(0, -stepMove, 0);
        }

        {
            Debug.Log("Invalid direction");
        }

        Vector3 newPosition = movingSystemBase.transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, min_X, max_X);
        newPosition.y = Mathf.Clamp(newPosition.y, min_Y, max_Y);
        newPosition.z = Mathf.Clamp(newPosition.z, min_Z, max_Z);
        movingSystemBase.transform.position = newPosition;


        Debug.Log("Robot is moving: " + movingSystemBase.transform.position);
    }
}
