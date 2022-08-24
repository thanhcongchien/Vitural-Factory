using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControlRobotButton : MonoBehaviour
{
    public GameObject RobotMovement;
    public float intial_X, intial_Y, intial_Z, current_X, current_Y, current_Z, min_X, min_Y, min_Z, max_X, max_Y, max_Z;
    float step = 0.2f;

    void Start()
    {
        intial_X = RobotMovement.transform.position.x;
        intial_Y = RobotMovement.transform.position.y;
        intial_Z = RobotMovement.transform.position.z;
        min_X = intial_X - step;
        min_Y = intial_Y - step;
        min_Z = intial_Z - step;
        current_X = intial_X;
        current_Y = intial_Y;
        current_Z = intial_Z;
        max_X = intial_X + step;
        max_Y = intial_Y + step;
        max_Z = intial_Z + step;
    }


    public void ControlRobotMovement(float x, float y, float z)
    {
        {
            // RobotMovement.transform.position += new Vector3(x, y, z);
            // current_X = RobotMovement.transform.position.x;
            // current_Y = RobotMovement.transform.position.y;
            // current_Z = RobotMovement.transform.position.z;
            RobotMovement.transform.position += new Vector3(x,  y,  z);
            Vector3 newPosition = RobotMovement.transform.position;
            newPosition.x = Mathf.Clamp(newPosition.x,min_X, max_X);
            newPosition.y = Mathf.Clamp(newPosition.y,min_Y,max_Y);
            newPosition.z = Mathf.Clamp(newPosition.z,min_Z,max_Z);
            RobotMovement.transform.position = newPosition;


        }

        Debug.Log("Robot is moving");

    }
}
