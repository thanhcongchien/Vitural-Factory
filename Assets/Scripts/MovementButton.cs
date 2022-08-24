using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementButton : MonoBehaviour
{
    public XRButton xrButton;
    public ControlRobotButton controlRobotButton;
    public float x, y, z;
    bool isCollision = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (xrButton.currentPress)
        {
            controlRobotButton.ControlRobotMovement(x, y, z);
        }
    }

    // public void onclickButton()
    // {
    //     {
    //             controlRobotButton.ControlRobotMovement(x, y, z);
    //     }

    // }
}
